using Microsoft.AspNetCore.Mvc;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManager _manager;
        private readonly IOfficeService _officeService;
        private readonly ISecurityService _securityService;
        private readonly IVisitorService _visitorService;

        public ManagerController(IManager manager, IOfficeService officeService, ISecurityService securityService, IVisitorService visitorService)
        {
            _manager = manager;
            _officeService = officeService;
            _securityService = securityService;
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<ManagerDto> AddManager(ManagerDto managerDto)
        {
            return await _manager.AddManager(managerDto);
        }

        [HttpGet]
        public async Task<ManagerDto> GetManagerByUId(string uId)
        {
            return await _manager.GetManagerByUId(uId);
        }

        [HttpPut]

        public async Task<ManagerDto> UpdateManager( ManagerDto managerDto)
        {
            return await _manager.UpdateManager(managerDto);
        }

        [HttpDelete]
        public async Task<string> DeleteManager(string UId) 
        {
            return await _manager.DeleteManager(UId);
        }

        [HttpPost]
        public async Task<OfficeDto> AddOffice(OfficeDto officeDto)
        {
            return await _officeService.AddOffice(officeDto);
        }

        [HttpPost]
        public async Task<SecurityDto> AddSecurity(SecurityDto securityDto)
        {
            return await _securityService.AddSecurity(securityDto);
        }

    }
}
