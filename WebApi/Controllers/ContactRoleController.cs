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
    public class ContactRoleController : ControllerBase
    {
        private readonly IContactRoleService _contactRoleService;
        private readonly IMapper _mapper;

        public ContactRoleController(IContactRoleService contactService, IMapper mapper)
        {
            _contactRoleService = contactService;
            _mapper = mapper;
        }

        [HttpPut("{id:int}", Name = nameof(ModifyContactRole))]
        public async Task<IActionResult> ModifyContactRole([FromRoute] int id, [FromBody] ContactRoleForUpdateDto contactDto)
        {
            await _contactRoleService.ModifyAsync(id, _mapper.Map<ContactRole>(contactDto));
            return Ok();
        }
        [HttpPost("", Name = nameof(CreateContactRole))]
        public async Task<IActionResult> CreateContactRole([FromBody] ContactRoleForCreationDto contact)
           => Ok(await _contactRoleService.CreateAsync(_mapper.Map<ContactRole>(contact)));

        [HttpGet("{id:int}", Name = nameof(GetContactRoleById))]
        public async Task<IActionResult> GetContactRoleById([FromRoute] int id)
           => Ok(_mapper.Map<ContactRoleDto>(await _contactRoleService.GetByIdAsync(id)));


        [HttpDelete("{id:int}", Name = nameof(DeleteContactRole))]
        public async Task<IActionResult> DeleteContactRole([FromRoute] int id)
        {
            await _contactRoleService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}