using Newtonsoft.Json;

namespace Employee_Management.Entities
{
    public class IdentityInfo
    {
        [JsonProperty(PropertyName = "pan", NullValueHandling = NullValueHandling.Ignore)]
        public string PAN { get; set; }

        [JsonProperty(PropertyName = "aadhar", NullValueHandling = NullValueHandling.Ignore)]
        public string Aadhar { get; set; }

        [JsonProperty(PropertyName = "propertyName", NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }

        [JsonProperty(PropertyName = "passportNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }

        [JsonProperty(PropertyName = "pfNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PFNumber { get; set; }
    }
}
