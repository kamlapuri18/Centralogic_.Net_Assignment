using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Service;

namespace Visitor_Security_Clearance_System.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorServicecs _visitorService;

        public VisitorController(IVisitorServicecs visitorService)
        {
            _visitorService = visitorService;
        }


        [HttpPost]

    public async Task<ActionResult<VisitorDTO>> RegisterVisitor(VisitorDTO visitorDTO)
        {

            var existingVisitor = await _visitorService.GetVisitorByEmail(visitorDTO.Email);
            if (existingVisitor != null)
            {
                return Ok("Email already exist");
            }


            var response = await _visitorService.RegisterVisitor(visitorDTO);
            return response;
        }


        [HttpGet]

        public async Task<VisitorDTO> GetVisitorByUId(string UId)
        {
            var response = await _visitorService.GetVisitorByUId(UId);
            return response;
        }

        [HttpPost]

        public async Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO)
        {
            var response = await _visitorService.UpdateVisitor(visitorDTO);
            return response;
        }

        [HttpPost]

        public async Task<string> DeleteVisitor(string UId)
        {
            var response = await _visitorService.DeleteVisitor(UId);
            return response;
        }

        [HttpGet]

        public async Task<ActionResult<List<VisitorDTO>>> GetVisitorByStatus(string status)
        {
            var visitors = await _visitorService.GetVisitorsByStatus(status);
            return Ok(visitors);
        }
    }
}