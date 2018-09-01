using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Sports.Football.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            var json = "{  \"competition\":{      \"id\": 2144,      \"area\": {        \"id\": 2000,        \"name\": \"Afghanistan\"      },      \"name\": \"Playoffs 2/3\",      \"code\": null,      \"plan\": \"TIER_FOUR\",      \"currentSeason\": {        \"id\": 212,        \"startDate\": \"2018-05-22\",        \"endDate\": \"2018-05-27\",        \"currentMatchday\": null      },      \"numberOfAvailableSeasons\": 1,      \"lastUpdated\": \"2018-07-13T13:34:06Z\"    }}";
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
