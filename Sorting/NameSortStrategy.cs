using BreweryApi.Models;

namespace BreweryApi.Sorting
{
    public class NameSortStrategy : ISortStrategy
    {
        public string Name => "name";
        public IEnumerable<BreweryDto> Sort(IEnumerable<BreweryDto> breweries)
            => breweries.OrderBy(b => b.Name);
    }

}
