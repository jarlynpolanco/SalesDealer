
namespace SalesDealer.UI.Static
{
    public static class Endpoints
    {
        public static readonly string BaseUrl = "https://localhost:5001/";
        public static readonly string GenerateSalesEndpoint = $"{BaseUrl}api/Sales/GenerateAllSales";
        public static readonly string GetSalesEndpoint = $"{BaseUrl}api/Sales/GetSalesFromFile";
    }
}