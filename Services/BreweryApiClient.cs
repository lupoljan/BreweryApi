using AutoMapper;
using BreweryApi.Models;

namespace BreweryApi.Services
{
    public class BreweryApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public BreweryApiClient(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BreweryModel>> FetchBreweriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OpenBreweryResponse>>("https://api.openbrewerydb.org/v1/breweries");

            if (response == null) return Enumerable.Empty<BreweryModel>();

            return _mapper.Map<IEnumerable<BreweryModel>>(response);
        }
    }
}
