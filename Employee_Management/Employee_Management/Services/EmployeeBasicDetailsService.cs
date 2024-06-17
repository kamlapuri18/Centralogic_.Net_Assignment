using AutoMapper;
using Employee_Management.Common;
using Employee_Management.CosmosDB;
using Employee_Management.DTO;
using Employee_Management.Entities;
using Employee_Management.Interfaces;

namespace Employee_Management.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        public readonly ICosmosDBService _cosmosDBService;
        public readonly IMapper _mapper;
        public EmployeeBasicDetailsService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<EmployeeBasicDetailDTO> AddEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            var employee = _mapper.Map<EmployeeBasicDetails>(employeeBasicDetailDTO);
            employee.Intialize(true, Credentials.EmployeeDocumentType, "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDBService.AddEmployee(employee);
            return _mapper.Map<EmployeeBasicDetailDTO>(response);


        }

        public async Task<List<EmployeeBasicDetailDTO>> GetAllEmployee()
        {
            var employees = await _cosmosDBService.GetAllEmployee();
            var employeeBasicDetailDTOs = new List<EmployeeBasicDetailDTO>();
            foreach (var employee in employees)
            {
                var employeeBasicDetailDTO = _mapper.Map<List<EmployeeBasicDetailDTO>>(employees);
            }
            return employeeBasicDetailDTOs;
        }

        public async Task<EmployeeBasicDetailDTO> GetAllEmployeeByID(string employeeID)
        {
            var employees = await _cosmosDBService.GetAllEmployeeByID(employeeID);
            var employeeBasicDetailDTO = _mapper.Map<EmployeeBasicDetailDTO>(employees);
            return employeeBasicDetailDTO;
        }

        public async Task<EmployeeBasicDetailDTO> UpdateEmployee(EmployeeBasicDetailDTO employeeBasicDetailDTO)
        {
            var existingEmployee = await _cosmosDBService.GetAllEmployeeByID(employeeBasicDetailDTO.EmployeeID);

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmployeeDocumentType, "Sudhanshu", "Sudhanshu");

            var responses = await _cosmosDBService.AddEmployee(existingEmployee);
            return _mapper.Map<EmployeeBasicDetailDTO>(responses);


            /*existingStudent.StudentName = studentModel.StudentName;
            existingStudent.PhoneNumber = studentModel.PhoneNumber;
            existingStudent.Age = studentModel.Age;
            existingStudent.RollNo = studentModel.RollNo;*/

            var response = await _cosmosDBService.AddEmployee(existingEmployee);

            // var responseModel = _mapper.Map<EmployeeBasicDetailDTO>(response);

            var responseModel = new EmployeeBasicDetailDTO
            {
                Salutory = response.Salutory,
                FirstName = response.FirstName,
                MiddleName = response.MiddleName,
                LastName = response.LastName,
                NickName = response.NickName,
                Email = response.Email,
                Mobile = response.Mobile,
                EmployeeID = response.EmployeeID,
                Role = response.Role,
                ReportingManagerUId = response.ReportingManagerUId,
                ReportingManagerName = response.ReportingManagerName,
                Address = response.Address
            };


            return responseModel;
        }

        public async Task<string> DeleteEmployee(string employeeID)
        {
            var employee = await _cosmosDBService.GetAllEmployeeByID(employeeID);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmployeeDocumentType, "Sudhanshu", "Sudhanshu");
            //student.Archived = true;
            employee.Active = false;

            var response = await _cosmosDBService.AddEmployee(employee);

            return "Record Deleted Successfully";
        }
    }


}