using System;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;

namespace Sports.Football.Core.ServiceClient.Model.DTOs
{
    public class CompetitionDto : BaseDto
    {
        public AreaDto Area { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public SeasonDto CurrentSeason { get; set; }
        public int NumberOfAvailableSeasons { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}