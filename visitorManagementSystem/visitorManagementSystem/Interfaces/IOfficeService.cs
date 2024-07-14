using visitorManagementSystem.Dto;

namespace visitorManagementSystem.Interfaces
{
    public interface IOfficeService
    {
        Task<OfficeDto> AddOffice(OfficeDto officeDto);
        Task<string> DeleteOffice(string uId);
        Task<OfficeDto> GetOfficeByUId(string uId);
        Task<OfficeDto> UpdateOffice(OfficeDto officeDto);
    }
}
