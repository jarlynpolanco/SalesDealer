namespace SalesDealer.Shared
{
    public class AppSettings
    {
        public string DestFilePath { get; set; }

        public SftpSettings SftpSettings { get; set; }

        public PgpSettings PgpSettings { get; set; }
    }
}
