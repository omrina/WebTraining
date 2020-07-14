using System.Linq;
using MongoDB.Bson;
using Server.BL.Ratings.Enums;
using Server.Models;

namespace Server.BL.Ratings.ViewModels
{
    public abstract class BaseVotableViewModel
    {
        public int Rating { get; set; }
        public VoteDirections UserVote { get; set; }

        protected BaseVotableViewModel(IVotable item, ObjectId userId)
        {
            Rating = item.Rating;
            UserVote = GetUserVote(item, userId);
        }

        private VoteDirections GetUserVote(IVotable item, ObjectId userId)
        {
            return item.Upvoters.Contains(userId)
                ? VoteDirections.Up
                : item.Downvoters.Contains(userId)
                    ? VoteDirections.Down
                    : VoteDirections.Cancel;
        }
    }
}