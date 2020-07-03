using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("SalesSummary")]
    public class SaleSummary
    {
        [Key]
        public int Id { get; set; }
        public int SaleCod { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientDocumentNumber { get; set; }
        public string ResellerName { get; set; }
        public string SaleDescription { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime SaleDate { get; set; }
        public TimeSpan SaleTime { get; set; }
        public double SaleAmount { get; set; }
    }
}
