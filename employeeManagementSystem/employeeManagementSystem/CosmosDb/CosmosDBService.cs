using employeeManagementSystem.Common;
using employeeManagementSystem.Entities;
using Microsoft.Azure.Cosmos;

namespace employeeManagementSystem.CosmosDb
{
    public class CosmosDBService : ICosmosDBService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);
        }


        public async Task<EmployeeBasicDetailEntity> AddEmployeeBasicDetail(EmployeeBasicDetailEntity employee)
        {
            var response = await _container.CreateItemAsync(employee);
            return response;
        }

        public async Task<EmployeeBasicDetailEntity> GetEmployeeById(string id)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetailEntity>(true).Where(x => x.Id == id && x.Active && !x.Archived).FirstOrDefault();
            return response;
        }

        public async Task ReplaceAsync(dynamic existing, string id)
        {
            var response = await _container.ReplaceItemAsync(existing, existing.Id);
        }

        public async Task<EmployeeAdditionalDetailEntity> AddEmployeeAdditionalDetail(EmployeeAdditionalDetailEntity additional)
        {
            var response = await _container.CreateItemAsync(additional);
            return response;
        }

        public async Task<EmployeeAdditionalDetailEntity> GetEmployeeAdditionalByUid(string employeeBasicDetailsUId)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailEntity>(true).Where(x => x.EmployeeBasicDetailsUId == employeeBasicDetailsUId && x.Active && !x.Archived).FirstOrDefault();
            return response;

        }

        public async Task<List<EmployeeBasicDetailEntity>> GetAllEmployee()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetailEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "BasicDetail").ToList();
            return response;
        }

        public async Task<List<EmployeeAdditionalDetailEntity>> GetAllData()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "AdditionalDetail").ToList();
            return response;
        }
    }
}