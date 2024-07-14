using Newtonsoft.Json;

namespace visitorManagementSystem.Dto
{
    public class VisitorDto
    {
        [JsonProperty(PropertyName = "uId")]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "companyName")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "purpose")]
        public string Purpose { get; set; }
        [JsonProperty(PropertyName = "entryTime")]
        public string EntryTime { get; set; }
        [JsonProperty(PropertyName = "ExitTime")]
        public string ExitTime { get; set; }
        public bool PassStatus { get; set; }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}
