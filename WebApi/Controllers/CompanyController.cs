using ApplicationCore.Services;
using AutoMapper;
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

        [HttpPost("", Name = nameof(CreateCompany))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto copmpany)
            => Ok(await _companyService.CreateAsync(_mapper.Map<Company>(copmpany)));
    }
}