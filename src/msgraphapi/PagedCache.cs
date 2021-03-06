﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace msgraphapi
{
    public interface IPagedCache<T> : IDisposable
    {
        PagedCache<T> AddItems(string key, List<T> items);
        void Remove(string key);
        void RemoveAll();
        List<T> GetPage(string key, int pageNumber, int pageSize);
    }

    public sealed class PagedCache<T> : IPagedCache<T>
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;
        private readonly ILogger<PagedCache<T>> _logger;

        public PagedCache(IMemoryCache cache, MemoryCacheEntryOptions memoryCacheEntryOptions,
            ILogger<PagedCache<T>> logger)
        {
            _cache = cache;
            _memoryCacheEntryOptions = memoryCacheEntryOptions;
            _logger = logger;
        }

        public PagedCache<T> AddItems(string key, List<T> items)
        {
            if (_cache.TryGetValue(key, out List<T> list))
            {
                _logger.LogTrace("Adding '{NoOfItems}' {ItemType}(s) to existing cached entries with key '{CacheKey}'",
                    items.Count, typeof(T).Name, key);
                list.AddRange(items);
            }
            else
            {
                _logger.LogTrace("Adding '{NoOfItems}' {ItemType}(s) to new cache entry with key '{CacheKey}'",
                    items.Count, typeof(T).Name, key);
                list = items;
            }

            _cache.Set(key, list, _memoryCacheEntryOptions);
            return this;
        }

        public void Remove(string key)
        {
            _logger.LogTrace("Removing key '{CacheKey}' and all associated items from cache", key);
            _cache.Remove(key);
        }

        public void RemoveAll()
        {
            _logger.LogTrace("Evicting all cache entries");
            ((MemoryCache) _cache).Compact(1.0);
            _logger.LogDebug("Cache item count: {CacheItemCount}", ((MemoryCache) _cache).Count);
        }

        public List<T> GetPage(string key, int pageNumber, int pageSize)
        {
            // Return null if not in the cache, return empty list if pageNumber is out of range
            if (!_cache.TryGetValue(key, out List<T> list))
            {
                _logger.LogTrace("No '{ItemType}(s)' for key '{CacheKey}' found in cache", typeof(T).Name, key);
                return null;
            }

            _logger.LogTrace("'{ItemCount}' '{ItemType}(s)' for key '{CacheKey}' found in cache", list.Count,
                typeof(T).Name, key);
            var page = list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            _logger.LogTrace("Returning '{ItemCount}' of the top '{PageSize}' '{ItemType}(s)' on page '{PageNumber}'",
                list.Count, pageSize, typeof(T).Name, pageNumber);
            return page;
        }

        public void Dispose()
        {
            _logger.LogDebug($"Disposing of {nameof(PagedCache<T>)}.");
            _cache?.Dispose();
        }
    }
}