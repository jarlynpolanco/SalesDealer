using SalesDealer.UI.Contracts;
using System.Net.Http;

namespace SalesDealer.UI.Services
{
    public class FileRepository : BaseRepository<string>, IFileRepository
    {
        public FileRepository(HttpClient httpClient) : base(httpClient) { }
    }
}
