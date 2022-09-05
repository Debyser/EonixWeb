using AutoMapper;
using EonixWebApi.ApplicationCore.Entities;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<PersonDto, Person>()
                .ForMember(w => w.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(w => w.LastName, opt => opt.MapFrom(src => src.LastName)).ReverseMap();
        }
    }
}
