namespace BreweryApi.Models
{
    // Matches OpenBreweryDB API schema: https://www.openbrewerydb.org/documentation/01-listbreweries
    public class OpenBreweryResponse
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string brewery_type { get; set; } = string.Empty;
        public string address_1 { get; set; } = string.Empty;
        public string address_2 { get; set; } = string.Empty;
        public string address_3 { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string state_province { get; set; } = string.Empty;
        public string postal_code { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public double? longitude { get; set; }
        public double? latitude { get; set; } 
        public string phone { get; set; } = string.Empty;
        public string website_url { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string street { get; set; } = string.Empty;
    }
}
