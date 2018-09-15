using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sports.Football.Tests.Utilities;
using Sports.Football.Web.Controllers;
using Xunit;

namespace Sports.Football.Web.Tests
{
    public class PlayerControllerTests : FootballTestBase
    {
        [Fact]
        public async Task GivenValidLeagueCodeShouldReturnPlayerCount()
        {
            var componentsProvider = MockComponentsProvider(GetInMemoryDbContext());
            var footballController = new FootballController(componentsProvider);
            var playerController = new PlayerController(componentsProvider);

            await footballController.Import("BL1");

            var result = await playerController.TotalPlayers("BL1");

            var objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult);
            var value = objectResult.Value.ToString();

            Assert.True(value == "{ total = 1184 }");
        }
    }
}
