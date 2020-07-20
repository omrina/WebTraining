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

        public async Task IncrementSubscribers(ObjectId id)
        {
            await AddToSubscribersCount(id, 1);
        }

        public async Task DecrementSubscribers(ObjectId id)
        {
            await AddToSubscribersCount(id, -1);
        }

        private async Task AddToSubscribersCount(ObjectId id, int value)
        {
            await GetCollection().UpdateOneAsync(x => x.Id == id,
                UpdateBuilder.Inc(x => x.SubscribersCount, value));
        }
    }
}