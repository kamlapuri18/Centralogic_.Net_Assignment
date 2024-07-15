using employeeManagementSystem.Entities;

namespace employeeManagementSystem.CosmosDb
{
    public interface ICosmosDBService
    {
        Task<EmployeeAdditionalDetailEntity> AddEmployeeAdditionalDetail(EmployeeAdditionalDetailEntity additional);
        Task<EmployeeBasicDetailEntity> AddEmployeeBasicDetail(EmployeeBasicDetailEntity employee);
        Task<List<EmployeeAdditionalDetailEntity>> GetAllData();
        Task<List<EmployeeBasicDetailEntity>> GetAllEmployee();
        Task<EmployeeAdditionalDetailEntity> GetEmployeeAdditionalByUid(string employeeBasicDetailsUId);
        Task<EmployeeBasicDetailEntity> GetEmployeeById(string id);
        Task ReplaceAsync(dynamic existing, string id);
    }
}
