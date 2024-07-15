using AutoMapper;
using visitorManagementSystem.CosmosDB;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public OfficeService(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }

        public async Task<OfficeDto> AddOffice(OfficeDto officeDto)
        {
            var office = _mapper.Map<OfficeEntity>(officeDto);
            office.Initialize(true, "office", "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDbService.AddOffice(office);
            return _mapper.Map<OfficeDto>(response);
        }

        public async Task<OfficeDto> GetOfficeByUId(string uId)
        {
            var office = await _cosmosDbService.GetOfficeByUId(uId);
            if (office == null)
            {
                throw new Exception("Office not found");
            }
            var officeDto = _mapper.Map<OfficeDto>(office);
            return officeDto;
        }

        public async Task<OfficeDto> UpdateOffice(OfficeDto officeDto)
        {
            var existing = await _cosmosDbService.GetOfficeByUId(officeDto.UId);
            if (existing == null)
            {
                throw new Exception("Office not found");
            }
            existing.Active = false;
            await _cosmosDbService.ReplaceAsync(existing);
            existing.Initialize(false, "office", "Sudhanshu", "Sudhanshu");
            existing.Name = officeDto.Name;
            existing.Phone = officeDto.Phone;
            existing.Email = officeDto.Email;
            existing.Role = officeDto.Role;

            var response = await _cosmosDbService.AddOffice(existing);
            var responseModel = new OfficeDto
            {
                UId = officeDto.UId,
                Name = officeDto.Name,
                Phone = officeDto.Phone,
                Email = officeDto.Email,
                Role = officeDto.Role,
            };
            return responseModel;
        }

        public async Task<string> DeleteOffice(string uId)
        {

            var office = await _cosmosDbService.GetOfficeByUId(uId);
            if (office == null)
            {
                throw new Exception("Office not found");
            }
            office.Active = false;
            office.Archived = true;
            await _cosmosDbService.ReplaceAsync(office);
            office.Initialize(false, "office", "Sudhhanshu", "Sudhanshu");
            office.Active = false;
            return "Office Deleted";
        }

    }
}