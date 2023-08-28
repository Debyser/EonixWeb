using ApplicationCore.Entities;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyView>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(p => p.Address));
            CreateMap<CompanyView, Company>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(p => p.Address));
        }
    }
}
