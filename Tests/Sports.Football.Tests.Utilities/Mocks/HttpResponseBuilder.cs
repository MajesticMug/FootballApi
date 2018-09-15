using System.Net;
using System.Net.Http;
using System.Text;

namespace Sports.Football.Tests.Utilities.Mocks
{
    public class HttpResponseBuilder
    {
        public static HttpResponseMessage Build(string jsonContent)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };
        }
    }
}