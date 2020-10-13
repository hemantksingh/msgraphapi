using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController: ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var builder = new StringBuilder();
            foreach (var requestHeader in Request.Headers)
            {    
                builder.AppendLine($"{requestHeader.Key}: {requestHeader.Value} ");
            }

            Console.WriteLine("Listing the request headers:");
            Console.WriteLine(builder);

            return Ok("Healthy");
        }

        [HttpGet]
        [Route("dependencies")]
        public IActionResult GetConnections()
        {
            var redisConnection = new RedisConnection();
            redisConnection.Add("foo", "bar");

            return Ok(redisConnection.Get("foo"));
        }
    }
}
