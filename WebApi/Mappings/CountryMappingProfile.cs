using ApplicationCore.Entities;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryView>();
        }
    }
}
