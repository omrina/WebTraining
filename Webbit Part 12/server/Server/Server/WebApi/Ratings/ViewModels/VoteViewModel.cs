using System.Linq;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings.ViewModels
{
    public class VoteViewModel
    {
        public int Rating { get; set; }
        public VoteStates UserVote { get; set; }

        public VoteViewModel(IVotable item, ObjectId userId)
        {
            Rating = item.Upvoters.Count() - item.Downvoters.Count();
            UserVote = GetUserVote(item, userId);
        }

        private VoteStates GetUserVote(IVotable item, ObjectId userId)
        {
            return item.Upvoters.Contains(userId)
                ? VoteStates.Up
                : item.Downvoters.Contains(userId)
                    ? VoteStates.Down
                    : VoteStates.Cancel;
        }
    }
}