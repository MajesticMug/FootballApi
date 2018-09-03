using System;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;

namespace Sports.Football.Core.ServiceClient.Model.DTOs
{
    public class SeasonDto : BaseDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CurrentMatchDay { get; set; }
    }
}