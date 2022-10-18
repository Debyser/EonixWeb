using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
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

        [HttpGet("{id:int}", Name = nameof(GetCountryById))]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
        {
            return Ok(_mapper.Map<CountryDto>(await _countryService.GetByIdAsync(id)));
        }

        [HttpGet("", Name = nameof(GetCountryByFilter))]
        public async Task<IActionResult> GetCountryByFilter([FromQuery] Country filter)
            => Ok(_mapper.Map<IEnumerable<CountryDto>>(await _countryService.GetByFilterAsync(_mapper.Map<Country>(filter))));
    }
}
