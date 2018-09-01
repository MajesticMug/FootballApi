using System;

namespace Sports.Football.Data.Model
{
    public class Season
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CurrentMatchDay { get; set; }
    }
}