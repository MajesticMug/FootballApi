using System;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class Season : BaseModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CurrentMatchDay { get; set; }
    }
}