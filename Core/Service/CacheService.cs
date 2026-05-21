using Domain.Contracts;
using Services.Abstractions;

namespace Service
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCacheValueAsync(string cacheKey)
           => await _cacheRepository.GetCacheAsync(cacheKey);

        public async Task SetCacheValueAsync(string cacheKey, object cacheValue, TimeSpan duration)
            => await _cacheRepository.SetCacheAsync(cacheKey, cacheValue, duration);

        public async Task<string?> GetStringAsync(string key)
            => await _cacheRepository.GetStringAsync(key);

        public async Task SetStringAsync(string key, string value, TimeSpan duration)
            => await _cacheRepository.SetStringAsync(key, value, duration);

        public async Task<bool> RemoveAsync(string key)
            => await _cacheRepository.RemoveAsync(key);

        public async Task<long> IncrementAsync(string key)
            => await _cacheRepository.IncrementAsync(key);
    }
}
