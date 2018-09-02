using System.Threading.Tasks;

namespace Sports.Football.Core.Components
{
    public interface IFootballManager
    {
        Task ImportLeagueFromApiAsync(string leagueCode);
        Task<int> GetTotalPlayers(string leagueCode);
    }
}