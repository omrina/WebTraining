using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class CommentLogic : BaseLogic<Subwebbit>
    {
        public async Task Post(NewCommentViewModel comment)
        {
            // TODO: add validation???
            var updateField = "Threads.$.Comments";
            var arrayFilters = new List<ArrayFilterDefinition>();

            if (!string.IsNullOrWhiteSpace(comment.ParentCommentId))
            {
                updateField += ".$[comment].comments";
                arrayFilters.Add((ArrayFilterDefinition<BsonDocument>) new BsonDocument("comment._id",
                    new BsonDocument("$eq", new ObjectId(comment.ParentCommentId))));
            }

            var newComment = new Comment(comment.Author, comment.Content);
            var threadFilter = new ThreadLogic()
                .GetThreadFilterDefinition(comment.SubwebbitId, comment.ThreadId);

            await Collection.UpdateOneAsync(threadFilter,
                UpdateBuilder.AddToSet(updateField, newComment),
                new UpdateOptions {ArrayFilters = arrayFilters});
        }

        public async Task<IEnumerable<CommentViewModel>> GetAll(string subwebbitId, string threadId, string userId)
        {
            var thread = await Get(subwebbitId).SelectMany(x => x.Threads).Where(x => x.Id == new ObjectId(threadId))
                .FirstAsync();
            var comments = thread.Comments.OrderByDescending(x => x.Rating);

            return comments.Select(x => new CommentViewModel(x, subwebbitId, threadId, new ObjectId(userId)));
        }
    }
}