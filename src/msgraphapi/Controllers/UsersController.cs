using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace msgraphapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(TokenService tokenService, ILogger<UsersController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _tokenService.GetAccessToken());

            _logger.LogInformation("Requesting users 'https://graph.microsoft.com/v1.0/users'");
            var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/users");
            return Ok(response.GetContentAs<dynamic>());
        }
    }
}