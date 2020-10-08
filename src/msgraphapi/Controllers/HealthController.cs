using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}
