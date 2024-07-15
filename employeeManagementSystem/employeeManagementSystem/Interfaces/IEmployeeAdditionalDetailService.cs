using employeeManagementSystem.DTO;

namespace employeeManagementSystem.Interfaces
{
    public interface IEmployeeAdditionalDetailService
    {
        Task<EmployeeAdditionalDetailDTO> AddAdditionalDetail(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO);
        Task<string> DeleteEmployee(string employeeBasicDetailsUId);
        Task<List<EmployeeAdditionalDetailDTO>> GetAllData();
        Task<EmployeeAdditionalDetailDTO> GetEmployeeAdditionalByUid(string employeeBasicDetailsUId);
        Task<EmployeeAdditionalDetailDTO> UpdateAdditionalByUId(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO);
    }
}
