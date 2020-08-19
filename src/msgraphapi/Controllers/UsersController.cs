using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace msgraphapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfidentialClientApplication _clientApplication;

        public UsersController(IConfidentialClientApplication clientApplication, ILogger<UsersController> logger)
        {
            _logger = logger;
            _clientApplication = clientApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AuthenticationResult authenticationResult = await _clientApplication
                    .AcquireTokenForClient(new[] {"https://graph.microsoft.com/.default"})
                    .ExecuteAsync();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);

                var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/users");
                return Ok(response.GetContentAs<dynamic>());
            }
            catch (MsalServiceException ex) when(ex.Message.Contains("AADSTS50049"))
            {
                _logger.LogError(ex, "Incorrect authority configuration");
                return new ObjectResult($"Incorrect authority configuration, {ex.Message}");
            }
        }
    }
}