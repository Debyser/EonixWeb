using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactForCreationDto, Contact>()
            .ForMember(dest => dest.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, input => input.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Address, input => input.MapFrom(src => src.Address))
            .ReverseMap();


            CreateMap<ContactForUpdateDto, Contact>()
            .ForMember(dest => dest.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, input => input.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.Address, input => input.MapFrom(src => src.Address))
            .ReverseMap();

            CreateMap<ContactDto, Contact>()
            .ForMember(dest => dest.Firstname, input => input.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, input => input.MapFrom(src => src.Lastname))
            .ReverseMap();


            CreateMap<ContactForCreationDto, ContactRole>()
               .ForMember(dest => dest.Name, input => input.MapFrom(src => src.RoleName))
               .ForPath(dest => dest.Contact.Firstname, input => input.MapFrom(src => src.Firstname))
               .ForPath(dest => dest.Contact.Lastname, input => input.MapFrom(src => src.Lastname))
               .ForPath(dest => dest.Contact.Address, input => input.MapFrom(src => src.Address))
               .ReverseMap();

        }
    }
}