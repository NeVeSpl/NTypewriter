using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tests.Assets.WebApi.Controllers
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

        [HttpGet("hkk")]
        public IEnumerable<WeatherForecast> GetData([FromServices] ILogger<WeatherForecastController> logger, [FromBody] int body)
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

        [HttpGet("hkk/{url}")]

        public ActionResult<IEnumerable<WeatherForecast>> GetDataNoBody([FromServices] ILogger<WeatherForecastController> logger, int url)
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

        [HttpPut]
        [AcceptVerbs("put", "get")]
        [Route("~/sd", Name ="some_name")]
        public async Task<IEnumerable<WeatherForecast>> SomeAsync(InputDTO body, [FromQuery] Paggination pagg)
        {
            return await Task.FromResult(GetData(null, 7));
        }

        [HttpDelete("[action]/{par1:double}/{par2=false}/{par3?}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ActionName("akacja")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> SomeAsync2(int par3, InputDTO body, double par1, bool par2, int par4, int par5)
        {
            await Task.FromResult(5);
            return Ok(GetData(null, 666));
        }

        //[ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        public IActionResult ActionWithEnumParam(Numbers numbers, int? optional, DateTime date)
        {
            return Ok(GetData(null, 666).FirstOrDefault());
        }
    }

    public class InputDTO
    {

    }

    public class Paggination
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }

    public enum Numbers 
    {
        Five,
        Seven,
    }
}
