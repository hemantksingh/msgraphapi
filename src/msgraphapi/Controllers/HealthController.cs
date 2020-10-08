using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController: ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            foreach (var requestHeader in Request.Headers)
            {
                Console.WriteLine();
                Console.WriteLine("Listing the request headers:");
                Console.WriteLine($"${requestHeader.Key}: {requestHeader.Value} ");
            }

            return Ok("Healthy");
        }


    }
}
