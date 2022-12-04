using ApplicationCore.Services;
using Autofac.Core;
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

        public CompanyController(IAddressService addressService, ICompanyService companyService, IMapper mapper)
        {
            _addressService = addressService;
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("", Name = nameof(GetCompanyByFilter))]
        public async Task<IActionResult> GetCompanyByFilter([FromQuery] CompanyDto filter)
         => Ok(_mapper.Map<IEnumerable<CompanyDto>>(await _companyService.GetByFilterAsync(_mapper.Map<Company>(filter))));


        [HttpPost("", Name = nameof(CreateCompany))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto copmpany)
            => Ok(await _companyService.CreateAsync(_mapper.Map<Company>(copmpany)));

        [HttpPut("{id:int}", Name = nameof(ModifyCompany))]
        public async Task<IActionResult> ModifyCompany([FromRoute] int id, [FromBody] CompanyForUpdateDto  companyDto)
        {
            await _companyService.ModifyAsync(id, _mapper.Map<Company>(companyDto));
            return Ok();
        }

        [HttpDelete("{id:int}", Name = nameof(DeleteCompany))]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
            await _companyService.DeleteIdAsync(id);
            return NoContent();
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = await _companyService.CreateCompanyCollection(_mapper.Map<IEnumerable<Company>>(companyCollection));

            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }
    }
}