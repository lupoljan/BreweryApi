using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public class InMemoryBreweryRepository : IBreweryRepository
    {
        private IEnumerable<BreweryModel> _breweries = new List<BreweryModel>();

        public Task<IEnumerable<BreweryModel>> GetAllAsync() => Task.FromResult(_breweries);

        public Task SaveAllAsync(IEnumerable<BreweryModel> breweries)
        {
            _breweries = breweries;
            return Task.CompletedTask;
        }
    }
}
