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
        public async Task<IActionResult> ModifyContact([FromRoute] int id, [FromBody] ContactView contactView)
        {
            contactView.Id = id;
            await _contactService.ModifyAsync(id, _mapper.Map<Contact>(contactView));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateContact))]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateContact([FromBody] ContactView contact)
            => Ok(await _contactService.CreateAsync(_mapper.Map<Contact>(contact)));

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

        //[HttpGet("", Name = nameof(GetContacts))]
        //[ProducesResponseType(typeof(IEnumerable<ContactView>), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetContacts([FromQuery] ContactParameters contactParameters, CancellationToken cancellationToken = default)
        //{
        //    var (contacts, metaData) = await _contactService.GetAllAsync(contactParameters, cancellationToken);
        //    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(metaData); // Use indexer to set the header
        //    return Ok(_mapper.Map<IEnumerable<CompanyView>>(companies));
        //}
    }
}