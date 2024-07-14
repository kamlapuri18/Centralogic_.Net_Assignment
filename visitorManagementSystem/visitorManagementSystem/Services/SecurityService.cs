using AutoMapper;
using visitorManagementSystem.CosmosDB;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Services
{
    public class SecurityService : ISecurityService

    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;
        public SecurityService(ICosmosDbService cosmosDbService, IMapper mapper ) 
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }

        public async Task<SecurityDto> AddSecurity(SecurityDto securityDto)
        {
             var security = _mapper.Map<SecurityEntity>(securityDto);
            security.Initialize(true, "security", "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDbService.AddSecurity(security);
            return _mapper.Map<SecurityDto>(response);  
        }

        

        public async Task<SecurityDto> GetSecurityByUId(string uId)
        {
            var security = await _cosmosDbService.GetSecurityByUId(uId);
            if(security == null)
            {
                throw new Exception("Security Not Found");
            }
            var securityDto =_mapper.Map<SecurityDto>(security);
            return securityDto;        
        }

        public async Task<SecurityDto> UpdateSecurity(SecurityDto securityDto)
        {
            var existing = await _cosmosDbService.GetSecurityByUId(securityDto.UId);
            if (existing == null)
            {
                throw new Exception("Security not found");
            }
            existing.Active = false;
            await _cosmosDbService.ReplaceAsync(existing);
            existing.Initialize(false, "security", "Sudhanshu", "Sudhanshu");
            existing.Name = securityDto.Name;
            existing.Phone = securityDto.Phone;
            existing.Email = securityDto.Email;
            existing.Role = securityDto.Role;

            var response = await _cosmosDbService.AddSecurity(existing);
            var responseModel = new SecurityDto
            {
                UId = securityDto.UId,
                Name = securityDto.Name,
                Phone = securityDto.Phone,
                Email = securityDto.Email,
                Role = securityDto.Role,
            };
            return responseModel;
        }

        public async Task<string> DeleteSecurity(string uId)
        {
            var security = await _cosmosDbService.GetSecurityByUId(uId);
            if(security == null)
            {
                throw new Exception("Security Not found");
            }
            security.Active = false;
            security.Archived = true;
            await _cosmosDbService.ReplaceAsync(security);
            security.Initialize(false, "security", "Sudhanshu", "Sudhanshu");
            security.Active = false;
            return "Security Deleted";
        }
    }
}
