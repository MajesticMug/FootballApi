using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sports.Football.Tests.Utilities;
using Sports.Football.Web.Controllers;
using Xunit;

namespace Sports.Football.Web.Tests
{
    public class FootballControllerTests : FootballTestBase
    {
        [Fact]
        public async Task GivenValidLeagueCodeImportShouldStoreData()
        {
            var controller = new FootballController(MockComponentsProvider(GetInMemoryDbContext()));

            var result = await controller.Import("BL1");

            var objectResult = result as ObjectResult;

            Assert.NotNull(objectResult);

            Assert.True(objectResult.StatusCode == 201);

            var value = objectResult.Value.ToString();

            Assert.True(value == "{ Message = Successfully imported }");
        }
        
        [Fact]
        public async Task GivenInvalidLeagueCodeImportShouldReturn404()
        {
            var controller = new FootballController(MockComponentsProvider(GetInMemoryDbContext()));

            var result = await controller.Import(Guid.NewGuid().ToString());

            var objectResult = result as NotFoundObjectResult;

            Assert.NotNull(objectResult);

            var value = objectResult.Value.ToString();

            Assert.True(value == "{ Message = Not found }");
        }
        
        [Fact]
        public async Task GivenAlreadyImportedLeagueCodeShouldReturnConflict()
        {
            var controller = new FootballController(MockComponentsProvider(GetInMemoryDbContext()));

            await controller.Import("BL1");
            var result = await controller.Import("BL1");

            var objectResult = result as ConflictObjectResult;

            Assert.NotNull(objectResult);

            var value = objectResult.Value.ToString();

            Assert.True(value == "{ Message = League already imported }");
        }
    }
}
