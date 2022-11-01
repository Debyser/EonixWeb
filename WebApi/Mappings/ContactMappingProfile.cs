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
            .ForMember(p => p.Firstname, opt => opt.MapFrom(p => p.Firstname))
            .ForMember(p => p.Lastname, opt => opt.MapFrom(p => p.Lastname))
            .ForMember(p => p.Address, opt => opt.MapFrom(p => p.Address))
            .ReverseMap();

            CreateMap<ContactForUpdateDto, Contact>()
            .ForMember(p => p.Firstname, opt => opt.MapFrom(p => p.Firstname))
            .ForMember(p => p.Lastname, opt => opt.MapFrom(p => p.Lastname))
            .ForMember(p => p.Address, opt => opt.MapFrom(p => p.Address))
            .ReverseMap();

            CreateMap<ContactDto, Contact>()
            .ForMember(p => p.Firstname, opt => opt.MapFrom(p => p.Firstname))
            .ForMember(p => p.Lastname, opt => opt.MapFrom(p => p.Lastname))
            .ReverseMap();
        }
    }
}