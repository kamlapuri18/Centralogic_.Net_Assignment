using AutoMapper;
using Microsoft.Azure.Cosmos.Linq;
using visitorManagementSystem.CosmosDB;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Services
{
    public class ManagerService : IManager
    { 
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public ManagerService(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
    
        public async Task<ManagerDto> AddManager(ManagerDto managerDto)
        {
            var manager = _mapper.Map<ManagerEntity>(managerDto);
            manager.Initialize(true, "manager", "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDbService.AddManager(manager);
            return _mapper.Map<ManagerDto>(response);
        }


        public async Task<ManagerDto> GetManagerByUId(string uId)
        {
            
            var manager = await _cosmosDbService.GetManagerByUId(uId);
            if (manager == null)
            {
                throw new Exception("Manager not found");
            }
            var managerDto =  _mapper.Map<ManagerDto>(manager);
            return managerDto;
            
        }

        public async Task<ManagerDto> UpdateManager(ManagerDto managerDto)
        {
            var existing = await _cosmosDbService.GetManagerByUId(managerDto.UId);
            if (existing == null)
            {
                throw new Exception("Manager not found");
            }
            existing.Active = false;
            await _cosmosDbService.ReplaceAsync(existing);
            existing.Initialize(false, "manager", "Sudhanshu", "Sudhanshu");
            existing.Name = managerDto.Name;
            existing.Phone = managerDto.Phone;
            existing.Email = managerDto.Email;
            existing.Role = managerDto.Role;

            var response = await _cosmosDbService.AddManager(existing);

            return _mapper.Map<ManagerDto>(existing);
        }


        public async Task<string> DeleteManager(string uId)
        {
            var manager = await _cosmosDbService.GetManagerByUId(uId);
            if (manager == null)
            {
                throw new Exception("Manager not found");
            }
            manager.Active = false;
            manager.Archived = true;
            await _cosmosDbService.ReplaceAsync(manager);
            manager.Initialize(false, "manager", "Sudhanshu", "Sudhanshu");
            manager.Active = false;
            return "Deleted";
        }
    }
}
