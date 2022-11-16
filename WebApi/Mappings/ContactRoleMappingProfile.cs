using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class ContactRoleMappingProfile : Profile
    {
        public ContactRoleMappingProfile()
        {
            CreateMap<ContactRoleForCreationDto, ContactRole>()
               .ForMember(w => w.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(w => w.ContactRole2contact, opt => opt.MapFrom(src => src.ContactRole2contact))
               .ForMember(w => w.ContactRole2company, opt => opt.MapFrom(src => src.ContactRole2company))
               .ReverseMap();

            CreateMap<ContactRoleForUpdateDto, ContactRole>()
               .ForMember(w => w.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(w => w.ContactRole2contact, opt => opt.MapFrom(src => src.ContactId))
               .ForMember(w => w.ContactRole2company, opt => opt.MapFrom(src => src.CompanyId))
               .ReverseMap();

            CreateMap<ContactRoleDto, ContactRole>()
               .ForMember(w => w.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(w => w.ContactRole2contactNavigation, opt => opt.MapFrom(src => src.Contact))
               .ForMember(w => w.ContactRole2companyNavigation, opt => opt.MapFrom(src => src.Company))
               .ReverseMap();
        }
    }
}