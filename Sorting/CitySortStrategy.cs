using BreweryApi.Models;

namespace BreweryApi.Sorting
{
    public class CitySortStrategy : ISortStrategy
    {
        public string Name => "city";

        public IEnumerable<BreweryDto> Sort(IEnumerable<BreweryDto> breweries)
            => breweries.OrderBy(b => b.City);
    }
}
