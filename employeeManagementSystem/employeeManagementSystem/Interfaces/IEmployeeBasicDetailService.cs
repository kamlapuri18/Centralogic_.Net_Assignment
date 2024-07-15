using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;

namespace employeeManagementSystem.Interfaces
{
    public interface IEmployeeBasicDetailService
    {
        Task<EmployeeBasicDetailDTO> AddEmployeeBasicDetail(EmployeeBasicDetailDTO employeeModel);
        Task<EmployeeBasicDetailDTO> AddEmployeeByMakePostRequest(EmployeeBasicDetailDTO employeeBasicDTO);
        Task<string> DeleteEmployee(string id);
        Task<List<EmployeeBasicDetailDTO>> GetAllEmployee();
        Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria);
        Task<List<EmployeeBasicDetailDTO>> GetAllEmployeeByMakeGetRequest(string employeeId);
        Task<List<EmployeeBasicDetailDTO>> GetAllEmployeeDetailByRole(string role);
        Task<EmployeeBasicDetailDTO> GetEmployeeById(string id);
        Task<EmployeeBasicDetailDTO> UpdateDetail(EmployeeBasicDetailDTO employeeBasicDetailDTO);
    }
}
