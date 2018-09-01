using System.Collections.Generic;
using System.Threading.Tasks;
using Sports.Football.Data.Model;

namespace Sports.Football.Core.ServiceClient
{
    public interface ILeagueClient
    {
        Task<IEnumerable<League>> GetLeaguesAsync(string leagueCode);
    }
}