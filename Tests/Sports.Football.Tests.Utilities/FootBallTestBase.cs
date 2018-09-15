using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sports.Football.Core.Components;
using Sports.Football.Core.ServiceClient;
using Sports.Football.Core.ServiceClient.Mappers;
using Sports.Football.Data;
using Sports.Football.Repositories.Implementations;
using Sports.Football.Repositories.Interfaces;
using Sports.Football.Tests.Utilities.Mocks;

namespace Sports.Football.Tests.Utilities
{
    public abstract class FootballTestBase
    {
        public static IComponentsProvider MockComponentsProvider(FootballDbContext dbContext)
        {
            return new DefaultComponentsProvider(
                MockFootballManager(dbContext), new TracerLogManager());
        }

        public static IFootballManager MockFootballManager(FootballDbContext dbContext)
        {
            return new FootballManager(
                MockFootballClient(),
                MockCompetitionRepository(dbContext),
                MockPlayerRepository(dbContext),
                MockTeamRepository(dbContext),
                new TracerLogManager());
        }

        public static ICompetitionRepository MockCompetitionRepository(FootballDbContext dbContext)
        {
            return new EfCompetitionRepository(dbContext);
        }

        public static ITeamRepository MockTeamRepository(FootballDbContext dbContext)
        {
            return new EfTeamRepository(dbContext);
        }

        public static IPlayerRepository MockPlayerRepository(FootballDbContext dbContext)
        {
            return new EfPlayerRepository(dbContext);
        }

        public static FootballDbContext GetInMemoryDbContext()
        {
            var dbContextOptions                             
                = new DbContextOptionsBuilder<FootballDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return new FootballDbContext(dbContextOptions);
        }

        public static IFootballClient MockFootballClient()
        {
            return new FootballClient(MockServiceClient(), new AutoModelMapper());
        }

        public static IHttpClientWrapper MockHttpClientWrapper()
        {
            var httpClientWrapper = new Mock<IHttpClientWrapper>();

            httpClientWrapper
                .Setup(x => x.GetAsync(It.IsRegex("competitions$")))
                .Returns(() =>
                    Task.FromResult(HttpResponseBuilder.Build(ServiceResponses.Competitions.All147Competitions)));

            httpClientWrapper
                .Setup(x => x.GetAsync(It.IsRegex("\\/teams")))
                .Returns(() =>
                    Task.FromResult(HttpResponseBuilder.Build(ServiceResponses.Competitions.CompetitionTeams)));

            httpClientWrapper
                .Setup(x => x.GetAsync(It.IsRegex("teams\\/")))
                .Returns(() =>
                    Task.FromResult(HttpResponseBuilder.Build(ServiceResponses.Teams.TeamDetail)));

            return httpClientWrapper.Object;
        }

        public static IServiceClient MockServiceClient()
        {
            return new FootballHttpServiceClient(MockHttpClientWrapper());
        }
    }
}