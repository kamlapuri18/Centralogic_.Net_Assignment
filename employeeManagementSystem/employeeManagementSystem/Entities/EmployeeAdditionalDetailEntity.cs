using employeeManagementSystem.Common;
using Newtonsoft.Json;

namespace employeeManagementSystem.Entities
{
    public class EmployeeAdditionalDetailEntity : BaseEntity
    {
        [JsonProperty(PropertyName = "employeeBasicDetailsUId", NullValueHandling = NullValueHandling.Ignore)]

        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]

        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]

        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]

        public WorkInformation WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]

        public PersonalDetails PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]

        public IdentityInformation IdentityInformation { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]

        public DateTime DateOfBirth { get; internal set; }

        [JsonProperty(PropertyName = "dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]

        public DateTime DateOfJoining { get; internal set; }
        public string DesignationName { get; internal set; }
    }
}
