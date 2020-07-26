using SalesDealer.Shared;
using SalesDealer.UI.Contracts;
using System.Net.Http;

namespace SalesDealer.UI.Services
{
    public class SalesRepository : BaseRepository<SalesFH>, ISalesRepository
    {
        public SalesRepository(HttpClient httpClient) : base(httpClient) { }
    }
}
