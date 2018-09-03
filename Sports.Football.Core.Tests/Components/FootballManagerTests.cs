using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sports.Football.Core.Model.Exceptions;
using Sports.Football.Tests.Utilities;
using Xunit;

namespace Sports.Football.Core.Tests.Components
{
    public class FootballManagerTests : FootballTestBase
    {
        [Fact]
        public async Task GivenValidLeagueCodeShouldStoreDataInDatabase()
        {
            var memoryContext = GetInMemoryDbContext();

            var manager = MockFootballManager(memoryContext);

            await manager.ImportLeagueFromApiAsync("BL1");

            var importedCompetitions = await MockCompetitionRepository(memoryContext).GetAll().ToListAsync();
            var importedTeams = await MockTeamRepository(memoryContext).GetAll().ToListAsync();
            var importedPlayers = await MockPlayerRepository(memoryContext).GetAll().ToListAsync();
            
            Assert.True(importedCompetitions.Count == 1);
            Assert.True(importedTeams.Any());
            Assert.True(importedPlayers.Any());
        }

        [Fact]
        public async Task GivenImportedLeagueShouldReturnCorrectPlayerCount()
        {
            var dbContext = GetInMemoryDbContext();

            var manager = MockFootballManager(dbContext);

            await manager.ImportLeagueFromApiAsync("BL1");

            var playerCount = await manager.GetTotalPlayers("BL1");

            Assert.True(playerCount == 1184);
        }

        [Fact]
        public async Task GivenInvalidLeagueCodeImportShouldThrowCorrectException()
        {
            var manager = MockFootballManager(GetInMemoryDbContext());

            await Assert.ThrowsAsync<CompetitionNotFoundException>(
                async () => await manager.ImportLeagueFromApiAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task GivenInvalidLeagueCodeTotalPlayersShouldThrowCorrectException()
        {
            var manager = MockFootballManager(GetInMemoryDbContext());

            await Assert.ThrowsAsync<CompetitionNotFoundException>(
                async () => await manager.GetTotalPlayers(Guid.NewGuid().ToString()));
        }
    }
}
