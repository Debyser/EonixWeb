using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;

        public CompanyController(IAddressService addressService, ICompanyService companyService, IMapper mapper, ILoggerService logger)
        {
            _addressService = addressService;
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("", Name = nameof(GetCompanyByFilter))]
        public async Task<IActionResult> GetCompanyByFilter([FromQuery] CompanyDto filter)
         => Ok(_mapper.Map<IEnumerable<CompanyDto>>(await _companyService.GetByFilterAsync(_mapper.Map<Company>(filter))));


        [HttpPost("", Name = nameof(CreateCompany))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto copmpany)
            => Ok(await _companyService.CreateAsync(_mapper.Map<Company>(copmpany)));

        [HttpPut("{id:int}", Name = nameof(ModifyCompany))]
        public async Task<IActionResult> ModifyCompany([FromRoute] int id, [FromBody] ContactForUpdateDto contactDto)
        {
            //await _companyService.ModifyAsync(id, _mapper.Map<Contact>(contactDto));
            return Ok();
        }
    }
}