using Server.Models;
using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings
{
    public class RatingLogic : BaseLogic<Thread>
    {
        // public async Task Vote(UserVoteViewModel voteInfo)
        // {
        //     // TODO: check voting works!
        //     var updateInfo = new UpdatingOperation(voteInfo.ThreadId);
        //
        //     if (!string.IsNullOrWhiteSpace(voteInfo.ParentCommentId))
        //     {
        //         updateInfo.AddNestedCommentFilter(ObjectId.Parse(voteInfo.ParentCommentId));
        //     }
        //
        //     if (!string.IsNullOrWhiteSpace(voteInfo.CommentId))
        //     {
        //         updateInfo.AddNestedCommentFilter(ObjectId.Parse(voteInfo.CommentId));
        //     }
        //
        //     await UpdateUserInVotersLists(updateInfo, voteInfo.VoteChange);
        //     // TODO: return after-vote info (new rating, user's vote direction?)
        //     // await UpdateRating(updateInfo, voteInfo.VoteChange);
        // }
        //
        // private async Task UpdateUserInVotersLists(UpdatingOperation updatingInfo,
        //     VoteChangeViewModel voteChange)
        // {
        //     await GetCollection().UpdateOneAsync(updatingInfo.Filter,
        //         UpdateBuilder.Pull(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Upvoters).ToLower(), UserSession.UserId),
        //         updatingInfo.Options);
        //
        //     await GetCollection().UpdateOneAsync(updatingInfo.Filter,
        //         UpdateBuilder.Pull(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Downvoters).ToLower(), UserSession.UserId),
        //         updatingInfo.Options);
        //
        //     if (voteChange.NewType != voteChange.PreviousType)
        //     {
        //         await GetCollection().UpdateOneAsync(updatingInfo.Filter,
        //             UpdateBuilder.AddToSet(updatingInfo.ModelHierarchy + "." + GetVotersListName(voteChange.NewType).ToLower(), UserSession.UserId),
        //             updatingInfo.Options);
        //     }
        // }

        private string GetVotersListName(VoteStates voteStates)
        {
            return voteStates == VoteStates.Down
                ? nameof(IVotable.Downvoters)
                : nameof(IVotable.Upvoters);
        }

        // private async Task UpdateRating(UpdatingOperation updatingInfo,
        //                                 VoteChangeViewModel voteChange)
        // {
        //     var votesAmountToAdd = GetVotesAmountToAdd(voteChange);
        //
        //     await GetCollection().UpdateOneAsync(updatingInfo.Filter,
        //         UpdateBuilder.Inc(updatingInfo.ModelHierarchy + "." + nameof(IVotable.Rating).ToLower(),
        //                           votesAmountToAdd),
        //         updatingInfo.Options);
        // }

        // private int GetVotesAmountToAdd(VoteChangeViewModel voteChange)
        // {
        //     return voteChange.NewType == voteChange.PreviousType
        //             ? (int) voteChange.NewType * -1
        //             : voteChange.NewType - voteChange.PreviousType;
        // }


    }
}