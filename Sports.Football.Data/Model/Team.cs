using System;
using System.Collections.Generic;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class Team : BaseModel
    {
        public Area Area { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string CrestUrl { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public int? Founded { get; set; }
        public string ClubColors { get; set; }
        public string Venue { get; set; }
        public List<Player> Players { get; set; }
        public DateTime? LastUpdated { get; set; }

        public List<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();
    }
}