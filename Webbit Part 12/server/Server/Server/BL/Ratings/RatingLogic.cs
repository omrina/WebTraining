using System.Threading.Tasks;
using MongoDB.Bson;
using Server.BL.Ratings.Enums;
using Server.BL.Ratings.ViewModels;
using Server.Models;

namespace Server.BL.Ratings
{
    public class RatingLogic : BaseLogic<Subwebbit>
    {
        public async Task Vote(UserVoteViewModel voteInfo)
        {
            var updateInfo = new UpdatingOperation(voteInfo.SubwebbitId, voteInfo.ThreadId);

            if (!string.IsNullOrWhiteSpace(voteInfo.ParentCommentId))
            {
                updateInfo.AddNestedCommentFilter(ObjectId.Parse(voteInfo.ParentCommentId));
            }

            if (!string.IsNullOrWhiteSpace(voteInfo.CommentId))
            {
                updateInfo.AddNestedCommentFilter(ObjectId.Parse(voteInfo.CommentId));
            }

            await UpdateRating(updateInfo, voteInfo.VoteChange);
            await UpdateUserInVotersLists(updateInfo, voteInfo.VoteChange);
        }

        private async Task UpdateRating(UpdatingOperation updatingInfo,
                                        VoteChangeViewModel voteChange)
        {
            var votesAmountToAdd = GetVotesAmountToAdd(voteChange);

            await Collection.UpdateOneAsync(updatingInfo.Filter,
                UpdateBuilder.Inc(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Rating).ToLower(),
                                  votesAmountToAdd),
                updatingInfo.Options);
        }

        private int GetVotesAmountToAdd(VoteChangeViewModel voteChange)
        {
            return voteChange.NewDirection == voteChange.PreviousDirection
                    ? (int) voteChange.NewDirection * -1
                    : voteChange.NewDirection - voteChange.PreviousDirection;
        }

        private async Task UpdateUserInVotersLists(UpdatingOperation updatingInfo,
                                                   VoteChangeViewModel voteChange)
        {
            await Collection.UpdateOneAsync(updatingInfo.Filter,
                UpdateBuilder.Pull(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Upvoters).ToLower(), UserId),
                updatingInfo.Options);

            await Collection.UpdateOneAsync(updatingInfo.Filter,
                UpdateBuilder.Pull(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Downvoters).ToLower(), UserId),
                updatingInfo.Options);

            if (voteChange.NewDirection != voteChange.PreviousDirection)
            {
                await Collection.UpdateOneAsync(updatingInfo.Filter,
                    UpdateBuilder.AddToSet(updatingInfo.ModelHierarchy + "." + GetVotersListName(voteChange.NewDirection).ToLower(), UserId),
                    updatingInfo.Options);
            }
        }

        private string GetVotersListName(VoteDirections voteDirections)
        {
            return voteDirections == VoteDirections.Down
                ? nameof(IVotable.Downvoters)
                : nameof(IVotable.Upvoters);
        }
    }
}