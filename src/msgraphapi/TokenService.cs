using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace msgraphapi
{
    public class TokenService
    {
        private readonly IConfidentialClientApplication _clientApplication;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfidentialClientApplication confidentialClientApplication,
            ILogger<TokenService> logger)
        {
            _clientApplication = confidentialClientApplication;
            _logger = logger;
        }

        public async Task<string> GetAccessToken()
        {
            try
            {
                _logger.LogInformation("Requesting access token for confidential client from authority {Authority}",
                    _clientApplication.Authority);
                AuthenticationResult authenticationResult = await _clientApplication
                    .AcquireTokenForClient(new[] {"https://graph.microsoft.com/.default"})
                    .ExecuteAsync();
                _logger.LogDebug("Access token received");
                return authenticationResult.AccessToken;
            }
            catch (MsalUiRequiredException ex)
            {
                throw new ConfigurationException("The application doesn't have sufficient permissions.", ex);
            }

            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                throw new ConfigurationException(
                    "Invalid scope. The scope has to be in the form \"https://resourceurl/.default\"", ex);
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS50049"))
            {
                throw new ConfigurationException("Invalid authority configuration in the MS Identity client", ex);
            }
        }
    }


    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfigurationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}