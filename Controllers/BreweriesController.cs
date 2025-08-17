using BreweryApi.Models;
using BreweryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        /// <summary>
        /// Get brewery name suggestions for autocomplete functionality
        /// </summary>
        /// <remarks>
        /// Returns matching brewery names based on partial input query.
        /// 
        /// Example:
        /// GET /api/breweries/autocomplete?query=brew&limit=10
        /// 
        /// Sample response:
        /// [
        ///  "(405) Brewing Co",
        ///  "(512) Brewing Co",
        ///  "1 of Us Brewing Company",
        ///  "10 Barrel Brewing Co",
        ///  "10 Barrel Brewing Co - Bend Pub",
        ///  "10 Barrel Brewing Co - Boise",
        ///  "10 Barrel Brewing Co - Denver",
        ///  "10 Torr Distilling and Brewing",
        ///  "10-56 Brewing Company",
        ///  "101 North Brewing Company"
        /// ]
        /// </remarks>
        /// <param name="query">Partial name query for brewery suggestions</param>
        /// <param name="limit">Maximum number of suggestions to return (default: 10)</param>
        /// <returns>List of brewery name suggestions</returns>
        [HttpGet("autocomplete")]
        public async Task<IActionResult> GetAutocomplete([FromQuery] string query, [FromQuery] int limit = 10)
        {
            var suggestions = await _breweryService.GetAutocompleteAsync(query, limit);
            return Ok(suggestions);
        }
    }
}
