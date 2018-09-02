using System;
using System.Collections.Generic;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;

namespace Sports.Football.Core.ServiceClient.Model.DTOs
{
    public class TeamDto : BaseDto
    {
        public AreaDto Area { get; set; }
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
        public List<SquadMemberDto> Squad { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}