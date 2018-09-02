using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sports.Football.Data;
using Sports.Football.Data.Model;
using Sports.Football.Repositories.Interfaces;

namespace Sports.Football.Repositories.Implementations
{
    public class EfCompetitionRepository : EntityFrameworkRepository<Competition>, ICompetitionRepository
    {
        public EfCompetitionRepository(FootballDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddTeamsForCompetitionAsync(Competition competition, IEnumerable<Team> teams)
        {
            var competitionTeams
                = teams.Select(
                    team => new CompetitionTeam
                    {
                        CompetitionId = competition.Id, TeamId = team.Id
                    })
                    .ToList();

            await DbContext.Set<CompetitionTeam>().AddRangeAsync(competitionTeams);
        }

        public async Task<Competition> GetByLeagueCodeAsync(string leagueCode)
        {
            return await GetAll()
                .SingleOrDefaultAsync(
                    c => c.LeagueCode.Equals(leagueCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}