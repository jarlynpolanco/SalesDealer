using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDealer.UI.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> Get(string url, string parameter);
        Task<T> Get(string url);
    }
}
