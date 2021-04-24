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
    public class UrlController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UrlController> _logger;

        public UrlController(ILogger<UrlController> logger)
        {           
            _logger = logger;
        }

        [HttpGet("foo")]
        public IEnumerable<WeatherForecast> WithSimpleTypeInBody([FromServices] ILogger<UrlController> logger, [FromBody] int body)
        {
            return null;
        }

        [HttpGet("foo/{url}")]
        public ActionResult<IEnumerable<WeatherForecast>> WithParameterInPath([FromServices] ILogger<UrlController> logger, int url)
        {
            return null;
        }

        [HttpGet("foo")]
        public ActionResult<IEnumerable<WeatherForecast>> WithComplexTypeInQuery([FromQuery] Paggination pagg)
        {
            return null;
        }

        [HttpPut]
        [AcceptVerbs("put", "get")]
        [Route("~/sd", Name ="some_name")]
        public async Task<IEnumerable<WeatherForecast>> WithRouteAttribute(InputDTO body)
        {
            return null;
        }


        [HttpDelete("[action]/{par1:double}/{par2=false}/{par3?}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ActionName("akacja")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> SomeComplexTest(int par3, InputDTO body, double par1, bool par2, int par4, int par5)
        {
            return null;
        }
        
        public IActionResult WithEnumParam(NumbersEnum numbers)
        {
            return null;
        }

        public IActionResult WithOptionalParam(int? optional)
        {
            return null;
        }
    }
}