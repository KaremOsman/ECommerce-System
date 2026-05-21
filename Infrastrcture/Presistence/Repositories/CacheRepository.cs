using Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        private static readonly JsonSerializerOptions _serializeOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<string?> GetCacheAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? default : value;
        }

        public async Task SetCacheAsync(string cacheKey, object cacheValue, TimeSpan duration)
        {
            var value = JsonSerializer.Serialize(cacheValue, _serializeOptions);
            await _database.StringSetAsync(cacheKey, value, duration);
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value.ToString();
        }

        public async Task SetStringAsync(string key, string value, TimeSpan duration)
        {
            await _database.StringSetAsync(key, value, duration);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<long> IncrementAsync(string key)
        {
            return await _database.StringIncrementAsync(key);
        }
    }
}
