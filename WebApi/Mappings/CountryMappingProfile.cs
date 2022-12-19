using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryView>()
                .ForMember(w => w.CountryAlpha3Code, opt => opt.MapFrom(src => src.Iso3Code))
                .ForMember(w => w.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
