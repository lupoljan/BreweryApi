using BreweryApi.Models;

namespace BreweryApi.Services
{
    public interface IBreweryService
    {
        Task<IEnumerable<BreweryDto>> GetBreweriesAsync(string? search, string? sort, double? userLat, double? userLon);
        Task<List<string>> GetAutocompleteAsync(string query, int limit = 10);
    }
}
