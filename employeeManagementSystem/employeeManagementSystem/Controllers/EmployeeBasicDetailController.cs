using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;
using employeeManagementSystem.Interfaces;
using employeeManagementSystem.ServiceFilters;
using employeeManagementSystem.Services;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;


namespace employeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetailController : Controller
    {
        private readonly IEmployeeBasicDetailService _employeeBasicDetailService;
      
        public EmployeeBasicDetailController(IEmployeeBasicDetailService employeeBasicDetailService)
        {
            _employeeBasicDetailService = employeeBasicDetailService;
           
        }

        [HttpPost]
        public async Task<EmployeeBasicDetailDTO> AddEmployeeBasicDetail(EmployeeBasicDetailDTO employeeModel)
        {
            return await _employeeBasicDetailService.AddEmployeeBasicDetail(employeeModel);
        }

        [HttpGet]
        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployee()
        {
            return await _employeeBasicDetailService.GetAllEmployee();
        }

        [HttpGet]
        public async Task<EmployeeBasicDetailDTO> GetEmployeeById(string id)
        {
            return await _employeeBasicDetailService.GetEmployeeById(id);
        }

        [HttpPut]
        public async Task<EmployeeBasicDetailDTO> UpdateDetail(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            return await _employeeBasicDetailService.UpdateDetail(employeeBasicDetailDTO);
        }

        [HttpDelete]
        public async Task<string> DeleteEmployee(string Id)
        {
            return await _employeeBasicDetailService.DeleteEmployee(Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeDetailByRole(string role)
        {
            var response = await _employeeBasicDetailService.GetAllEmployeeDetailByRole(role);
            return Ok(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            return await _employeeBasicDetailService.GetAllEmployeeBasicDetailsByPagination(employeeFilterCriteria);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeByMakePostRequest(EmployeeBasicDetailDTO employeeBasicDTO)
        {
            var response = await _employeeBasicDetailService.AddEmployeeByMakePostRequest(employeeBasicDTO);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeByMakeGetRequest(string employeeId)
        {
            var response = await _employeeBasicDetailService.GetAllEmployeeByMakeGetRequest(employeeId);
            return Ok(response);
        }

    }
}

