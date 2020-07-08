using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;

namespace Server.Logic
{
    public class UserLogic : BaseLogic<User>
    {
        public async Task<IEnumerable<ObjectId>> GetSubscribedIds(string userId)
        {
            return await Get(userId).SelectMany(x => x.SubscribedSubwebbits).ToListAsync();
        }

        public async Task<bool> IsSubscribed(string userId, string subwebbitId)
        {
            return (await GetSubscribedIds(userId)).Contains(new ObjectId(subwebbitId));
        }

        public async Task Subscribe(string userId, string subwebbitId)
        {
            await Collection.UpdateOneAsync(GenerateByIdFilter(userId),
                Builders<User>.Update.AddToSet(x => x.SubscribedSubwebbits, new ObjectId(subwebbitId)));
            await new SubwebbitLogic().IncrementSubscribers(subwebbitId);
        }

        public async Task Unsubscribe(string userId, string subwebbitId)
        {
            await Collection.UpdateOneAsync(GenerateByIdFilter(userId),
                Builders<User>.Update.Pull(x => x.SubscribedSubwebbits, new ObjectId(subwebbitId)));
            await new SubwebbitLogic().DecrementSubscribers(subwebbitId);
        }
    }
}