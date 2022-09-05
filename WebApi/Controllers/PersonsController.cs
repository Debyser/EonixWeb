using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EonixWebApi.ApplicationCore.Entities;
using ApplicationCore.Services;
using WebApi.Models;

namespace EonixWebApi.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;

        public PersonsController(IPersonService personService, IMapper mapper, ILoggerService logger)
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("{id:guid}", Name = nameof(ModifyPerson))]
        public async Task<IActionResult> ModifyPerson([FromRoute] Guid id, [FromBody] Person person)
        {
            await _personService.ModifyAsync(id, _mapper.Map<PersonDto>(person));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreatePerson))]
        public async Task<IActionResult> CreatePerson([FromBody] Person person) 
            => Ok(await _personService.CreateAsync(_mapper.Map<PersonDto>(person)));

        [HttpGet("", Name = nameof(Get))]
        public IEnumerable<string> Get()
        {
            throw new Exception("Exception");
            _logger.LogInfo("Here is info message from our values controller."); 
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarn("Here is warn message from our values controller.");
            _logger.LogError("Here is an error message from our values controller."); 
            return new string[] { "value1", "value2" };
        }
        //[HttpGet("", Name = nameof(GetPersonByFilter))]
        //public async Task<IActionResult> GetPersonByFilter([FromQuery] PersonViewModel filter)
        // => Ok(_mapper.Map<IEnumerable<PersonViewModel>>(await _personService.GetByFilterAsync(_mapper.Map<Person>(filter))));


        [HttpGet("{id:guid}", Name = nameof(GetPersonById))]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid id) 
            => Ok(_mapper.Map<Person> (await _personService.GetByIdAsync(id)));

        [HttpDelete("{id:guid}", Name = nameof(DeletePerson))]
        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
        {
            await _personService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}