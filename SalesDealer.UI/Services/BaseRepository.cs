using SalesDealer.Shared;
using SalesDealer.UI.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SalesDealer.UI.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HttpClient _client;

        public BaseRepository(HttpClient client) 
        {
            _client = client;
        }

        public async Task<IList<T>> Get(string url, string parameter) 
        {
            var response = await _client.GetFromJsonAsync<GenericResponse<IList<T>>>($"{url}/{parameter}");
            return response.Data;
        }

        public async Task<T> Get(string url) 
        {
            var response = await _client.GetFromJsonAsync<GenericResponse<T>>(url);
            return response.Data;
        }
    }
}
