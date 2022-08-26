using AutoMapper;
using EonixWebApi.ApplicationCore.Entities;
using EonixWebApi.Web.Models;

namespace EonixWebApi.Web.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactViewModel>()
                .ForMember(w => w.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(w => w.LastName, opt => opt.MapFrom(src => src.LastName)).ReverseMap();
        }
    }
}
