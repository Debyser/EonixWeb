using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactView, ContactRole>()
            .ForMember(dest => dest.Name, input => input.MapFrom(src => src.RoleName))
            .ForPath(dest => dest.Contact.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForPath(dest => dest.Contact.Lastname, input => input.MapFrom(src => src.Lastname))
            .ForPath(dest => dest.Contact.Address, input => input.MapFrom(src => src.Address));


            CreateMap<ContactRole, ContactView>()
            .ForPath(dest => dest.Lastname, input => input.MapFrom(src => src.Contact.Lastname))
            .ForPath(dest => dest.Firstname, input => input.MapFrom(src => src.Contact.Firstname))
            .ForPath(dest => dest.Address, input => input.MapFrom(src => src.Contact.Address));

            CreateMap<ContactView, Contact>()
            .ForMember(dest => dest.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, input => input.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Address, input => input.MapFrom(src => src.Address))
            .ForMember(dest => dest.Id, input=> input.Ignore())
            .ForMember(dest => dest.Contact2address, input=> input.Ignore());

            CreateMap<Contact, ContactView>()
            .ForMember(dest => dest.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, input => input.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Address, input => input.MapFrom(src => src.Address))
            .ForMember(dest => dest.RoleName, input => input.Ignore());


           //CreateMap<List<ContactView>, List<Contact>>();
        }
    }
}