using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class RatingLogic : BaseLogic<Subwebbit>
    {
        public async Task Vote(VoteOnItemViewModel voteInfo)
        {
            // TODO: use hierarchy of id's
            var a = await GetAll().SelectMany(x => x.Threads).FirstOrDefaultAsync(x => x.Id == new ObjectId(voteInfo.ItemId));
        }
    }
}