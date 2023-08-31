using ApplicationCore.Entities;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactView>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ContactRoles.Select(role => new RoleView
            {
                Id = role.Id,
                Name = role.Name,
                Active = role.Active,
                Company = role.Company != null ? new CompanyView
                {
                    Id = role.CompanyId,
                    Name = role.Company.Name,
                    Address = role.Company.Address != null ? new AddressView
                    {
                        BoxNumber = role.Company.Address.BoxNumber,
                        City = role.Company.Address.City,
                        Street = role.Company.Address.Street,
                        Zipcode = role.Company.Address.Zipcode,
                        Country = role.Company.Address.Country != null ? new CountryView
                        {
                            Id = role.Company.Address.Country.Id,
                            Name = role.Company.Address.Country.Name
                        } : null,
                    } : null
                } : null
            })));

            // mettre default!;
            // language en 10 pas en 11

            //CreateMap<Contact, ContactView>()
            //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.ContactRoles.Select(role => new RoleView
            //{
            //    Id = role.Id,
            //    Name = role.Name,
            //    Active = role.Active.HasValue ? role.Active.Value : null,
            //})));
        }
    }
}