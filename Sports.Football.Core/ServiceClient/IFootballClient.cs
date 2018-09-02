using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data.Model;

namespace Sports.Football.Core.ServiceClient
{
    public interface IFootballClient
    {
        Task<Competition> GetCompetitionByLeagueCodeAsync(string leagueCode);
        Task<IEnumerable<Team>> GetTeamsByCompetition(Competition competition);
        Task<IEnumerable<Player>> GetPlayersByTeamAsync(Team team);
    }
}