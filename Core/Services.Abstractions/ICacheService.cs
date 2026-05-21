namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task<string?> GetCacheValueAsync(string cacheKey);
        Task SetCacheValueAsync(string cacheKey, object cacheValue, TimeSpan duration);

        Task<string?> GetStringAsync(string key);
        Task SetStringAsync(string key, string value, TimeSpan duration);
        Task<bool> RemoveAsync(string key);
        Task<long> IncrementAsync(string key);
    }
}
