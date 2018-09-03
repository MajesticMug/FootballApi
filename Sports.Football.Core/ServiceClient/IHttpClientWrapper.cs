using System.Net.Http;
using System.Threading.Tasks;

namespace Sports.Football.Core.ServiceClient
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}