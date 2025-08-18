using AutoMapper;
using BreweryApi.Models;
using BreweryApi.Repositories;
using BreweryApi.Sorting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BreweryApi.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _repository;
        private readonly IBreweryApiClient _apiClient;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly ILogger<BreweryService> _logger;
        private const string CacheKey = "BreweryCache";
        private const int CacheDuration = 10; // minutes

        public BreweryService(
            IBreweryRepository repository,
            IBreweryApiClient apiClient, 
            IMemoryCache cache,
            IMapper mapper, 
            ILogger<BreweryService> logger)
        {
            _repository = repository;
            _apiClient = apiClient;
            _cache = cache;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BreweryDto>> GetBreweriesAsync(string? search, string? sort, double? userLat, double? userLon)
        {


            var breweries = await _cache.GetOrCreateAsync(CacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheDuration);
                var freshData = await _apiClient.FetchBreweriesAsync();
                await _repository.SaveAllAsync(freshData);
                return freshData;
            });

            var query = breweries.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                _logger.LogInformation("Filtering breweries by search term: {search}", search);
                query = query.Where(b => b.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                         b.City.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // Map to DTOs
            var dtos = _mapper.Map<IEnumerable<BreweryDto>>(query);

            // Add calculated distance if coordinates provided
            if (userLat != null && userLon != null)
            {
                foreach (var (dto, b) in dtos.Zip(query))
                {
                    if (b.Latitude != null && b.Longitude != null)
                    {
                        dto.Distance = CalculateDistance(userLat.Value, userLon.Value, b.Latitude.Value, b.Longitude.Value);
                    }
                }
            }

            // Sorting
            var sortStrategy = SortStrategyFactory.GetStrategy(sort);
            _logger.LogInformation("Applying sorting strategy: {sortStrategy.Name}", sortStrategy.Name);
            return sortStrategy.Sort(dtos);
        }

        public async Task<List<string>> GetAutocompleteAsync(string query, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<string>();

            _logger.LogInformation("Autocomplete requested for query: {query}", query);

            var breweries = await GetBreweriesAsync(null, null, null, null);

            var suggestions = breweries
                .Where(b => b.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(b => b.Name)
                .Distinct()
                .OrderBy(name => name)
                .Take(limit)
                .ToList();

            _logger.LogInformation("Returning {count} autocomplete suggestions.", suggestions.Count);
            return suggestions;
        }


        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // km
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            return R * 2 * Math.Asin(Math.Sqrt(a));
        }
    }
}
