using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Enums;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class RatingLogic : BaseLogic<Subwebbit>
    {
        public async Task Vote(UserVoteViewModel voteInfo, ObjectId userId)
        {
            var threadFilter = new ThreadLogic().GetThreadFilterDefinition(voteInfo.SubwebbitId,
                                                                           voteInfo.ThreadId);
            var itemDefinition = "Threads.$";

            // TODO: move all this comment fiddling somewhere else!
            var arrayFilters = new List<ArrayFilterDefinition>();
            if (!string.IsNullOrWhiteSpace(voteInfo.ParentCommentId))
            {
                itemDefinition += ".comments.$[parentComment]";
                arrayFilters.Add((ArrayFilterDefinition<BsonDocument>)new BsonDocument("parentComment._id",
                    new BsonDocument("$eq", new ObjectId(voteInfo.ParentCommentId))));
            }

            if (!string.IsNullOrWhiteSpace(voteInfo.CommentId))
            {
                itemDefinition += ".comments.$[comment]";
                arrayFilters.Add((ArrayFilterDefinition<BsonDocument>)new BsonDocument("comment._id",
                    new BsonDocument("$eq", new ObjectId(voteInfo.CommentId))));
            }

            // TODO: change everywhere to nameof
            var updateInfo = new UpdateOperationDto(threadFilter, itemDefinition, arrayFilters);
            await UpdateRating(updateInfo, voteInfo.PreviousDirection, voteInfo.NewDirection);
            await UpdateUserInVotersLists(updateInfo,
                                          voteInfo.PreviousDirection,
                                          voteInfo.NewDirection,
                                          userId);
        }

        private async Task UpdateRating(UpdateOperationDto updateInfo,
            VoteDirections previousDirection,
            VoteDirections newDirection)
        {
            var votesAmountToAdd = GetVotesAmountToAdd(previousDirection,
                newDirection);

            await Collection.UpdateOneAsync(updateInfo.Filter,
                UpdateBuilder.Inc(updateInfo.Item + ".rating", votesAmountToAdd),
                updateInfo.Options);
        }

        private int GetVotesAmountToAdd(VoteDirections previousDirection,
                                        VoteDirections newDirection)
        {
            return newDirection == previousDirection
                    ? (int) newDirection * -1
                    : newDirection - previousDirection;
        }

        private async Task UpdateUserInVotersLists(UpdateOperationDto updateInfo,
                                                   VoteDirections previousDirection,
                                                   VoteDirections newDirection,
                                                   ObjectId userId)
        {
            //TODO: make previous and new a single change-directions dto???
            await Collection.UpdateOneAsync(updateInfo.Filter,
                UpdateBuilder.Pull(updateInfo.Item + ".upvoters", userId),
                updateInfo.Options);

            await Collection.UpdateOneAsync(updateInfo.Filter,
                UpdateBuilder.Pull(updateInfo.Item + ".downvoters", userId),
                updateInfo.Options);

            if (newDirection != previousDirection)
            {
                await Collection.UpdateOneAsync(updateInfo.Filter,
                    UpdateBuilder.AddToSet(updateInfo.Item + "." + GetVotersListName(newDirection), userId),
                    updateInfo.Options);
            }
        }

        private string GetVotersListName(VoteDirections voteDirections)
        {
            return voteDirections == VoteDirections.Down
                ? "downvoters"
                : "upvoters";
        }

        public VoteDirections GetUserCurrentVote()
        {
            // var thread = Get(voteInfo.SubwebbitId).SelectMany(x => x.Threads)
            // .Where(GenerateByIdFilter<Thread>(voteInfo.ThreadId));
            // var currentUserVote = upvotersIds.Contains(userId)
            // ? VoteDirections.Up
            // : downvotersIds.Contains(userId)
            // ? VoteDirections.Down
            // : VoteDirections.Cancel;
            return VoteDirections.Down;
        }
    }
}