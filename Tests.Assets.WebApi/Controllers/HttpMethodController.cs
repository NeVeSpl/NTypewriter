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
    public class HttpMethodController : ControllerBase
    {
        public IEnumerable<WeatherForecast> Default([FromServices] ILogger<ParamtersController> logger, [FromBody] int body)
        {
            return null;
        }

        [HttpGet("hkk/{url}")]

        public ActionResult<IEnumerable<WeatherForecast>> Get([FromServices] ILogger<ParamtersController> logger, int url)
        {
            return null;
        }

        [HttpPut]
        [AcceptVerbs("put", "get")]
        [Route("~/sd", Name = "some_name")]
        public async Task<IEnumerable<WeatherForecast>> Put(InputDTO body, [FromQuery] Paggination pagg)
        {
            return null;
        }

        [HttpDelete("[action]/{par1:double}/{par2=false}/{par3?}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ActionName("akacja")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Delete(int par3, InputDTO body, double par1, bool par2, int par4, int par5)
        {
            return null;
        }
    }
}