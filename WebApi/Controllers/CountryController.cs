using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;

        public CountryController(ICountryService countryService, IMapper mapper, ILoggerService logger)
        {
            _countryService = countryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id:int}", Name = nameof(GetCountryById))]
        public async Task<IActionResult> GetCountryById([FromRoute] long id)
            => Ok(_mapper.Map<CountryView>(await _countryService.GetByIdAsync(id)));

        //[HttpGet("", Name = nameof(GetCountries))]
        //public async Task<IActionResult> GetCountries()
        //     => Ok(_mapper.Map<IEnumerable<CountryView>>(await _countryService.GetListAsync()));

        [HttpGet("", Name = nameof(GetCountries))]
        public List<string> GetCountries()
            => new List<string>{"azerty","azerto"};
    }
}