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
           => Ok(_mapper.Map<PersonDto>(await _countryService.GetByIdAsync(id)));
    }
}
