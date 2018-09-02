using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data;
using Sports.Football.Data.Model;

namespace Sports.Football.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<IEnumerable<Team>> AddNonExistingTeamsAsync(IList<Team> teams);
    }
}