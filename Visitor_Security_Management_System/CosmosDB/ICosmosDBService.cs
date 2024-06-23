

using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;

namespace Visitor_Security_Clearance_System.CosmosDB
{
    public interface ICosmosDBService
    {
        Task<VisitorEntity> RegisterVisitor(VisitorEntity visitorEntity);
        Task<VisitorEntity> GetVisitorByEmail(string email);
        Task<VisitorEntity> GetVisitorByUId(string uId);
        Task ReplaceAsync(dynamic visitor);
        Task<ManagerEntity> RegisterManager(ManagerEntity managerEntity);
        Task<ManagerEntity> GetManagerByEmail(string email);
        Task<ManagerEntity> GetManagerByUId(string UId);
        Task<OfficeEntity> RegisterOffice(OfficeEntity officeEntity);
        Task<OfficeEntity> GetOfficeByUId(string UId);
        Task<SecurityEntity> RegisterSecurity(SecurityEntity securityEntity);
        Task<SecurityEntity> GetSecurityByEmail(string email);
        Task<SecurityEntity> GetSecurityByUId(string uId);
        Task<UserLoginDTO> GetUserByEmail(string email);
        Task<PassEntity> CreatePass(PassEntity passEntity);
        Task<PassEntity> GetPassByVisitorId(string visitorId);
        Task<List<VisitorEntity>> GetVisitorsByStatus(string status);
        Task<PassEntity> GetPassById(string uId);
        Task<PassEntity> UpdatePass(PassEntity passEntity);
    }
}
