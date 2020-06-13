using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("DescripcionVenta")]
    public class SaleDescription
    {
        [Key]
        [Column("VentaId")]
        public int SaleId { get; set; }

        [Column("Descripcion")]
        public string Description { get; set; }
    }
}
