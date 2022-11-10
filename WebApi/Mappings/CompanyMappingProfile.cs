using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyForCreationDto>()
            .ForMember(w => w.Name, opt => opt.MapFrom(p => p.Name))
            .ForMember(w => w.Address, opt => opt.MapFrom(p => p.Address))
            .ReverseMap();

            CreateMap<Company, CompanyDto>()
            .ForMember(w => w.Name, opt => opt.MapFrom(p => p.Name))
            .ReverseMap();
        }
    }
}