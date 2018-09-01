using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class League : BaseModel
    {
        public Area Area { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Season CurrentSeason { get; set; }
        public int NumberOfAvailableSeasons { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
