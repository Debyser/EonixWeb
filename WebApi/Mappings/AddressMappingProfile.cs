using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDto>()
            .ForMember(w => w.BoxNumber, opt => opt.MapFrom(p => p.BoxNumber))
            .ForMember(w => w.Street, opt => opt.MapFrom(p => p.Street))
            .ForMember(w => w.Zipcode, opt => opt.MapFrom(p => p.Zipcode))
            .ForMember(w => w.City, opt => opt.MapFrom(p => p.City))
            .ForMember(w => w.Country, opt => opt.MapFrom(p => p.Country))
            .ReverseMap();
        }
    }
}