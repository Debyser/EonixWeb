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
            .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Name))
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(p => p.ContactRoles))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(p => p.Address));

            CreateMap<CompanyView, ContactRole>()
                .ForPath(dest => dest.Company.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<CompanyView, Company>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Name))
            .ForMember(dest => dest.ContactRoles, opt => opt.MapFrom(p => p.Contacts))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(p => p.Address));
        }
    }
}