using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace msgraphapi
{
    public class TokenService
    {
        private readonly IConfidentialClientApplication _clientApplication;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfidentialClientApplication clientApplication, ILogger<TokenService> logger)
        {
            _clientApplication = clientApplication;
            _logger = logger;
        }

        public async Task<string> GetToken()
        {
            try
            {
                AuthenticationResult authenticationResult = await _clientApplication
                    .AcquireTokenForClient(new[] {"https://graph.microsoft.com/.default"})
                    .ExecuteAsync();
                return authenticationResult.AccessToken;
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS50049"))
            {
                _logger.LogError(ex, "Incorrect authority configuration in the client");
                throw;
            }
        }
    }
}
