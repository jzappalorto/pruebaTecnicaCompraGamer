using Microsoft.AspNetCore.Mvc;

namespace MiBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { mensaje = "Backend funcionando correctamente" });
        }
    }
}
