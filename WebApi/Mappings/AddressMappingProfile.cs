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
            //.ForMember(dest => dest.BoxNumber, opt => opt.MapFrom(p => p.BoxNumber))
            //.ForMember(dest => dest.Street, opt => opt.MapFrom(p => p.Street))
            //.ForMember(dest => dest.Zipcode, opt => opt.MapFrom(p => p.Zipcode))
            //.ForMember(dest => dest.City, opt => opt.MapFrom(p => p.City));

            //CreateMap<AddressView, Country>()
            //    .ForMember(dest => dest.)

        }
    }
}