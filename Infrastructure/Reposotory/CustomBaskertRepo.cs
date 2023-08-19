using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Reposotory
{
    public class CustomBaskertRepo : IBasketRepo
    {
        private IDatabase _database;
        public CustomBaskertRepo(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> DeleteCustomBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<customBasket> GetAllCustomBasket(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<customBasket>(data);
        }

        public async Task<customBasket> UpdateCustomBasket(customBasket customBasketItems)
        {
            var created = await _database.StringSetAsync(customBasketItems.Id, JsonSerializer.Serialize(customBasketItems), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetAllCustomBasket(customBasketItems.Id);
        }

       

       
    }
}
