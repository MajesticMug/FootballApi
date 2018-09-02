using System;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data.Model
{
    public class Player : BaseModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Role { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
