using System;
using System.Collections.Generic;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class Competition : BaseModel
    {
        public Area Area { get; set; }
        public string Name { get; set; }
        public string LeagueCode { get; set; }
        public Season CurrentSeason { get; set; }
        public int NumberOfAvailableSeasons { get; set; }
        public DateTime? LastUpdated { get; set; }

        public List<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();
    }
}
