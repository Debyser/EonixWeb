using AutoMapper;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Mappings
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyForCreationDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(p => p.Address))
            .ReverseMap();

            CreateMap<Company, CompanyDto>()
            .ForMember(w => w.Name, opt => opt.MapFrom(p => p.Name))
            .ReverseMap();

            CreateMap<Company, CompanyForUpdateDto>()
            .ForMember(w => w.Name, opt => opt.MapFrom(p => p.Name))
            .ReverseMap();

            //CreateMap<CompanyForCreationDto, ContactRole>()
            //   .ForPath(dest => dest.Company.Name, input => input.MapFrom(src => src.Name))
            //   .ForPath(dest => dest.Company.Address, input => input.MapFrom(src => src.Address))
            //   .ReverseMap();
        }
    }
}