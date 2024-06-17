using AutoMapper;
using Employee_Management.DTO;
using Employee_Management.Entities;

namespace Employee_Management.Common
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<EmployeeBasicDetailDTO, EmployeeBasicDetails>().ReverseMap();

        }
    }
}
