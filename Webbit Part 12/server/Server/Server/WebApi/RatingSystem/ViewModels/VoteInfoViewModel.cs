using MongoDB.Bson;
using Server.Models;
using Server.WebApi.RatingSystem.Enums;

namespace Server.WebApi.RatingSystem.ViewModels
{
    public class VoteInfoViewModel
    {
        public int Rating { get; set; }
        public VoteStates UserVote { get; set; }

        public VoteInfoViewModel(IVotable item, ObjectId userId)
        {
            Rating = item.Upvoters.Count - item.Downvoters.Count;
            UserVote = new ItemVoter(item, userId).GetUserVote();
        }
    }
}