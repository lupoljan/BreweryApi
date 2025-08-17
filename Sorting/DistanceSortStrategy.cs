using BreweryApi.Models;

namespace BreweryApi.Sorting
{
    public class DistanceSortStrategy : ISortStrategy
    {
        public string Name => "distance";
        public IEnumerable<BreweryDto> Sort(IEnumerable<BreweryDto> breweries)
            => breweries.OrderBy(b => b.Distance ?? double.MaxValue);
    }
}
