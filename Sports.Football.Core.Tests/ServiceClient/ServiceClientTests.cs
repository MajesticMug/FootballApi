using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sports.Football.Core.ServiceClient;
using Sports.Football.Core.ServiceClient.Mappers;
using Sports.Football.Core.ServiceClient.Model;
using Sports.Football.Tests.Utilities;
using Sports.Football.Tests.Utilities.Mocks;
using Xunit;

namespace Sports.Football.Core.Tests.ServiceClient
{
    public class ServiceClientTests : FootballTestBase
    {
        [Fact]
        public async Task GivenValidResponseShouldRetrieveRootObjectCorrectly()
        {
            var client = MockServiceClient();

            var result = await client.GetRootAsync<CompetitionsRootObject>("competitions");
            
            Assert.NotNull(result);
            Assert.True(result.Competitions.Count() == 147);
        }

        [Fact]
        public async Task GivenValidLeagueCodeShouldRetrieveCorrectCompetition()
        {
            var serviceClient = MockServiceClient();

            var footballClient = new FootballClient(serviceClient, new AutoModelMapper());

            var result = await footballClient.GetCompetitionByLeagueCodeAsync("BL1");

            Assert.NotNull(result);
            Assert.True(result.LeagueCode == "BL1");
        }
    }
}
