using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("", Name = nameof(GetTests))]
        public List<string> GetTests()
             => new List<string> { "azerty", "azerto" };
    }
}
