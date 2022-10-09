using Microsoft.AspNetCore.Mvc;

namespace ExampleApplication.SwaggerVersioning.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index =>
            {
                var (temperature, summary) = GetForecast();

                return new WeatherForecast
                {
                    Date = DateTime.UtcNow.AddDays(index),
                    TemperatureC = temperature,
                    Summary = summary
                };
            })
            .ToArray();
        }

        private static (int, string) GetForecast()
        {
            var temperature = Random.Shared.Next(-20, 55);

            var summary = temperature switch
            {
                int n when (n > 40) => "Scorching",
                int n when (n > 35) => "Sweltering",
                int n when (n > 28) => "Hot",
                int n when (n > 25) => "Balmy",
                int n when (n > 23) => "Warm",
                int n when (n > 20) => "Mild",
                int n when (n > 18) => "Cool",
                int n when (n > 16) => "Chilly",
                int n when (n > 5) => "Bracing",
                _ => "Freezing"
            };

            return (temperature, summary);
        }
    }
}