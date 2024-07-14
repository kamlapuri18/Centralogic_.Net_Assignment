using visitorManagementSystem.Dto;

namespace visitorManagementSystem.Interfaces
{
    public interface ISecurityService
    {
        Task<SecurityDto> AddSecurity(SecurityDto securityDto);
        Task<string> DeleteSecurity(string uId);
        Task<SecurityDto> GetSecurityByUId(string uId);
        Task<SecurityDto> UpdateSecurity(SecurityDto securityDto);
    }
}
