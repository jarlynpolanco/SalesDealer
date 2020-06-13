using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("Venta")]
    public class Sale
    {
        [Key]
        [Column("CodRegistro")]
        public int SaleId { get; set; }

        [Column("NoDocumento")]
        public string DocumentNumber { get; set; }

        [Column("CodEmpresaRevendedoraFK")]
        public int ResellerCompanyId { get; set; }

        [Column("NoServicio")]
        public int ServiceId { get; set; }

        [Column("Fecha")]
        public DateTime Date { get; set; }

        [Column("Hora")]
        public TimeSpan Time { get; set; }

        [Column("Monto")]
        public double Amount { get; set; }

        public virtual IList<Client> Clients { get; set; }
        public virtual IList<SaleDescription> SaleDescriptions { get; set; }

    }
}
