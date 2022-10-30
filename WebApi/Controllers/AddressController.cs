using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        private ILoggerService _logger;

        public AddressController(IAddressService addressService, IMapper mapper, ILoggerService logger)
        {
            _addressService = addressService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("{id:int}", Name = nameof(ModifyAddress))]
        public async Task<IActionResult> ModifyAddress([FromRoute] int id, [FromBody] AddressDto addressDto)
        {
            await _addressService.ModifyAsync(id, _mapper.Map<Address>(addressDto));
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateAddress))]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDto Address)
            => Ok(await _addressService.CreateAsync(_mapper.Map<Address>(Address)));


        [HttpGet("{id:int}", Name = nameof(GetAddressById))]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
            => Ok(_mapper.Map<AddressDto>(await _addressService.GetByIdAsync(id)));


        [HttpDelete("{id:int}", Name = nameof(DeleteAddress))]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            await _addressService.DeleteIdAsync(id);
            return NoContent();
        }
    }
}
