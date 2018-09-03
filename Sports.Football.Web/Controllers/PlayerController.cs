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
        
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{leagueCode}")]
        public async Task<IActionResult> TotalPlayers(string leagueCode)
        {
            try
            {
                var playerCount
                    = await _componentsProvider.FootballManager.GetTotalPlayers(leagueCode);

                return Ok(new {total = playerCount});
            }
            catch (CompetitionNotFoundException)
            {
                _componentsProvider.LogManager.Info($"League with code {leagueCode} was not found");
                return NotFound();
            }
            catch (Exception e)
            {
                _componentsProvider.LogManager.Error("Error when getting the total players", e);
                throw;
            }
        }
    }
}