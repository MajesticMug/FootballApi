using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data;
using Sports.Football.Data.Model;

namespace Sports.Football.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task AddPlayersToTeamAsync(Team team, IList<Player> players);
    }
}