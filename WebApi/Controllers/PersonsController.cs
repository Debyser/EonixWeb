﻿using AutoMapper;
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
        public async Task<IActionResult> ModifyPerson([FromRoute] Guid id, [FromBody] PersonDto person)
        {
            await _personService.ModifyAsync(id, _mapper.Map<Person>(person));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreatePerson))]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto person) 
            => Ok(await _personService.CreateAsync(_mapper.Map<Person>(person)));


        [HttpGet("", Name = nameof(GetPersonByFilter))]
        public async Task<IActionResult> GetPersonByFilter([FromQuery] PersonDto filter)
         => Ok(_mapper.Map<IEnumerable<PersonDto>>(await _personService.GetByFilterAsync(_mapper.Map<Person>(filter))));


        [HttpGet("{id:guid}", Name = nameof(GetPersonById))]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid id)
            => Ok(_mapper.Map<PersonDto>(await _personService.GetByIdAsync(id)));

        [HttpDelete("{id:guid}", Name = nameof(DeletePerson))]
        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
        {
            await _personService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}