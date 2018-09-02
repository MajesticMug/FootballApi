using Microsoft.EntityFrameworkCore;
using Sports.Football.Data.Model;

namespace Sports.Football.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<CompetitionTeam> CompetitionTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Competition>()
                .HasOne<Area>();

            modelBuilder
                .Entity<Competition>()
                .HasOne<Season>();

            modelBuilder
                .Entity<CompetitionTeam>()
                .HasKey(ct => new {ct.TeamId, ct.CompetitionId});

            modelBuilder
                .Entity<CompetitionTeam>()
                .HasOne(ct => ct.Competition)
                .WithMany(c => c.CompetitionTeams)
                .HasForeignKey(ct => ct.CompetitionId);

            modelBuilder
                .Entity<CompetitionTeam>()
                .HasOne(ct => ct.Team)
                .WithMany(t => t.CompetitionTeams)
                .HasForeignKey(ct => ct.TeamId);

            modelBuilder
                .Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
