namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetCacheAsync(string cacheKey);
        Task SetCacheAsync(string cacheKey, object cacheValue, TimeSpan duration);

        Task<string?> GetStringAsync(string key);
        Task SetStringAsync(string key, string value, TimeSpan duration);
        Task<bool> RemoveAsync(string key);
        Task<long> IncrementAsync(string key);
    }
}
