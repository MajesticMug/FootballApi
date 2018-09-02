using System.Collections.Generic;
using Sports.Football.Core.ServiceClient.Model.DTOs;

namespace Sports.Football.Core.ServiceClient.Model
{
    public class TeamsRootObject
    {
        public int Count { get; set; }
        public CompetitionDto Competition { get; set; }
        public SeasonDto Season { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}