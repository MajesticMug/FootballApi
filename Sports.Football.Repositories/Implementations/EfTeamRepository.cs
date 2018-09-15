using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sports.Football.Data;
using Sports.Football.Data.Model;
using Sports.Football.Repositories.Interfaces;

namespace Sports.Football.Repositories.Implementations
{
    public class EfTeamRepository : EntityFrameworkRepository<Team>, ITeamRepository
    {
        public EfTeamRepository(FootballDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Team>> AddNonExistingTeamsAsync(IList<Team> teams)
        {
            var dbSet = DbContext.Set<Team>();

            foreach (var team in teams)
            {
                var storedTeam = await GetAll().SingleOrDefaultAsync(t => t.Name.Equals(team.Name));
                if (storedTeam == null)
                {
                    dbSet.Add(team);
                }
                else
                {
                    team.Id = storedTeam.Id;
                }
            }

            await DbContext.SaveChangesAsync();

            return teams;
        }
    }
}