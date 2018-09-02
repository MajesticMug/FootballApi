using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Sports.Football.Core.ServiceClient.Model.Exceptions;

namespace Sports.Football.Core.ServiceClient
{
    public class FootballHttpServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        public FootballHttpServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<TRoot> GetRootAsync<TRoot>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TRoot>(json);
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                // requests-per-minute limit exceeded
                throw new RequestNumberLimitExceededException("Too many request were made");
            }

            throw new ServiceClientException($"Could not get root object for uri: {uri}. HttpStatus code: {response.StatusCode}");
        }
    }
}