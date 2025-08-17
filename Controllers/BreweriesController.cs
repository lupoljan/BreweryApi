using BreweryApi.Models;
using BreweryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweriesController : ControllerBase
    {
        private readonly IBreweryService _breweryService;
        private readonly ILogger<BreweriesController> _logger;

        public BreweriesController(IBreweryService breweryService, ILogger<BreweriesController> logger)
        {
            _breweryService = breweryService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of breweries with optional search, sorting, and distance calculation.
        /// </summary>
        /// <param name="search">Search term for brewery name or city</param>
        /// <param name="sort">Sort by "name", "city", or "distance"</param>
        /// <param name="latitude">Latitude for distance sorting</param>
        /// <param name="longitude">Longitude for distance sorting</param>
        /// <returns>List of breweries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BreweryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBreweries(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude)
        {
            _logger.LogInformation("Fetching breweries. Search={search}, Sort={sort}, Lat={latitude}, Lon={longitude}",
                search, sort, latitude, longitude);

            var breweries = await _breweryService.GetBreweriesAsync(search, sort, latitude, longitude);

            return Ok(breweries);
        }
    }
}
