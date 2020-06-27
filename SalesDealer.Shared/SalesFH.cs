using FileHelpers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SalesDealer.Shared
{
    [DelimitedRecord(";")]
    [XmlRoot(ElementName = "Sale")]
    public class SalesFH
    {
        [XmlElement(ElementName = "SaleCod")]
        public string SaleCod { get; set; }
        [XmlElement(ElementName = "ClientName")]
        public string ClientName { get; set; }
        [XmlElement(ElementName = "ClientLastName")]
        public string ClientLastName { get; set; }
        [XmlElement(ElementName = "ClientDocumentNumber")]
        public string ClientDocumentNumber { get; set; }
        [XmlElement(ElementName = "ResellerCName")]
        public string ResellerCName { get; set; }
        [XmlElement(ElementName = "SaleDescription")]
        public string SaleDescription { get; set; }
        [XmlElement(ElementName = "ServiceDescription")]
        public string ServiceDescription { get; set; }
        [XmlElement(ElementName = "SaleDate")]
        public string SaleDate { get; set; }
        [XmlElement(ElementName = "SaleTime")]
        public string SaleTime { get; set; }
        [XmlElement(ElementName = "SaleAmount")]
        public string SaleAmount { get; set; }
    }

    [XmlRoot(ElementName = "Sales")]
    public class Sales 
    {
        [XmlElement(ElementName = "Sale")]
        public List<SalesFH> Sale { get; set; }
    }

    [XmlRoot(ElementName = "SalesRoot")]
    public class SalesRoot 
    {
        [XmlElement(ElementName = "Sales")]
        public Sales Sales { get; set; }
    }
}
