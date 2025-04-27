using System.Text.Json;
using Domain.Contracts;
using Domain.Entities;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public Task<bool> DeleteBasketAsync(string Id)
        => _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var basket = await _database.StringGetAsync(Id);
            if (basket.IsNullOrEmpty) 
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, serializedBasket, timeToLive ?? TimeSpan.FromDays(30));
            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
