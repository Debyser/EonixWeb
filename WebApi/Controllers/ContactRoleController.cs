using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactRoleController : ControllerBase
    {
        private readonly IContactRoleService _contactRoleService;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public ContactRoleController(IContactRoleService contactService, ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _contactRoleService = contactService;
            _mapper = mapper;
        }

        [HttpPost("", Name = nameof(CreateContactRole))]
        public async Task<IActionResult> CreateContactRole([FromBody] ContactRoleForCreationDto contact)
           => Ok(await _contactRoleService.CreateAsync(_mapper.Map<ContactRole>(contact)));

        [HttpGet("{id:int}", Name = nameof(GetContactRoleById))]
        public async Task<IActionResult> GetContactRoleById([FromRoute] int id)
           => Ok(_mapper.Map<ContactRoleDto>(await _contactRoleService.GetByIdAsync(id)));

    }
}