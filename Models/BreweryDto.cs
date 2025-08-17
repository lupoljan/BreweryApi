namespace BreweryApi.Models
{
    // DTO for client response
    public class BreweryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public double? Distance { get; set; } // optional (calculated only if lattitude/longitude is provided)
    }
}
