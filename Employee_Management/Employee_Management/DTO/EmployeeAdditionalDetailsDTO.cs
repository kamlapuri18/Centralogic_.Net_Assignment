using Employee_Management.Entities;
using Newtonsoft.Json;

namespace Employee_Management.DTO
{
    public class EmployeeAdditionalDetailsDTO
    {
        [JsonProperty("employeeBasicDetailsUId")]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty("alternateEmail")]
        public string AlternateEmail { get; set; }

        [JsonProperty("alternateMobile")]
        public string AlternateMobile { get; set; }

        [JsonProperty("workInformation")]
        public WorkInfo WorkInformation { get; set; }

        [JsonProperty("personalDetails")]
        public PersonalDetails PersonalDetails { get; set; }

        [JsonProperty("identityInformation")]
        public IdentityInfo IdentityInformation { get; set; }
        public string Id { get; internal set; }
    }
}
