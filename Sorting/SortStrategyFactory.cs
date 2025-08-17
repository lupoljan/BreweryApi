namespace BreweryApi.Sorting
{
    public static class SortStrategyFactory
    {
        private static readonly Dictionary<string, ISortStrategy> Strategies =
            new Dictionary<string, ISortStrategy>(StringComparer.OrdinalIgnoreCase)
            {
                ["name"] = new NameSortStrategy(),
                ["city"] = new CitySortStrategy(),
                ["distance"] = new DistanceSortStrategy()
            };

        public static ISortStrategy GetStrategy(string sortBy)
        {
            return sortBy != null && Strategies.TryGetValue(sortBy, out var strategy)
                ? strategy
                : new DefaultSortStrategy();
        }
    }
}
