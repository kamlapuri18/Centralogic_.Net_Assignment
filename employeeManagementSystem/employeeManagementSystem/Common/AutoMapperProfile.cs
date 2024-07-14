using AutoMapper;
using employeeManagementSystem.DTO;
using employeeManagementSystem.Entities;

namespace employeeManagementSystem.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeBasicDetailEntity, EmployeeBasicDetailDTO>().ReverseMap();
            CreateMap<EmployeeAdditionalDetailEntity, EmployeeAdditionalDetailDTO>().ReverseMap();

        }
    }
}
