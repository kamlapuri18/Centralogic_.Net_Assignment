using Employee_Management.DTO;
using Employee_Management.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class EmployeeBaseDetailsController : Controller
    {
        public readonly IEmployeeBasicDetailsService _employeeBasicDetailsService;

        public EmployeeBaseDetailsController(IEmployeeBasicDetailsService employeeBasicDetailsService)
        {
            _employeeBasicDetailsService = employeeBasicDetailsService;
        }

        [HttpPost]
        public async Task<EmployeeBasicDetailDTO> AddEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            var response =  await _employeeBasicDetailsService.AddEmployee(employeeBasicDetailDTO);
            return response;
        }

        [HttpGet]
        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployee()
        {
            var response = await _employeeBasicDetailsService.GetAllEmployee();
            return response;
        }
       [HttpGet]
        public async Task<EmployeeBasicDetailDTO> GetEmployeeByUId(string employeeID)
        {
            var response = await _employeeBasicDetailsService.GetAllEmployeeByID(employeeID);
            return response;
        }

       [HttpPost]
        public async Task<EmployeeBasicDetailDTO> UpdateEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            var response = await _employeeBasicDetailsService.UpdateEmployee(employeeBasicDetailDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteEmployee(string employeeID)
        {
            var response = await _employeeBasicDetailsService.DeleteEmployee(employeeID);
            return response;
        }
    }
}
