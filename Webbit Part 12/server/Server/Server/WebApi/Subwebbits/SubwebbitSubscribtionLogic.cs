using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;

namespace Server.WebApi.Subwebbits
{
    public class SubwebbitSubscriptionLogic : BaseLogic<Subwebbit>
    {
        public async Task<IEnumerable<ObjectId>> GetMostSubscribed(int amount)
        {
            return await GetCollection().AsQueryable().OrderByDescending(x => x.SubscribersCount)
                .Take(amount).Select(x => x.Id).ToListAsync();
        }

        public async Task IncrementSubscribers(string id)
        {
            await AddToSubscribersCount(id, 1);
        }

        public async Task DecrementSubscribers(string id)
        {
            await AddToSubscribersCount(id, -1);
        }

        private async Task AddToSubscribersCount(string id, int value)
        {
            await GetCollection().UpdateOneAsync(x => x.Id == ObjectId.Parse(id),
                UpdateBuilder.Inc(x => x.SubscribersCount, value));
        }
    }
}