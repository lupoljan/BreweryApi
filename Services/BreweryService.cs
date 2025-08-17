using AutoMapper;
using BreweryApi.Models;
using BreweryApi.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BreweryApi.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _repository;
        private readonly BreweryApiClient _apiClient;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private const string CacheKey = "BreweryCache";
        private const int CacheDuration = 10; // minutes

        public BreweryService(IBreweryRepository repository, BreweryApiClient apiClient, IMemoryCache cache, IMapper mapper)
        {
            _repository = repository;
            _apiClient = apiClient;
            _cache = cache;
            _mapper = mapper;
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
                query = query.Where(b => b.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         b.City.Contains(search, StringComparison.OrdinalIgnoreCase));

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
            dtos = sort?.ToLower() switch
            {
                "name" => dtos.OrderBy(b => b.Name),
                "city" => dtos.OrderBy(b => b.City),
                "distance" => dtos.OrderBy(b => b.Distance),
                _ => dtos
            };

            return dtos;
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
