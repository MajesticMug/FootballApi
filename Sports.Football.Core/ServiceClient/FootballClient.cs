using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sports.Football.Core.ServiceClient.Mappers;
using Sports.Football.Core.ServiceClient.Model;
using Sports.Football.Core.ServiceClient.Model.DTOs;
using Sports.Football.Data.Model;
using Competition = Sports.Football.Data.Model.Competition;

namespace Sports.Football.Core.ServiceClient
{
    public class FootballClient : IFootballClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly IModelMapper _mapper;

        public FootballClient(IServiceClient serviceClient, IModelMapper mapper)
        {
            _serviceClient = serviceClient;
            _mapper = mapper;
        }

        public async Task<Competition> GetCompetitionByLeagueCodeAsync(string leagueCode)
        {
            var root = await _serviceClient.GetRootAsync<CompetitionsRootObject>("competitions");

            var competition = 
                root
                    .Competitions
                    .SingleOrDefault(c => 
                        leagueCode.Equals(c.Code, StringComparison.InvariantCultureIgnoreCase));

            return
                competition != null ? 
                    _mapper.Map<CompetitionDto, Competition>(competition) : null;
        }

        public async Task<IEnumerable<Team>> GetTeamsByCompetition(Competition competition)
        {
            var root = await _serviceClient.GetRootAsync<TeamsRootObject>($"competitions/{competition.ExternalId}/teams");
            
            return root.Teams.Select(t => _mapper.Map<TeamDto, Team>(t));
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamAsync(Team team)
        {
            var players
                = (await _serviceClient.GetRootAsync<TeamDto>($"teams/{team.ExternalId}"))
                .Squad
                .Select(s => _mapper.Map<SquadMemberDto, Player>(s)).ToList();

            return players;
        }
    }
}