using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings
{
    public class ItemVoter
    {
        private IVotable Item { get; }
        private ObjectId UserId { get; }

        public ItemVoter(IVotable item, ObjectId userId)
        {
            Item = item;
            UserId = userId;
        }

        public void Vote(VoteStates vote)
        {
            var previousVote = GetUserVote();

            Item.Upvoters.Remove(UserId);
            Item.Downvoters.Remove(UserId);

            if (previousVote != vote)
            {
                (vote == VoteStates.Up ? Item.Upvoters : Item.Downvoters)
                    .Add(UserId);
            }
        }

        public VoteStates GetUserVote()
        {
            return Item.Upvoters.Contains(UserId)
                ? VoteStates.Up
                : Item.Downvoters.Contains(UserId)
                    ? VoteStates.Down
                    : VoteStates.Cancel;
        }
    }
}