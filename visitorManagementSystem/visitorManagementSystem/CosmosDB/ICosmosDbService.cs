using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;

namespace visitorManagementSystem.CosmosDB
{
    public interface ICosmosDbService
    {
        Task<ManagerEntity> AddManager(ManagerEntity manager);
        Task<OfficeEntity> AddOffice(OfficeEntity office);
        Task<SecurityEntity> AddSecurity(SecurityEntity security);
        Task<ManagerEntity> GetManagerByUId(string uId);
        Task<OfficeEntity> GetOfficeByUId(string uId);
        Task<SecurityEntity> GetSecurityByUId(string uId);
        Task<VisitorEntity> AddVisitor(VisitorEntity visitor);
        Task ReplaceAsync(dynamic entity);
        Task<VisitorEntity> GetVisitorByUId(string uId);
    }
}

