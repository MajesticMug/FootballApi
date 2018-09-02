using System;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;

namespace Sports.Football.Core.ServiceClient.Model.DTOs
{
    public class SquadMemberDto : BaseDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Role { get; set; }
    }
}