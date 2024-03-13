using Microsoft.AspNetCore.Mvc;

namespace Weather.API.Conttrollers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        public WeatherController()
        {

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await Task.FromResult(0);

            return Ok("Hello");    
        }
    }
}
