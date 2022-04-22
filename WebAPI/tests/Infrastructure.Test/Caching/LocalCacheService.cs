using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<WebAPI.Infrastructure.Caching.LocalCacheService>
{
    protected override WebAPI.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<WebAPI.Infrastructure.Caching.LocalCacheService>.Instance);
}