using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public interface IBreweryRepository
    {
        Task<IEnumerable<BreweryModel>> GetAllAsync();
        Task SaveAllAsync(IEnumerable<BreweryModel> breweries);
    }
}
