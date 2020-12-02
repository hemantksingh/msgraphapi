using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace msgraphapi.MsGraph
{
    public class MsGraphClient
    {
        private readonly ILogger<MsGraphClient> _logger;
        private readonly GraphServiceClient _graphServiceClient;

        public MsGraphClient(IConfidentialClientApplication confidentialClientApplication,
            ILogger<MsGraphClient> logger)
        {
            _graphServiceClient = new GraphServiceClient(new ClientCredentialProvider(confidentialClientApplication));
            _logger = logger;
        }


        public async Task<IEnumerable<Domain>> GetSupportedDomains()
        {
            _logger.LogInformation("Getting all Azure AD domains...");

            var domains = new List<Domain>();
            var domainsPage = await _graphServiceClient.Domains
                .Request()
                .GetAsync();
            
            // Get the domains on the first page
            domains.AddRange(domainsPage.CurrentPage.OfType<Microsoft.Graph.Domain>()
                .Select(x => new Domain(x.Id, x.AuthenticationType)));

            // Get the remaining domains. This is least performant way to retrieve data from Graph (or any REST API really).
            // Your app will be sitting there for a long time while it downloads all this data.
            // The proper methodology here is to fetch each page and process just that page before fetching additional data.
            // Realistically, all the domains in an Azure AD should fit into a single page, therefore this should be okay. 
            while (domainsPage.NextPageRequest !=  null)
            {
                domainsPage = await domainsPage.NextPageRequest.GetAsync();
                domains.AddRange(domainsPage.CurrentPage.OfType<Microsoft.Graph.Domain>()
                    .Select(x => new Domain(x.Id, x.AuthenticationType)));
            }

            return domains;
        }
    }
}