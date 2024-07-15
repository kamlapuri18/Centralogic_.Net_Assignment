using Microsoft.AspNetCore.Mvc;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IVisitorService _visitorService;

        public SecurityController(ISecurityService securityService,IVisitorService visitorService )
        {
            _securityService = securityService;
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<SecurityDto> AddSecurity(SecurityDto securityDto)
        {
            var response = await _securityService.AddSecurity(securityDto);
            return response;
        }

        [HttpGet]
        public async Task<SecurityDto> GetSecurityByUId(string UId)
        {
            var response = await _securityService.GetSecurityByUId(UId);
            return response;
        }

        [HttpPut]
        public async Task<SecurityDto> UpdateSecurity(SecurityDto securityDto)
        {
            var response = await _securityService.UpdateSecurity(securityDto);
            return response;
        }
        
        [HttpDelete]
        public async Task<string> DeleteSecurity(string UId)
        {
            var response = await _securityService.DeleteSecurity(UId);
            return response;
        }

        [HttpGet]
        public async Task<VisitorDto> GetVisitorByUId(string UId)
        {
            return await _visitorService.GetVisitorByUId(UId);
        }
        
    }
}