using AutoMapper;
using employeeManagementSystem.Common;
using employeeManagementSystem.CosmosDb;
using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;
using employeeManagementSystem.Interfaces;
using Newtonsoft.Json;

namespace employeeManagementSystem.Services
{
    public class EmployeeBasicDetailService : IEmployeeBasicDetailService
    {
        private readonly ICosmosDBService _cosmoDBService;
        private readonly IMapper _mapper;


        public EmployeeBasicDetailService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmoDBService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<EmployeeBasicDetailDTO> AddEmployeeBasicDetail(EmployeeBasicDetailDTO employeeModel)
        {
            var employee = _mapper.Map<EmployeeBasicDetailEntity>(employeeModel);
            employee.Initialize(true, "BasicDetail", "Sudhanshu", "Sudhanshu");
            var response = await _cosmoDBService.AddEmployeeBasicDetail(employee);
            return _mapper.Map<EmployeeBasicDetailDTO>(response);
        }
        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployee()
        {
            var employees = await _cosmoDBService.GetAllEmployee();
            var employeeModels = new List<EmployeeBasicDetailDTO>();
            foreach(var employee in employees)
            {

                var employeeModel = new EmployeeBasicDetailDTO();
                employeeModel.Id = employee.Id;
                employeeModel.Salutory = employee.Salutory;
                employeeModel.FirstName = employee.FirstName;
                employeeModel.MiddleName = employee.MiddleName;
                employeeModel.LastName = employee.LastName;
                employeeModel.Email = employee.Email;
                employeeModel.NickName = employee.NickName;
                employeeModel.ReportingManagerUId = employee.ReportingManagerUId;
                employeeModel.ReportingManagerName = employee.ReportingManagerName;
                employeeModel.Mobile = employee.Mobile;
                employeeModel.Role = employee.Role;
                employeeModel.Address = employee.Address;

                employeeModels.Add(employeeModel);

            }
            return employeeModels;

        }

        public async Task<EmployeeBasicDetailDTO> GetEmployeeById(string id)
        {
            var employee = await _cosmoDBService.GetEmployeeById(id);
            if (employee == null)
            {
                throw new Exception("Employee not Found");
            }
            var employeeModel = _mapper.Map<EmployeeBasicDetailDTO>(employee);
            return employeeModel;
        }

        public async Task<EmployeeBasicDetailDTO> UpdateDetail(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            var existing = await _cosmoDBService.GetEmployeeById(employeeBasicDetailDTO.Id);
            if (existing == null)
            {
                throw new Exception("Employee Not found");
            }
            existing.Active = false;
            await _cosmoDBService.ReplaceAsync(existing, existing.Id);
            existing.Initialize(false, "BasicDetail", "Sudhanshu", "Sudhanshu");

            existing.FirstName = employeeBasicDetailDTO.FirstName;
            existing.MiddleName = employeeBasicDetailDTO.MiddleName;
            existing.LastName = employeeBasicDetailDTO.LastName;
            existing.Email = employeeBasicDetailDTO.Email;
            existing.NickName = employeeBasicDetailDTO.NickName;
            existing.ReportingManagerName = employeeBasicDetailDTO.ReportingManagerName;
            existing.ReportingManagerUId = employeeBasicDetailDTO.ReportingManagerUId;
            existing.Mobile = employeeBasicDetailDTO.Mobile;
            existing.Role = employeeBasicDetailDTO.Role;
            existing.Address = employeeBasicDetailDTO.Address;

            var response = await _cosmoDBService.AddEmployeeBasicDetail(existing);
            return _mapper.Map<EmployeeBasicDetailDTO>(existing);
        }

        public async Task<string> DeleteEmployee(string id)
        {
            var employee = await _cosmoDBService.GetEmployeeById(id);
            if (employee == null)
            {
                throw new Exception("Employee Not found");
            }
            employee.Active = false;
            employee.Archived = true;
            await _cosmoDBService.ReplaceAsync(employee, employee.Id);
            employee.Initialize(false, "BasicDetail", "Sudhanshu", "Sudhanshu");
            employee.Active = false;
            return "Data Deleted";
        }

        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployeeDetailByRole(string role)
        {
            try
            {
                var allEmployees = await GetAllEmployee();
                var response = allEmployees.FindAll(a => a.Role == role);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllEmployeeBasicDetailsByRole", ex);
            }
        }


        public async Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {

            var employees = await GetAllEmployee();
            employeeFilterCriteria.totalCount = employees.Count;
            var skip = employeeFilterCriteria.PageSize * (employeeFilterCriteria.Page - 1);
            employeeFilterCriteria.Employee = employees.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();
            return employeeFilterCriteria;
        }

        public async Task<EmployeeBasicDetailDTO> AddEmployeeByMakePostRequest(EmployeeBasicDetailDTO employeeBasicDTO)
        {
            var serialixedObj = JsonConvert.SerializeObject(employeeBasicDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddEmployeeEndpoint, serialixedObj);
            var responseObj = JsonConvert.DeserializeObject<EmployeeBasicDetailDTO>(requestObj);
            return responseObj;
        }

        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployeeByMakeGetRequest(string employeeId)
        {
                var url = $"{Credentials.EmployeeUrl}/{Credentials.GetEmployeeEndpoint}/{employeeId}";
                var responseObj = await HttpClientHelper.MakeGetRequest(url,Credentials.GetEmployeeEndpoint);
                var employeeBasicDetailDTO = JsonConvert.DeserializeObject<List<EmployeeBasicDetailDTO>>(responseObj);
                return employeeBasicDetailDTO;
        }
    }
}
