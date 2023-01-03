using ApplicationCore.Entities;
using AutoMapper;

namespace WebApi.Mappings
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressView>();
            CreateMap<AddressView, Address>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new Country()
                { 
                    Iso3Code = src.CountryCode,
                    Name = src.CountryName
                }));
        }
    }
}