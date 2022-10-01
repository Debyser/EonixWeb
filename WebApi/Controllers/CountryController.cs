using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController :  ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;

        public CountryController(ICountryService personService, IMapper mapper, ILoggerService logger)
        {
            _countryService = personService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id:int}", Name = nameof(GetCountryById))]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
        {
            //=> Ok(_mapper.Map<PersonDto>(await _countryService.GetByIdAsync(id)));
            //    provoque l'erreur car pas de personDto ic'

            return null;
        }

        [HttpGet("", Name = nameof(GetCountryByFilter))]
        public async Task<IActionResult> GetCountryByFilter([FromQuery] Country filter)
            => Ok(_mapper.Map<IEnumerable<PersonDto>>(await _countryService.GetByFilterAsync(_mapper.Map<Country>(filter))));
    }
}
