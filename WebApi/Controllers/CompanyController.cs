using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
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

        [HttpGet("", Name = nameof(GetCompanyByFilter))]
        public async Task<IActionResult> GetCompanyByFilter([FromQuery] CompanyDto filter)
         => Ok(_mapper.Map<IEnumerable<CompanyDto>>(await _companyService.GetByFilterAsync(_mapper.Map<Company>(filter))));

        [HttpGet("collection/({ids})", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
           => Ok(_mapper.Map<IEnumerable<CompanyDto>>(await _companyService.GetByIdsAsync(ids)));


        [HttpPost("", Name = nameof(CreateCompany))]
        // todo : ajouter le numero de statut code directement en attribut
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto companyVM)
        {
            // configurer company mapper pour qu'il map la liste des contact roles
            var entity = _mapper.Map<Company>(companyVM);
            entity.ContactRoles = _mapper.Map<List<ContactForCreationDto>, List<ContactRole>>(companyVM.Contacts.ToList());
            await _companyService.CreateCompanyAsync(entity);
            return Ok();
        }

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

        //[HttpPost("collection")]
        //public async Task<IActionResult> CreateCompanyCollection([FromBody] CompanyForCreationDto companyCollection)
        //{
        //    var result = await _companyService.CreateCompanyCollection(_mapper.Map<IEnumerable<Company>>(companyCollection));

        //    return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        //}
    }
}