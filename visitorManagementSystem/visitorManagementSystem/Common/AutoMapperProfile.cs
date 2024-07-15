using AutoMapper;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;

namespace visitorManagementSystem.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ManagerEntity, ManagerDto>().ReverseMap();
            CreateMap<OfficeEntity, OfficeDto>().ReverseMap();
            CreateMap<SecurityEntity, SecurityDto>().ReverseMap();
            CreateMap<VisitorEntity, VisitorDto>().ReverseMap();
        }
    }
}
