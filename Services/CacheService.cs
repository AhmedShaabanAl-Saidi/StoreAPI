using System.Text.Json;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cachekey)
         => await cacheRepository.GetAsync<string>(cachekey);

        public async Task SetAsync(string cachekey, object value, TimeSpan timeToLive)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await cacheRepository.SetAsync(cachekey, serializedValue, timeToLive);
        }
    }
}
