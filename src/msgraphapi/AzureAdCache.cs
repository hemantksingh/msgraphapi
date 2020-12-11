using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using msgraphapi.MsGraph;

namespace msgraphapi
{
    public class AzureAdCache
    {
        public const string AllGroups = "AllGroups";
        public const string AllDomains = "AllDomains";
        private readonly IPagedCache<Domain> _pagedDomainCache;
        private readonly IPagedCache<Group> _pagedGroupCache;
        private readonly ILogger<AzureAdCache> _logger;
        private static readonly object Padlock = new object();

        public AzureAdCache(IPagedCache<Domain> pagedDomainCache, IPagedCache<Group> pagedGroupCache,
            ILogger<AzureAdCache> logger)
        {
            _pagedDomainCache = pagedDomainCache;
            _pagedGroupCache = pagedGroupCache;
            _logger = logger;
        }

        public void Clear()
        {
            _logger.LogInformation("Clearing all cached items..");
            _pagedDomainCache.RemoveAll();
            _pagedGroupCache.RemoveAll();
        }

        public IEnumerable<Domain> GetDomains(Page page)
        {
            _logger.LogDebug("Requesting page '{pageNumber}' with maximum of '{pageSize}' domains from cache",
                page.Number, page.Size);
            var domains = _pagedDomainCache.GetPage(AllDomains, page.Number, page.Size);
            _logger.LogDebug("'{NoOfDomains}' domain(s) found in the cache on page '{pageNumber}'", domains?.Count ?? 0,
                page.Number);
            return domains;
        }

        public void AddDomains(IEnumerable<Domain> domains)
        {
            _logger.LogDebug("Adding '{NoOfDomains}' domain(s) to the cache", domains.Count());
            lock (Padlock) // make caching updates thread safe
            {
                _pagedDomainCache.Remove(AllDomains);
                _pagedDomainCache.AddItems(AllDomains, domains.ToList());
            }
        }

        public IEnumerable<Group> GetUsersGroups(Upn upn, Page page)
        {
            _logger.LogDebug(
                "Requesting page '{pageNumber} with maximum of '{pageSize}' groups for upn '{upn.Value}' from cache",
                page.Number, page.Size, upn.Value);
            var usersGroups = _pagedGroupCache.GetPage(upn.Value, page.Number, page.Size);
            _logger.LogDebug("'{NoOfGroups}' group(s) found in the cache on page '{pageNumber}'",
                usersGroups?.Count ?? 0, page.Number);
            return usersGroups;
        }

        public void AddUsersGroups(Upn upn, IEnumerable<Group> groups)
        {
            _logger.LogDebug("Adding '{NoOfGroups}' group(s) to the cache", groups.Count());
            lock (Padlock) // make caching updates thread safe
            {
                _pagedGroupCache.Remove(upn.Value);
                _pagedGroupCache.AddItems(upn.Value, groups.ToList());
            }
        }
    }
}