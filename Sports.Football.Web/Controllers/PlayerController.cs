using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sports.Football.Core.Components;
using Sports.Football.Core.Model.Exceptions;

namespace Sports.Football.Web.Controllers
{
    [Route("total-players")]
    [ApiController]
    [Produces("application/json")]
    public class PlayerController : ControllerBase
    {
        private readonly IComponentsProvider _componentsProvider;

        public PlayerController(IComponentsProvider componentsProvider)
        {
            _componentsProvider = componentsProvider;
        }

        // GET: import-league/total-players/BL1
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{leagueCode}")]
        public async Task<IActionResult> TotalPlayers(string leagueCode)
        {
            try
            {
                var playerCount
                    = await _componentsProvider.FootballManager.GetTotalPlayers(leagueCode);

                return StatusCode((int)HttpStatusCode.OK, new { total = playerCount });
            }
            catch (CompetitionNotFoundException)
            {
                _componentsProvider.LogManager.Info($"League with code {leagueCode} was not found");
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                _componentsProvider.LogManager.Error("Error when getting the total players", e);
                throw;
            }
        }
    }
}