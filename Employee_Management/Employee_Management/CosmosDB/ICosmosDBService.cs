using Employee_Management.DTO;
using Employee_Management.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.CosmosDB
{
    public interface ICosmosDBService
    {
        

        Task<EmployeeBasicDetails> AddEmployee(EmployeeBasicDetails entity);
        Task<List<EmployeeBasicDetails>> GetAllEmployee();
       
        Task<EmployeeBasicDetails> GetAllEmployeeByID(string employeeID);
        Task ReplaceAsync(dynamic employee);
        
    }
}