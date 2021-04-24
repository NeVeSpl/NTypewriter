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
    public class BodyParameterController : ControllerBase
    {
        private readonly ILogger<ParamtersController> _logger;

        public BodyParameterController(ILogger<ParamtersController> logger)
        {

            _logger = logger;
        }



        [HttpGet("Foo1")]
        public IEnumerable<WeatherForecast> WithFromServicesAttr([FromServices] ILogger<ParamtersController> logger)
        {
            return null;
        }

        [HttpGet("Foo2")]
        public IEnumerable<WeatherForecast> WithPrimitiveTypeInBody([FromBody] int body)
        {
            return null;
        }

        [HttpGet("Foo3")]
        public IEnumerable<WeatherForecast> WithPrimitiveType(int body)
        {
            return null;
        }

        [HttpGet("Foo4")]
        public IEnumerable<WeatherForecast> WithSimpleType(DateTime body)
        {
            return null;
        }

        [HttpGet("Foo5")]
        public IEnumerable<WeatherForecast> WithSimpleTypeInBody([FromBody] DateTime body)
        {
            return null;
        }

        [HttpGet("Foo6")]
        public async Task<IEnumerable<WeatherForecast>> WithCompexTypeInQuery([FromQuery] Paggination pagg)
        {
            return null;
        }

        [HttpGet("Foo7")]
        public async Task<IEnumerable<WeatherForecast>> WithCompexType(InputDTO body)
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
    }
}