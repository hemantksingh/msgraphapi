using System;
using Microsoft.AspNetCore.Mvc;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new InvalidOperationException("You have failed");

        }
    }
}
