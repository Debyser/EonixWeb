using ApplicationCore.Services;
using AutoMapper;
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

        public CountryController(ICountryService countryService, IMapper mapper, ILoggerService logger)
        {
            _countryService = countryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("{id:int}", Name = nameof(ModifyCountry))]
        public async Task<IActionResult> ModifyCountry([FromRoute] int id, [FromBody] CountryView countryDto)
        {
            await _countryService.ModifyAsync(id, _mapper.Map<Country>(countryDto));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateCountry))]
        public async Task<IActionResult> CreateCountry([FromBody] CountryView country)
            => Ok(await _countryService.CreateAsync(_mapper.Map<Country>(country)));


        [HttpGet("{id:int}", Name = nameof(GetCountryById))]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
        {
            return Ok(_mapper.Map<CountryView>(await _countryService.GetByIdAsync(id)));
        }

        [HttpGet("", Name = nameof(GetCountryByFilter))]
        public async Task<IActionResult> GetCountryByFilter([FromQuery] CountryView filter)
            => Ok(_mapper.Map<IEnumerable<CountryView>>(await _countryService.GetByFilterAsync(_mapper.Map<Country>(filter))));

        [HttpDelete("{id:int}", Name = nameof(DeleteCountry))]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            await _countryService.DeleteIdAsync(id);
            return NoContent();
        }

        [HttpGet("collection/({ids})", Name = "CountryCollection")] 
        public IActionResult GetCountryCollection(IEnumerable<int> ids) 
        { 
            var countries = _countryService.GetByIdsAsync(ids);
            return Ok(countries);
        }
    }
}
