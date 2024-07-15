using Microsoft.Azure.Cosmos;
using System.Reflection.Metadata;
using visitorManagementSystem.Common;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;

namespace visitorManagementSystem.CosmosDB
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        
        public CosmosDbService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName); 
        }

        public async Task<ManagerEntity> AddManager(ManagerEntity manager)
        {
            var response = await _container.CreateItemAsync(manager);
            return response;
        }
        public async Task<ManagerEntity> GetManagerByUId(string uId)
        {
            var responses  = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(a => a.UId == uId && a.Active && !a.Archived).FirstOrDefault();
            return responses;
        }
        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);
        }

        public async Task<OfficeEntity> AddOffice(OfficeEntity office)
        {
            return await _container.CreateItemAsync(office);
        }

        public async Task<SecurityEntity> AddSecurity(SecurityEntity security)
        {
            return await _container.CreateItemAsync(security);
        }

        public async Task<OfficeEntity> GetOfficeByUId(string uId)
        {
            var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(a => a.UId == uId && a.Active && !a.Archived).FirstOrDefault();
            return response;
        }

        public async Task<SecurityEntity> GetSecurityByUId(string uId)
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(a => a.UId == uId && a.Active && !a.Archived).FirstOrDefault();
            return response;
        }

        public async Task<VisitorEntity> AddVisitor(VisitorEntity visitor)
        {
            return await _container.CreateItemAsync(visitor);
            
        }

        public async Task<VisitorEntity> GetVisitorByUId(string uId)
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(a => a.UId == uId && a.Active && !a.Archived).FirstOrDefault();
            return response;
        }
        public async Task<List<VisitorEntity>> GetAllVisitor()
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "visitor").ToList();
            return response;
        }


    }
}

 