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
        public async Task<IActionResult> GetCompanyById([FromRoute] int id)
            => Ok(_mapper.Map<CompanyView>(await _companyService.GetByIdAsync(id)));

        // Why calling the mapper here ? : because the service doesn't know the ViewModel
        // So you do the mapping in the Application layer
        [HttpGet("", Name = nameof(GetCompanyByFilter))]
        public async Task<IActionResult> GetCompanyByFilter([FromQuery] CompanyView filter)
         => Ok(_mapper.Map<IEnumerable<CompanyView>>(await _companyService.GetByFilterAsync(_mapper.Map<Company>(filter))));

        [HttpGet("collection/({ids})", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
           => Ok(_mapper.Map<IEnumerable<CompanyView>>(await _companyService.GetByIdsAsync(ids)));


        [HttpPost("", Name = nameof(CreateCompany))]
        // todo : ajouter le numero de statut code directement en attribut
        public async Task<IActionResult> CreateCompany([FromBody] CompanyView companyVM)
        {
            var entity = _mapper.Map<Company>(companyVM);
            var createdCompany = await _companyService.CreateCompanyAsync(entity);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id:int}", Name = nameof(ModifyCompany))]
        public async Task<IActionResult> ModifyCompany([FromRoute] int id, [FromBody] CompanyView companyDto)
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
    }
}