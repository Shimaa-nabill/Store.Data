using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string key)
        {
            var cacheResponse = await _database.StringGetAsync(key);

            if (cacheResponse.IsNullOrEmpty)
                return null!;

            return cacheResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }
    }
}
