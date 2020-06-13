using Microsoft.Extensions.Options;
using PgpCore;
using SalesDealer.Shared;
using System.IO;

namespace SalesDealer.Services
{
    public class PgpEncryptionService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly PGP _pgpCore;

        public PgpEncryptionService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _pgpCore = new PGP();
        }

        public MemoryStream DescryptFileAsStream(string encryptedFile)
        {
            using var outputFileStream = new MemoryStream();
            using Stream inputFileStream = File.OpenRead(encryptedFile);
            using Stream privateKeyStream = File.OpenRead(_appSettings.Value.PgpSettings.PgpPrivateKey);
            _pgpCore.DecryptStream(inputFileStream, outputFileStream, privateKeyStream, _appSettings.Value.PgpSettings.PgpPassShared);
            return new MemoryStream(outputFileStream.ToArray());
        }

        public MemoryStream EncryptStreamFile(StreamReader inputFile)
        {
            using var outputFileStream = new MemoryStream();
            using Stream publicKeyStream = File.OpenRead(_appSettings.Value.PgpSettings.PgpPublicKey);
            _pgpCore.EncryptStream(inputFile.BaseStream, outputFileStream, publicKeyStream, false, false);
            return new MemoryStream(outputFileStream.ToArray());
        }
    }
}
