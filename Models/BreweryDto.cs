namespace BreweryApi.Models
{
    // DTO for client response
    public class BreweryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BreweryType { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Address3 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StateProvince { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string State{ get; set; } = string.Empty;
        public double? Distance { get; set; } // optional (calculated only if lattitude/longitude is provided)
    }
}
