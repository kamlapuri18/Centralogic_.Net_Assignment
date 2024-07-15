using visitorManagementSystem.Dto;

namespace visitorManagementSystem.Interfaces
{
    public interface IVisitorService
    {
        Task<VisitorDto> AddVisitor(VisitorDto visitorDto);
        Task<string> DeleteVisitor(string uId);
        Task<VisitorDto> GetVisitorByUId(string uId);
        Task<VisitorDto> UpdateVisitor(VisitorDto visitorDto);
        Task<List<VisitorDto>> GetAllVisitor();
        Task<List<VisitorDto>> GetVisitorByStatus(string passStatus);
    }
}
