using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace msgraphapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public UsersController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _tokenService.GetAccessToken());

            var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/users");
            return Ok(response.GetContentAs<dynamic>());
        }
    }
}