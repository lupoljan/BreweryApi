using BreweryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweriesController : ControllerBase
    {
        private readonly IBreweryService _breweryService;

        public BreweriesController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBreweries(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude)
        {
            try
            {
                var breweries = await _breweryService.GetBreweriesAsync(search, sort, latitude, longitude);
                return Ok(breweries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            }
        }
    }
}
