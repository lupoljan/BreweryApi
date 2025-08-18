using BreweryApi.Models;

namespace BreweryApi.Services
{
    public interface IBreweryApiClient
    {
        Task<IEnumerable<BreweryModel>> FetchBreweriesAsync();
    }
}
