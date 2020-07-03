using AutoMapper;
using FileHelpers;
using Microsoft.Extensions.Options;
using SalesDealer.Core;
using SalesDealer.Data;
using SalesDealer.Data.Models;
using SalesDealer.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace SalesDealer.Services
{
    public class SalesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly PgpEncryptionService _pgpEncryptionService;
        private readonly SftpManagementService _sftpManagementService;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public SalesService(AppDbContext appDbContext, PgpEncryptionService pgpEncryptionService,
            SftpManagementService sftpManagementService, IMapper mapper, IOptions<AppSettings> appSettings) 
        {
            _appDbContext = appDbContext;
            _pgpEncryptionService = pgpEncryptionService;
            _sftpManagementService = sftpManagementService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public string GenerateSalesFile() 
        {
            var sales = GetAllSales();

            if (sales == null || !sales.Any())
                throw new HttpStatusException($"No hay ventas disponibles. Favor revisar la fuente de datos.",
                    HttpStatusCode.Forbidden);

            var engine = new FileHelperEngine<SalesFH>();
            string fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}.pgp";

            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);
            
            engine.WriteStream(streamWriter, sales);
            streamWriter.AutoFlush = true;
            stream.Position = 0;
            using var streamReader = new StreamReader(stream);
            var encryptedFile = _pgpEncryptionService.EncryptStreamFile(streamReader);
            _sftpManagementService.SftpUploadFile(encryptedFile, fileName);
            

            return fileName;
        }

        public string GenerateSalesOnXML() 
        {
            var sales = GetAllSales();

            if (sales == null || !sales.Any())
                throw new HttpStatusException($"No hay ventas disponibles. Favor revisar la fuente de datos.",
                    HttpStatusCode.Forbidden);

            var salesRoot = new SalesRoot()
            {
                Sales = new Sales() { Sale = sales.ToList() }
            };

            var xml = SerializerHelper.ObjectToXml(salesRoot);

            var file = Path.Combine(_appSettings.Value.XmlPath, $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}.xml");

            File.WriteAllText(file, xml);

            return file;
        }

        public IList<SalesFH> GetSalesFromFile(string fileName) 
        {
            var fullFileName = _sftpManagementService.SftpDownloadFile(fileName);

            if (!File.Exists(fullFileName))
                throw new HttpStatusException($"El archivo con el nombre indicado: {fileName} no existe en el SFTP.",
                    HttpStatusCode.Forbidden);

            var streamFile = _pgpEncryptionService.DescryptFileAsStream(fullFileName);
            using TextReader textReader = new StreamReader(streamFile);

            return new FileHelperEngine<SalesFH>().ReadStream(textReader);
        }

        public void PersistSalesFromXML(SalesRoot salesRoot) 
        {
            IList<SaleSummary> saleSummariesList = new List<SaleSummary>();

            salesRoot?.Sales?.Sale?.ForEach(x =>
            {
                saleSummariesList.Add(_mapper.Map<SaleSummary>(x));
            });

            _appDbContext.SaleSummaries.AddRange(saleSummariesList);
            _appDbContext.SaveChanges();
        }

        private IList<SalesFH> GetAllSales() 
        {
            return (from sale in _appDbContext.Sales
                    join client in _appDbContext.Clients on sale.DocumentNumber equals client.DocumentNumber
                    join resellerCompany in _appDbContext.ResellerCompanies on sale.ResellerCompanyId equals resellerCompany.ResellerCompanyId
                    join saleDescription in _appDbContext.SaleDescriptions on sale.SaleId equals saleDescription.SaleId
                    join service in _appDbContext.Services on sale.ServiceId equals service.ServiceId
                    select new SalesFH
                    {
                        SaleCod = sale.SaleId.ToString(),
                        ClientName = client.Name,
                        ClientLastName = client.LastName,
                        ClientDocumentNumber = sale.DocumentNumber,
                        ResellerCName = resellerCompany.Name,
                        SaleDescription = saleDescription.Description,
                        SaleDate = sale.Date.ToString("dd/MM/yyyy"),
                        SaleTime = sale.Time.ToString(),
                        SaleAmount = sale.Amount.ToString()
                    }).ToList();
        }
    }
}
