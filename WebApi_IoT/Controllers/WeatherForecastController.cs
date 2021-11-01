using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models;
using System.IO;
using System.Text;
using System.Timers;

namespace WebApi_IoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Weather[] Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Weather
            {
                Id = index + 1,
                TemperatureC = rng.Next(-20, 55),
                Name = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post([FromBody] Weather weather)
        {
            MySingletone.lastWeather = weather;
            MySingletone.manualResetEvent.WaitOne();
            MySingletone.qwQueue.Enqueue(weather);
        }
    }
}
