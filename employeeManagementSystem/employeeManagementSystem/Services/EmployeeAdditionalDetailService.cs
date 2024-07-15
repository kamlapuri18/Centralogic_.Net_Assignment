using AutoMapper;
using employeeManagementSystem.CosmosDb;
using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;
using employeeManagementSystem.Interfaces;

namespace employeeManagementSystem.Services
{
    public class EmployeeAdditionalDetailService : IEmployeeAdditionalDetailService
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IMapper _mapper;
       

        public EmployeeAdditionalDetailService( ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailDTO> AddAdditionalDetail(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO)
        {
            var additional = _mapper.Map<EmployeeAdditionalDetailEntity>(employeeAdditionalDetailDTO);
            additional.Initialize(true, "AdditionalDetail", "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDBService.AddEmployeeAdditionalDetail(additional);
            return _mapper.Map<EmployeeAdditionalDetailDTO>(additional);
        }

       

        public async Task<EmployeeAdditionalDetailDTO> GetEmployeeAdditionalByUid(string employeeBasicDetailsUId)
        {
            var additional = await _cosmosDBService.GetEmployeeAdditionalByUid(employeeBasicDetailsUId);
            if (additional == null)
            {
                throw new Exception("Details not Found");
            }
            var additionalModel = _mapper.Map<EmployeeAdditionalDetailDTO>(additional);
            return additionalModel;
        }

        public async Task<EmployeeAdditionalDetailDTO> UpdateAdditionalByUId(EmployeeAdditionalDetailDTO employeeAdditionalDetailDTO)
        {
            var existingAdditional = await _cosmosDBService.GetEmployeeAdditionalByUid(employeeAdditionalDetailDTO.EmployeeBasicDetailsUId);
            if (existingAdditional == null)
            {
                throw new Exception("Employee Not found");
            }
            existingAdditional.Active = false;
            await _cosmosDBService.ReplaceAsync(existingAdditional, existingAdditional.Id);
            existingAdditional.Initialize(false, "AdditionalDetail", "Sudhanshu", "Sudhanshu");
            
            existingAdditional.EmployeeBasicDetailsUId = employeeAdditionalDetailDTO.EmployeeBasicDetailsUId;
            existingAdditional.AlternateEmail = employeeAdditionalDetailDTO.AlternateEmail;
            existingAdditional.AlternateMobile = employeeAdditionalDetailDTO.AlternateMobile;
            existingAdditional.WorkInformation = employeeAdditionalDetailDTO.WorkInformation;
            existingAdditional.PersonalDetails = employeeAdditionalDetailDTO.PersonalDetails;
            existingAdditional.IdentityInformation = employeeAdditionalDetailDTO.IdentityInformation;
            
            var response = await _cosmosDBService.AddEmployeeAdditionalDetail(existingAdditional);
            return _mapper.Map<EmployeeAdditionalDetailDTO>(response);
        
        }

        public async Task<string> DeleteEmployee(string employeeBasicDetailsUId)
        {
            var additionalDetail = await _cosmosDBService.GetEmployeeAdditionalByUid(employeeBasicDetailsUId);
            if (additionalDetail == null)
            {
                throw new Exception("Detail Not Found");
            }
            additionalDetail.Active = false;
            additionalDetail.Archived = true;
            await _cosmosDBService.ReplaceAsync(additionalDetail, additionalDetail.Id);
            additionalDetail.Initialize(false, "AdditionalDetail", "Sudhanshu", "Sudhanshu");
            additionalDetail.Active = false;
            return "Additional Data Deleted";

        }

        public async Task<List<EmployeeAdditionalDetailDTO>> GetAllData()
        {
            var datas = await _cosmosDBService.GetAllData();
            var dataModels = new List<EmployeeAdditionalDetailDTO>();
            foreach (var data in datas)
            {
                var dataModel = new EmployeeAdditionalDetailDTO();
                dataModel.EmployeeBasicDetailsUId = data.EmployeeBasicDetailsUId;
                dataModel.AlternateEmail = data.AlternateEmail;
                dataModel.AlternateMobile = data.AlternateMobile;
                dataModel.WorkInformation = data.WorkInformation;
                dataModel.PersonalDetails = data.PersonalDetails;
                dataModel.IdentityInformation = data.IdentityInformation;
                dataModels.Add(dataModel);
            }
            return dataModels;
        }
    }
}
