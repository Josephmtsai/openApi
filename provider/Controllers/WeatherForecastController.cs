using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;


namespace provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecast> _logger;

        public WeatherForecastController(ILogger<WeatherForecast> ilogger)
        {
            _logger = ilogger;
            LogExtensions.ServerLog(_logger, "Controller", "Init", "Consturt", "");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
