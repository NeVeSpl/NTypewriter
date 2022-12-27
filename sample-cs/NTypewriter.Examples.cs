using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }

    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }

    public class GenericModel<T>
    {
        public T GenericProp { get; set; }
    }

    public record OrderModel(string orderId);

}
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            yield break;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<long>> CreateCustomer(CustomerModel customer)
        {
            return Ok();
        }
    }
}