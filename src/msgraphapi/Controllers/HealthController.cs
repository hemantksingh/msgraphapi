using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController: ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var builder = new StringBuilder();
            foreach (var requestHeader in Request.Headers)
            {    
                builder.AppendLine($"{requestHeader.Key}: {requestHeader.Value} ");
            }

            _logger.LogInformation("Listing the request headers:");
            _logger.LogInformation(builder.ToString());

            return Ok("Healthy");
        }
    }
}
