using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data;
using Sports.Football.Data.Model;

namespace Sports.Football.Repositories.Interfaces
{
    public interface ICompetitionRepository : IRepository<Competition>
    {
        Task AddTeamsForCompetitionAsync(Competition competition, IEnumerable<Team> teams);
        Task<Competition> GetByLeagueCodeAsync(string leagueCode);
    }
}