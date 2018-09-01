using System.Collections.Generic;
using Sports.Football.Core.ServiceClient.Model.Attributes;
using Sports.Football.Data.Model;

namespace Sports.Football.Core.ServiceClient.Model
{
    [ServiceClientResource(Uri = "competitions")]
    public class CompetitionsRootObject
    {
        public int Count { get; set; }
        public IList<Filter> Filters { get; set; }
        public IList<League> Competitions { get; set; }
    }
}