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

        public PersonsController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpPut("{id:guid}", Name = nameof(ModifyPerson))]
        public async Task<IActionResult> ModifyPerson([FromRoute] Guid id, [FromBody] PersonViewModel company)
        {
            await _personService.ModifyAsync(id, _mapper.Map<Person>(company));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreatePerson))]
        public async Task<IActionResult> CreatePerson(PersonViewModel person) 
            => Ok(await _personService.CreateAsync(_mapper.Map<Person>(person)));

        [HttpGet("", Name = nameof(GetAllPersons))]
        public async Task<IActionResult> GetAllPersons()
            => Ok(_mapper.Map<IEnumerable<PersonViewModel>>(await _personService.GetAllAsync()));

        [HttpGet("{id:guid}", Name = nameof(GetPersonById))]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid id) 
            => Ok(_mapper.Map<PersonViewModel> (await _personService.GetByIdAsync(id)));

        [HttpDelete("{id:guid}", Name = nameof(DeletePerson))]
        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
        {
            await _personService.DeleteIdAsync(id);
            return NoContent();
        }
        //[HttpGet("", Name = nameof(GetPersonByFilter))]
        //public async Task<IActionResult> GetPersonByFilter([FromBody] PersonViewModel filter)
        //   => Ok(_mapper.Map<PersonViewModel>(await _personService.GetByFilterAsync(_mapper.Map<Person>(filter))));

    }
}
