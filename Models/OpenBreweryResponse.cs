namespace BreweryApi.Models
{
    // Matches OpenBreweryDB API schema: https://www.openbrewerydb.org/documentation/01-listbreweries
    public class OpenBreweryResponse
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string brewery_type { get; set; } = string.Empty;
        public string street { get; set; } = string.Empty;
        public string address_2 { get; set; } = string.Empty;
        public string address_3 { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string county_province { get; set; } = string.Empty;
        public string postal_code { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string longitude { get; set; } = string.Empty;
        public string latitude { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string website_url { get; set; } = string.Empty;
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
    }
}
