using BreweryApi.Models;

namespace BreweryApi.Sorting
{
    public class DefaultSortStrategy : ISortStrategy
    {
        public string Name => "default";

        public IEnumerable<BreweryDto> Sort(IEnumerable<BreweryDto> breweries)
            => breweries;
    }
}
