using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("Servicio")]
    public class Service
    {
        [Key]
        [Column("ServicioId")]
        public int ServiceId { get; set; }

        [Column("Cedula")]
        public string DocumentNumber { get; set; }

        [Column("Descripcion")]
        public string Description { get; set; }
    }
}
