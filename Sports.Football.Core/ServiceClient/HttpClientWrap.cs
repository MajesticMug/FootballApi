using System.Net.Http;
using System.Threading.Tasks;

namespace Sports.Football.Core.ServiceClient
{
    public class HttpClientWrap : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrap(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _httpClient.GetAsync(uri);
        }
    }
}