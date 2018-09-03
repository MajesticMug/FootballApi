using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sports.Football.Core.Model.Exceptions;
using Sports.Football.Core.ServiceClient;
using Sports.Football.Core.ServiceClient.Model.Exceptions;
using Sports.Football.Repositories.Interfaces;

namespace Sports.Football.Core.Components
{
    public class FootballManager : IFootballManager
    {
        private readonly IFootballClient _footballClient;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ILogManager _log;

        public FootballManager(
            IFootballClient footballClient, 
            ICompetitionRepository competitionRepository, 
            IPlayerRepository playerRepository, 
            ITeamRepository teamRepository, 
            ILogManager log)
        {
            _footballClient = footballClient;
            _competitionRepository = competitionRepository;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _log = log;
        }

        public async Task ImportLeagueFromApiAsync(string leagueCode)
        {
            var competitionAlreadyImported = await _competitionRepository.ExistsAsync(c => leagueCode.Equals(c.LeagueCode));

            if (competitionAlreadyImported)
            {
                throw new CompetitionAlreadyImportedException($"Competition with code '{leagueCode}' is already imported");
            }

            var competition = await _footballClient.GetCompetitionByLeagueCodeAsync(leagueCode);

            if (competition == null)
            {
                throw new CompetitionNotFoundException($"Competition with code '{leagueCode}' was not found");
            }

            var teams = (await _footballClient.GetTeamsByCompetition(competition)).ToList();

            await _competitionRepository.CreateAsync(competition);

            await _teamRepository.AddNonExistingTeamsAsync(teams);

            await _competitionRepository.AddTeamsForCompetitionAsync(competition, teams);

            foreach (var team in teams)
            {
                try
                {
                    // todo: these can be done in parallel, maybe
                    var players = await _footballClient.GetPlayersByTeamAsync(team);
                    await _playerRepository.AddPlayersToTeamAsync(team, players.ToList());
                }
                catch (RequestNumberLimitExceededException)
                {
                    // too many requests
                    _log.Warn($"Exceeded number of request when importing players for teamId {team.Id}");
                    break;
                    // todo: enqueue teams to be handled by another process
                }
            }
        }

        public async Task<int> GetTotalPlayers(string leagueCode)
        {
            var competitionExists = await _competitionRepository.GetAll().AnyAsync(c => c.LeagueCode == leagueCode);

            if (!competitionExists)
            {
                throw new CompetitionNotFoundException();
            }

            return await _competitionRepository
                .GetAll()
                .Where(c => c.LeagueCode == leagueCode)
                .SelectMany(c => c.CompetitionTeams)
                .SelectMany(ct => ct.Team.Players)
                .CountAsync();
        }
    }
}