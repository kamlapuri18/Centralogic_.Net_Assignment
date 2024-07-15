using Newtonsoft.Json;

namespace employeeManagementSystem.Entities
{
    public class Address
    {
        [JsonProperty(PropertyName = "houseNumber", NullValueHandling = NullValueHandling.Ignore)]

        public string HouseNumber { get; set; }

        [JsonProperty(PropertyName = "streetName", NullValueHandling = NullValueHandling.Ignore)]

        public string StreetName { get; set; }

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]

        public string City { get; set; }

        [JsonProperty(PropertyName = "state", NullValueHandling = NullValueHandling.Ignore)]

        public string State { get; set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]

        public string Country { get; set; }

        [JsonProperty(PropertyName = "postalCodes", NullValueHandling = NullValueHandling.Ignore)]

        public string PostalCodes { get; set; }
    }
}
