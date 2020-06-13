using FileHelpers;

namespace SalesDealer.Shared
{
    [DelimitedRecord(";")]
    public class SalesFH
    {
        public string SaleCod { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientDocumentNumber { get; set; }
        public string ResellerCName { get; set; }
        public string SaleDescription { get; set; }
        public string ServiceDescription { get; set; }
        public string SaleDate { get; set; }
        public string SaleTime { get; set; }
        public string SaleAmount { get; set; }
    }
}
