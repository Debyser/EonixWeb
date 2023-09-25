using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;
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

        [HttpGet("{id:int}", Name = nameof(GetCompanyById))]
        [ProducesResponseType(typeof(CompanyView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyById([FromRoute] int id)
            => Ok(_mapper.Map<CompanyView>(await _companyService.GetByIdAsync(id)));

        // Why calling the mapper here? : because the service doesn't know the ViewModel
        //So you do the mapping in the Application layer
        [HttpGet("", Name = nameof(GetCompanyByFilter))]
        [ProducesResponseType(typeof(IEnumerable<CompanyView>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyByFilter([FromQuery] CompanyView filter)
         => Ok(_mapper.Map<IEnumerable<CompanyView>>(await _companyService.GetByFilterAsync(_mapper.Map<Company>(filter))));


        [HttpGet("collection/({ids})", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<long> ids)
           => Ok(_mapper.Map<IEnumerable<CompanyView>>(await _companyService.GetByIdsAsync(ids)));


        [HttpPost("", Name = nameof(CreateCompany))]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyView companyVM)
        {
            var createdCompany = _mapper.Map<CompanyView>(await _companyService.CreateCompanyAsync(_mapper.Map<Company>(companyVM)));
            return Created(createdCompany.Id.ToString(), createdCompany);
        }

        [HttpPut("{id:int}", Name = nameof(ModifyCompany))]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ModifyCompany([FromRoute] int id, [FromBody] CompanyView companyDto)
        {
            await _companyService.ModifyAsync(id, _mapper.Map<Company>(companyDto));
            return Ok();
        }

        // retourner les delete subsequent
        [HttpDelete("{id:int}", Name = nameof(DeleteCompanyById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteCompanyById([FromRoute] int id)
        {
            await _companyService.DeleteIdAsync(id);
            return NoContent();
        }


        //TODO: DeleteContactById , et ma route sera plus spécifique
    }
}