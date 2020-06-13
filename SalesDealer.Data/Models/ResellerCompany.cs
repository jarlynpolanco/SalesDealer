using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDealer.Data.Models
{
    [Table("EmpresaRevendedora")]
    public class ResellerCompany
    {
        [Key]
        [Column("EmpresaRevendedoraId")]
        public int ResellerCompanyId { get; set; }

        [Column("Nombre")]
        public string Name { get; set; }

        public virtual IList<Sale> Sales { get; set; }
    }
}
