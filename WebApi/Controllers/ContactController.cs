using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;
        public ContactController(IContactService contactService, IMapper mapper, ILoggerService logger)
        {
            _contactService = contactService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("{id:int}", Name = nameof(ModifyContact))]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ModifyContact([FromRoute] int id, [FromBody] ContactView contactDto)
        {
            await _contactService.ModifyAsync(id, _mapper.Map<Contact>(contactDto));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateContact))]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]     
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateContact([FromBody] ContactView contact)
            => Ok(await _contactService.CreateAsync(_mapper.Map<Contact>(contact)));

        //[HttpPost("", Name = nameof(CreateContactForCompany))]
        //[ProducesResponseType(201)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(409)]
        //public async Task<IActionResult> CreateContactForCompany(int companyId, [FromBody] ContactView contact) 
        //    => Ok(await _contactService.CreateEmployeeForCompany(companyId, _mapper.Map<Contact>(contact)));

        [HttpGet("{id:int}", Name = nameof(GetContactById))]
        [ProducesResponseType(typeof(ContactView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetContactById([FromRoute] int id)
            => Ok(_mapper.Map<ContactView>(await _contactService.GetByIdAsync(id)));


        [HttpDelete("{id:int}", Name = nameof(DeleteContact))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            await _contactService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}