using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sports.Football.Core.Components;
using Sports.Football.Core.Model.Exceptions;
using Sports.Football.Core.ServiceClient.Model.Exceptions;

namespace Sports.Football.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class FootballController : ControllerBase
    {
        private readonly IComponentsProvider _componentsProvider;

        public FootballController(IComponentsProvider componentsProvider)
        {
            _componentsProvider = componentsProvider;
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(504)]
        // GET: import-league/BL1
        [HttpGet("import-players/{leagueCode}")]

        public async Task<IActionResult> Import(string leagueCode)
        {
            try
            {
                _componentsProvider.LogManager.Info($"Importing league from code {leagueCode}");

                await _componentsProvider.FootballManager.ImportLeagueFromApiAsync(leagueCode);

                _componentsProvider.LogManager.Info($"League successfully imported: {leagueCode}");

                return Created("Leagues", "{ \"Message\": \"Successfully imported\" }");
            }
            catch (CompetitionNotFoundException)
            {
                _componentsProvider.LogManager.Warn($"Requested league with code '{leagueCode}' was not found");
                return StatusCode((int) HttpStatusCode.NotFound, "{ \"Message\": \"Not found\" }");
            }
            catch (CompetitionAlreadyImportedException)
            {
                _componentsProvider.LogManager.Info($"League with code '{leagueCode}' was already imported");
                return StatusCode((int) HttpStatusCode.Conflict, "{ \"Message\": \"League already imported\" }");
            }
            catch (RequestNumberLimitExceededException e)
            {
                _componentsProvider.LogManager.Error("Api request limit exceeded", e);
                return StatusCode(
                    (int)HttpStatusCode.GatewayTimeout,
                    "{ \"Message\": \"Server error\" }");

            }
            catch (Exception e)
            {
                _componentsProvider.LogManager.Error("Error importing the league", e);
                return StatusCode(
                    (int) HttpStatusCode.GatewayTimeout, 
                    "{ \"Message\": \"Server error\" }");
            }
        }


        // GET: import-league/total-players/BL1
        [HttpGet("total-players/{leagueCode}")]
        public async Task<IActionResult> TotalPlayers(string leagueCode)
        {
            try
            {
                var playerCount
                    = await _componentsProvider.FootballManager.GetTotalPlayers(leagueCode);

                return StatusCode((int) HttpStatusCode.OK, $"{{ \"total\": {playerCount} }}");
            }
            catch (CompetitionNotFoundException)
            {
                _componentsProvider.LogManager.Info($"League with code {leagueCode} was not found");
                return StatusCode((int) HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                _componentsProvider.LogManager.Error("Error when getting the total players", e);
                throw;
            }
        }
    }
}