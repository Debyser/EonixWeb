using ApplicationCore.Entities;
using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryDto>()
                .ForMember(w => w.CountryAlpha3Code, opt => opt.MapFrom(src => src.Iso3Code))
                .ForMember(w => w.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
