using ApplicationCore.Entities;
using AutoMapper;

namespace WebApi.Mappings
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<AddressView, Address>().ReverseMap();
                //.ForPath(dest => dest.CountryId, input => input.MapFrom(src => src.Country.Id));
            //.ForMember(dest => dest.Country, act => act.Ignore()) WHy not add this line here ?
            // because auto mapper has the responsability for juste the mappping , it doest not know 
            // how the db works
        }
    }
}