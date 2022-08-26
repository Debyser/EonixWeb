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

        public PersonsController(IPersonService organizationService, IMapper mapper)
        {
            _personService = organizationService;
            _mapper = mapper;
        }

        [HttpPut("{companyId:int}", Name = nameof(ModifyPerson))] // ask STEPH
        //[ProducesResponseType(typeof(int), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> ModifyPerson([FromRoute] Guid companyId, [FromBody] PersonViewModel company)
        {
            await _personService.ModifyAsync(companyId, _mapper.Map<Person>(company));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreatePerson))]
        [ProducesResponseType(typeof(long), 201)]
        [ProducesResponseType(typeof(long), 204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePerson(PersonViewModel company) 
        {
            return Ok(await _personService.CreateAsync(_mapper.Map<Person>(company)));
        }


        [HttpGet("", Name = nameof(GetAllPersons))]
        [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPersons() => Ok(_mapper.Map<IEnumerable<PersonViewModel>>(await _personService.GetAllAsync()));

        [HttpGet("{companyId:int}", Name = nameof(GetPersonById))]
        [ProducesResponseType(typeof(PersonViewModel), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid personId) => Ok(_mapper.Map<PersonViewModel> (await _personService.GetByIdAsync(personId)));

    }
}
