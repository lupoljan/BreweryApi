namespace BreweryApi.Models
{
    // DTO for client response
    public class BreweryDto
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public double? Distance { get; set; } // Optional: used when sorting by distance
    }
}
