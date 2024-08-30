using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WSInformatica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            List<WeatherForecast> lst = new List<WeatherForecast>();
            lst.Add(new WeatherForecast() { Id = 5, Nombre = "Braian" });
            lst.Add(new WeatherForecast() { Id = 6, Nombre = "Hector" });
            return lst;
        }
    }
}