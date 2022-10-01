using ApplicationCore.Entities;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            //CreateMap<Country, PersonDto>()
            //    .ForMember(w => w.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(w => w.LastName, opt => opt.MapFrom(src => src.LastName)).ReverseMap();
        }
    }
}
