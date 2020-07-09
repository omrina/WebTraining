using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class RatingLogic : BaseLogic<Subwebbit>
    {
        public async Task Vote(VoteOnItemViewModel voteInfo)
        {
            // TODO: preform graphLookup to search for nested comments
            // --> To: thread.rootComments.subcomments
            // --> From: thread.rootComments.id
            // Collection.Aggregate().GraphLookup(x => x.Threa, null, x => x.Threads).FirstOrDefaultAsync();

            var a = GetAll().SelectMany(x => x.Threads).FirstOrDefault(x => x.Id == new ObjectId(voteInfo.ItemId));
        }
    }
}