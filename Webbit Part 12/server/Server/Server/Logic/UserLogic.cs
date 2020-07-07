using System.Collections.Generic;
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
    }
}