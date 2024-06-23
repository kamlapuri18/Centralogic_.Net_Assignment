using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Service;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ActionResult<ManagerDTO>> RegisterManager(ManagerDTO managerDTO)
        {
            var response = await _managerService.RegisterManager(managerDTO);
            return response;
        }

        [HttpGet]
        public async Task<ManagerDTO> GetManagerByUId(string UId)
        {
            var response = await _managerService.GetManagerByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO)
        {
            var response = await _managerService.UpdateManager(managerDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteManager(string UId)
        {
            var response = await _managerService.DeleteManager(UId);
            return response;
        }
    }
}
