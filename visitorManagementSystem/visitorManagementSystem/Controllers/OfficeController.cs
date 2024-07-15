using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : Controller
    {
        public readonly IOfficeService _officeService;
        public readonly IVisitorService _visitorService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<OfficeDto> AddOffice(OfficeDto officeDto)
        {
            var response = await _officeService.AddOffice(officeDto);
            return response;
        }

        [HttpGet]
        public async Task<OfficeDto> GetOfficeByUId(string UId)
        {
            var response = await _officeService.GetOfficeByUId(UId);
                return response;
        }

        [HttpPut]
        public async Task<OfficeDto> UpdateOffice(OfficeDto officeDto)
        {
            var response = await _officeService.UpdateOffice(officeDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteOffice(string UId)
        {
            var response = await _officeService.DeleteOffice(UId);
            return response;
        }
    }
}
