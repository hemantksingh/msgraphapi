using System;

namespace msgraphapi
{
    public class AzureADClientConfig
    {
        public readonly string ClientId;
        public readonly string ClientSecret;
        public readonly Uri Authority;

        public AzureADClientConfig(string clientId, string clientSecret, string tenantId)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Authority = new Uri($"https://login.microsoftonline.com/{tenantId}");
        }
    }
}
