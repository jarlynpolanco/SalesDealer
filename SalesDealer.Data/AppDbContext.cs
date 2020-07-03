using Microsoft.EntityFrameworkCore;
using SalesDealer.Data.Models;
using System.Threading.Tasks;

namespace SalesDealer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ResellerCompany> ResellerCompanies { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDescription> SaleDescriptions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SaleSummary> SaleSummaries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
