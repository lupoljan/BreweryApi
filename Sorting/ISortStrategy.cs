using BreweryApi.Models;

namespace BreweryApi.Sorting
{
    public interface ISortStrategy
    {
        string Name { get; }
        IEnumerable<BreweryDto> Sort(IEnumerable<BreweryDto> breweries);
    }
}
