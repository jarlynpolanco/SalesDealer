using Microsoft.Extensions.Options;
using Renci.SshNet;
using SalesDealer.Shared;
using System.IO;

namespace SalesDealer.Services
{
    public class SftpManagementService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private ConnectionInfo _connectionInfo;

        public SftpManagementService(IOptions<AppSettings> appSettings) =>
            _appSettings = appSettings;

        public void SftpUploadFile(Stream fileStream, string fileName)
        {
            var sftpConnection = GetSftpConnection();

            using var sftp = new SftpClient(sftpConnection);
            sftp.Connect();
            sftp.UploadFile(fileStream, fileName);
            sftp.ChangePermissions(fileName, 777);

            sftp.Disconnect();
        }

        public string SftpDownloadFile(string fileName)
        {
            var sftpConnection = GetSftpConnection();
            var outPutFileName = Path.Combine(_appSettings.Value.DestFilePath, fileName);

            using var sftp = new SftpClient(sftpConnection);
            sftp.Connect();
            using Stream fileStream = File.Create(outPutFileName);

            if (!sftp.Exists(fileName))
                return string.Empty;

            sftp.DownloadFile(fileName, fileStream);

            sftp.Disconnect();

            return outPutFileName;
        }

        private ConnectionInfo GetSftpConnection()
        {
            if (_connectionInfo == null)
                _connectionInfo = new ConnectionInfo(_appSettings.Value.SftpSettings.SftpHost,
                    _appSettings.Value.SftpSettings.SftpPort,
                    _appSettings.Value.SftpSettings.SftpUserName,
                       new PasswordAuthenticationMethod(_appSettings.Value.SftpSettings.SftpUserName,
                       _appSettings.Value.SftpSettings.SftpPassword));

            return _connectionInfo;
        }
    }
}
