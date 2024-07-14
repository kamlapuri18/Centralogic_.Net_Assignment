using AutoMapper;
using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;
using employeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace employeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeAdditionalDetailController : Controller
    {
        private readonly IEmployeeAdditionalDetailService _employeeAdditionalDetailService;
        private readonly IMapper _mapper;

        public EmployeeAdditionalDetailController(IEmployeeAdditionalDetailService employeeAdditionalDetailService, IMapper mapper)
        {
            _employeeAdditionalDetailService = employeeAdditionalDetailService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<EmployeeAdditionalDetailDTO> AddAdditionalDetail(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO)
        {
            return await _employeeAdditionalDetailService.AddAdditionalDetail(employeeAdditionalDetailDTO);
        }

        [HttpGet]
        public async Task<List<EmployeeAdditionalDetailDTO>> GetAllData()
        {
            return await _employeeAdditionalDetailService.GetAllData();
        }

        [HttpGet]
        public async Task<EmployeeAdditionalDetailDTO> GetEmployeeAdditionalByUid(string employeeBasicDetailsUId)
        {
            return await _employeeAdditionalDetailService.GetEmployeeAdditionalByUid(employeeBasicDetailsUId);
        }

        [HttpPut]
        public async Task<EmployeeAdditionalDetailDTO> UpdateAdditionalByUId(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO)
        {
            return await _employeeAdditionalDetailService.UpdateAdditionalByUId(employeeAdditionalDetailDTO);
        }
        [HttpDelete]
        public async Task<string> DeleteDetail(string employeeBasicDetailsUId)
        {
            return await _employeeAdditionalDetailService.DeleteEmployee(employeeBasicDetailsUId);
        }
    }
}
