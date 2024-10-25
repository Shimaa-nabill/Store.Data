using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
            => await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!isCreated)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
