namespace BreweryApi.Models
{
    public class BreweryModel
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
