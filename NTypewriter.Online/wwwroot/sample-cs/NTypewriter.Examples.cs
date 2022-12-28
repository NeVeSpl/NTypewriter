using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class WeatherForecast : BaseModel
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

    public class BaseModel { };

    public class GenericModel<T>
    {
        public T GenericProp { get; set; }
    }

    public record OrderModel(string orderId, Numbers number);

    public enum Numbers { One, Two, Three, Seven = 7 };

    public class OffsetPagination
    {
        public int Offset { get; set; } = 0;
        [Range(0, 100)]
        public int Limit { get; set; } = 10;
    }
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
        public async Task<ActionResult<long>> CreateCustomer([FromServices] UsersService userService, CustomerModel customer)
        {
            return Ok();
        }

        [HttpGet("EmailAddress/IsTaken")]
        public async Task<ActionResult<bool>> IsEmailAddressTaken(string emailAddress)
        {
            return Ok(true);
        }

        [HttpGet]
        public async Task<ActionResult<CursorPagedResults<UserDTO>>> GetCustomers([FromQuery] OffsetPagination pagination)
        {
            yield break;
        }
    }
}