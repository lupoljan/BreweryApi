using AutoMapper;
using BreweryApi.Models;

namespace BreweryApi.Services
{
    public class BreweryApiClient : IBreweryApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ILogger<BreweryApiClient> _logger;

        public BreweryApiClient(HttpClient httpClient, IMapper mapper, ILogger<BreweryApiClient> logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BreweryModel>> FetchBreweriesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching breweries from OpenBreweryDB...");

                var response = await _httpClient.GetFromJsonAsync<List<OpenBreweryResponse>>("https://api.openbrewerydb.org/v1/breweries");

                _logger.LogInformation("Successfully retrieved {Count} breweries.", response?.Count ?? 0);

                if (response == null) return Enumerable.Empty<BreweryModel>();

                return _mapper.Map<IEnumerable<BreweryModel>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch breweries from API.");
                throw;
            }
        }
    }
}
