using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lif.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Lif.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(new
            {
                token = LifJwtGenerator.CreateToken(
                    "111",
                    DateTime.Now.AddDays(1),
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F42CEB76-1FD1-471B-8CBE-C732073023FE")),
                        SecurityAlgorithms.HmacSha256Signature)
                )
            });
        }
    }
}