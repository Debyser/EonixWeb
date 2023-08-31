using AutoMapper;

namespace WebApi.Mappings
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            //CreateMap<ContactRole, RoleView>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active.HasValue ? src.Active.Value : false))
            //.ForMember(dest => dest.Contact, opt => opt.Ignore())
            //.ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company)); // Include company data
        }
        // version de language ? avec le .net 6 tu peux faire du 11 
    }
}