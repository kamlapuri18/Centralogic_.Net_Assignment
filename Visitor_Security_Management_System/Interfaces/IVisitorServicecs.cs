using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IVisitorServicecs
    {


        Task<VisitorDTO> GetVisitorByEmail(string Email);
        Task<VisitorDTO> RegisterVisitor(VisitorDTO visitorDTO);
        Task<VisitorDTO> GetVisitorByUId(string UId);
        Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO);
        Task<string> DeleteVisitor(string uId);
        Task<List<VisitorDTO>> GetVisitorsByStatus(string status);
    }
}
