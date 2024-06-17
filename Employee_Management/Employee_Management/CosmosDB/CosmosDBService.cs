using Employee_Management.Common;
using Employee_Management.DTO;
using Employee_Management.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Employee_Management.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        public  async Task<EmployeeBasicDetails> AddEmployee(EmployeeBasicDetails employeeBasicDetail)
        {
            var response = await _container.CreateItemAsync(employeeBasicDetail);
            return response;
        }

        public async Task<List<EmployeeBasicDetails>> GetAllEmployee()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.Active == true && a.Archived == false && a.DocumentType == Credentials.EmployeeDocumentType).ToList();
            return response;
        }

       

        public async Task<EmployeeBasicDetails> GetAllEmployeeByID(string employeeID)
        {
            var employee = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.EmployeeID == employeeID && a.Active && a.Active && a.DocumentType == Credentials.EmployeeDocumentType).FirstOrDefault();
            return employee;
        }

        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.employeeId);
        }
    }
}
