using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;

namespace Server.BL.Subwebbits
{
    public class SubwebbitSubscriptionLogic : BaseLogic<Subwebbit>
    {
        public async Task<IEnumerable<ObjectId>> GetMostSubscribed(int amount)
        {
            return await GetAll().OrderByDescending(x => x.SubscribersCount)
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
            await Collection.UpdateOneAsync(GenerateByIdFilter<Subwebbit>(id),
                UpdateBuilder.Inc(x => x.SubscribersCount, value));
        }
    }
}