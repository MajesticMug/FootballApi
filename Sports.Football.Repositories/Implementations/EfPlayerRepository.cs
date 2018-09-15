using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data;
using Sports.Football.Data.Model;
using Sports.Football.Repositories.Interfaces;

namespace Sports.Football.Repositories.Implementations
{
    public class EfPlayerRepository : EntityFrameworkRepository<Player>, IPlayerRepository
    {
        public EfPlayerRepository(FootballDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddPlayersToTeamAsync(Team team, IList<Player> players)
        {
            foreach (var player in players)
            {
                player.TeamId = team.Id;
            }

            await CreateManyAsync(players);
        }
    }
}