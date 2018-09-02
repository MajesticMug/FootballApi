using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class CompetitionTeam : Entity
    {
        public int CompetitionId { get; set; }
        public int TeamId { get; set; }

        public Competition Competition { get; set; }
        public Team Team { get; set; }
    }
}