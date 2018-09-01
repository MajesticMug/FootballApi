using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Sports.Football.Core.ServiceClient.Model.Exceptions;

namespace Sports.Football.Core.ServiceClient
{
    public abstract class HttpServiceClient
    {
        private readonly HttpClient _httpClient;

        protected HttpServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        protected async Task<TRoot> GetRootAsync<TRoot>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TRoot>(json);
            }
            else
            {
                throw new ServiceClientException($"Could not get root object for uri: {uri}");
            }
        }
    }
}