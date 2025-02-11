using Microsoft.Extensions.Caching.Memory;
using Order.Application;

namespace Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public void Add<T>(string key, T value, TimeSpan expirationTime)
        {
            memoryCache.Set<T>(key, value, expirationTime);
        }

        public T Get<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }
    }
}
