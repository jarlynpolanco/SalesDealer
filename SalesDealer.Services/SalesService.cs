using FileHelpers;
using Microsoft.Extensions.Configuration;
using SalesDealer.Data;
using SalesDealer.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesDealer.Services
{
    public class SalesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _destFile;

        public SalesService(IConfiguration config, AppDbContext appDbContext) 
        {
            _destFile = config["AppSettings:DestFilePath"];
            _appDbContext = appDbContext;
        }

        public string GenerateSalesFile() 
        {
            var sales = GetAllSales();
            var engine = new FileHelperEngine<SalesFH>();
            string fileName = $"{Path.Combine(_destFile, Guid.NewGuid().ToString().Replace("-", string.Empty))}.txt";
            engine.WriteFile(fileName, sales);

            return Path.GetFileName(fileName);
        }

        public IList<SalesFH> GetSalesFromFile(string fileName) 
        {
            string path = Path.Combine(_destFile, fileName);

            return new FileHelperEngine<SalesFH>().ReadFileAsList(path);
        }

        public IList<SalesFH> GetAllSales() 
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
