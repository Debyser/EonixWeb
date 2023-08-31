using ApplicationCore.Entities;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<ContactRole, RoleView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Contact, opt => opt.Ignore()) // prevent loop
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
        }
        // version de language ? avec le .net 6 tu peux faire du 11 
    }
}