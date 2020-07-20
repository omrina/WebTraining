using System.Linq;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings.ViewModels
{
    public abstract class BaseVotableViewModel
    {
        public int Rating { get; set; }
        public VoteDirections UserVote { get; set; }

        protected BaseVotableViewModel(IVotable item, ObjectId userId)
        {
            Rating = item.Upvoters.Count() - item.Downvoters.Count();
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