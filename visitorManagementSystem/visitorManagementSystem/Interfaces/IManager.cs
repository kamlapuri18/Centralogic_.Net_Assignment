using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;

namespace visitorManagementSystem.Interfaces
{
    public interface IManager
    {
        Task<ManagerDto> AddManager(ManagerDto managerDto);
        Task<string> DeleteManager(string uId);
        Task<ManagerDto> GetManagerByUId(string uId);
        Task<ManagerDto> UpdateManager( ManagerDto managerDto);
    }
}
