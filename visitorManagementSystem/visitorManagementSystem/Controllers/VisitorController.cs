using Microsoft.AspNetCore.Mvc;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class VisitorController : Controller
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService) 
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<VisitorDto> AddVisitor(VisitorDto visitorDto)
        {
            return await _visitorService.AddVisitor(visitorDto);
        }

        [HttpGet]
        public async Task<VisitorDto> GetVisitorByUId(string UId)
        {
            return await _visitorService.GetVisitorByUId(UId);
        }
        [HttpPut]
        public async Task<VisitorDto> UpdateVisitor(VisitorDto visitorDto)
        {
            return await _visitorService.UpdateVisitor(visitorDto);
        }
        [HttpDelete]
        public async Task<string> DeleteVisitor(string UId)
        {
            return await _visitorService.DeleteVisitor(UId);
        }
    }
}
