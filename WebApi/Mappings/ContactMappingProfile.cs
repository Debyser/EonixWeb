namespace WebApi.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {

            CreateMap<Contact, ContactView>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ContactRoles));
        }
    }
}