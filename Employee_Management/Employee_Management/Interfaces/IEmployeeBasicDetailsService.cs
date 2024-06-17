using Employee_Management.DTO;

namespace Employee_Management.Interfaces
{
    public interface IEmployeeBasicDetailsService
    {
        Task<EmployeeBasicDetailDTO> AddEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO);
        Task<string> DeleteEmployee(string employeeID);
        Task<List<EmployeeBasicDetailDTO>> GetAllEmployee();
        Task<EmployeeBasicDetailDTO> GetAllEmployeeByID(string employeeID);
        Task<EmployeeBasicDetailDTO> UpdateEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO);
       
    }
}
