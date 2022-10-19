using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
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
        public async Task<IActionResult> ModifyCountry([FromRoute] int id, [FromBody] CountryDto countryDto)
        {
            await _countryService.ModifyAsync(id, _mapper.Map<Country>(countryDto));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateCountry))]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto country)
            => Ok(await _countryService.CreateAsync(_mapper.Map<Country>(country)));

        [HttpGet("{id:int}", Name = nameof(GetCountryById))]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
            => Ok(_mapper.Map<CountryDto>(await _countryService.GetByIdAsync(id)));

        [HttpGet("", Name = nameof(GetCountryByFilter))]
        public async Task<IActionResult> GetCountryByFilter([FromQuery] CountryDto filter)
            => Ok(_mapper.Map<IEnumerable<CountryDto>>(await _countryService.GetByFilterAsync(_mapper.Map<Country>(filter))));
        [HttpDelete("{id:int}", Name = nameof(DeleteCountry))]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            await _countryService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}
