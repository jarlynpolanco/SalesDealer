using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("Clientes")]
    public class Client
    {
        [Key]
        [Column("Cedula")]
        public string DocumentNumber { get; set; }

        [Column("Nombre")]
        public string Name { get; set; }

        [Column("Apellido")]
        public string LastName { get; set; }

        public virtual IList<Service> Services { get; set; }

    }
}
