using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public GroupsController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());

            var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/groups");

            return Ok(response.GetContentAs<dynamic>());
        }

        [HttpGet]
        [Route("{id}/users")]
        public async Task<IActionResult> GetUsers(string id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());

            var response = await httpClient.GetAsync($"https://graph.microsoft.com/v1.0/groups/{id}/members");
            var content = await response.GetContentAs<Users>();
            var users = content.Value.Where(x => x.type == "#microsoft.graph.user");

            return Ok(users);
        }
    }
}