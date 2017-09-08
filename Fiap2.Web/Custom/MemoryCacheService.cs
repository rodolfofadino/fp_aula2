using System;
using Microsoft.Extensions.Caching.Memory;

namespace Fiap2.Web.Custom
{
    public class MemoryCacheService : ICacheService
    {
        protected IMemoryCache Cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            Cache = cache;
        }

        public void Store(string key, object content)
        {
            Store(key, content, DefaultCacheDuration);
        }

        public void Store(string key, object content, int duration)
        {
            object cached;
            if (Cache.TryGetValue(key, out cached))
            {
                Cache.Remove(key);
            }

            CacheExtensions.Set(Cache, key, content,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(duration),
                    Priority = CacheItemPriority.Low
                });
        }


        private static int DefaultCacheDuration => 60;

        public T Get<T>(string key) where T : class
        {
            object result;
            if (Cache.TryGetValue(key, out result))
            {
                return result as T;
            }
            return null;
        }
    }
}