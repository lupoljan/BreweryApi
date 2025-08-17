using BreweryApi.Models;

namespace BreweryApi.Services
{
    public class BreweryApiClient
    {
        private readonly HttpClient _httpClient;

        public BreweryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BreweryModel>> FetchBreweriesAsync()
        {
            var breweries = await _httpClient.GetFromJsonAsync<List<dynamic>>("https://api.openbrewerydb.org/v1/breweries");

            if (breweries == null) return Enumerable.Empty<BreweryModel>();

            return breweries.Select(b => new BreweryModel
            {
                Name = b.name,
                City = b.city,
                Phone = b.phone,
                Longitude = double.TryParse((string)b.longitude, out var lon) ? lon : null,
                Latitude = double.TryParse((string)b.latitude, out var lat) ? lat : null
            });
        }
    }
}
