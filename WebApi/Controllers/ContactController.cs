using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
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
        public async Task<IActionResult> ModifyContact([FromRoute] int id, [FromBody] ContactForUpdateDto contactDto)
        {
            await _contactService.ModifyAsync(id, _mapper.Map<Contact>(contactDto));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateContact))]
        public async Task<IActionResult> CreateContact([FromBody] ContactForCreationDto contact)
            => Ok(await _contactService.CreateAsync(_mapper.Map<Contact>(contact)));


        [HttpGet("{id:int}", Name = nameof(GetContactById))]
        public async Task<IActionResult> GetContactById([FromRoute] int id)
            => Ok(_mapper.Map<ContactDto>(await _contactService.GetByIdAsync(id)));


        [HttpDelete("{id:int}", Name = nameof(DeleteContact))]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            await _contactService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}