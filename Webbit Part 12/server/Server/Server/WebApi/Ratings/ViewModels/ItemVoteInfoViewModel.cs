using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings.ViewModels
{
    public class ItemVoteInfoViewModel
    {
        public int Rating { get; set; }
        public VoteStates UserVote { get; set; }

        public ItemVoteInfoViewModel(IVotable item, ObjectId userId)
        {
            Rating = item.Upvoters.Count - item.Downvoters.Count;
            UserVote = new ItemVoter(item, userId).GetUserVote();
        }
    }
}