using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IConfidentialClientApplication _clientApplication;
        private readonly ILogger<UsersController> _logger;

        public GroupsController(IConfidentialClientApplication clientApplication, ILogger<UsersController> logger)
        {
            _clientApplication = clientApplication;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AuthenticationResult authenticationResult = await _clientApplication
                    .AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" })
                    .ExecuteAsync();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);

                var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/groups");

                return Ok(response.GetContentAs<dynamic>());
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS50049"))
            {
                _logger.LogError(ex, "Incorrect authority configuration");
                return new ObjectResult($"Incorrect authority configuration, {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}/users")]
        public async Task<IActionResult> GetUsers(string id)
        {
            try
            {
                AuthenticationResult authenticationResult = await _clientApplication
                    .AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" })
                    .ExecuteAsync();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);

                var response = await httpClient.GetAsync($"https://graph.microsoft.com/v1.0/groups/{id}/members");
                var content = await response.GetContentAs<Users>();
                var users = content.Value.Where(x => x.type == "#microsoft.graph.user");
                
                return Ok(users);
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS50049"))
            {
                _logger.LogError(ex, "Incorrect authority configuration");
                return new ObjectResult($"Incorrect authority configuration, {ex.Message}");
            }
        }
    }
}