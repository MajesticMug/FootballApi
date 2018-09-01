using System.Net.Http;
using Sports.Football.Core.ServiceClient.Model;

namespace Sports.Football.Core.ServiceClient
{
    public class CompetitionClient : HttpServiceClient
    {
        public CompetitionClient(HttpClient httpClient) : base(httpClient)
        {
        }


    }
}