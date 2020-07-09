using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class RatingLogic : BaseLogic<Subwebbit>
    {
        public async Task Vote(VoteOnItemViewModel voteInfo)
        {
            // TODO: use hierarchy of id's
            var a = GetAll().SelectMany(x => x.Threads).FirstOrDefault(x => x.Id == new ObjectId(voteInfo.ItemId));
        }
    }
}