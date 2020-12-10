using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace msgraphapi.MsGraph
{
    public class DomainsService
    {
        private readonly AzureAdCache _azureAdCache;
        private readonly MsGraphClient _graphServiceClient;

        public DomainsService(AzureAdCache azureAdCache, MsGraphClient graphServiceClient)
        {
            _azureAdCache = azureAdCache;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<IEnumerable<Domain>> GetDomainsAsync(Page page)
        {
            var cachedDomains = _azureAdCache.GetDomains(page);
            if (cachedDomains != null && cachedDomains.Any())
                return cachedDomains;

            var domains = await _graphServiceClient.GetDomainsAsync();
            _azureAdCache.AddDomains(domains);

            return domains.Skip(page.Size * (page.Number - 1)).Take(page.Size);
        }
    }
}
