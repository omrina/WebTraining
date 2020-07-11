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
            var filterBuilder = Builders<Subwebbit>.Filter;
            var filterDefinition = filterBuilder.And(
                filterBuilder.Eq(subwebbit => subwebbit.Id, new ObjectId(comment.SubwebbitId)),
                filterBuilder.ElemMatch(subwebbit => subwebbit.Threads,
                    thread => thread.Id == new ObjectId(comment.ThreadId)));

            var updateField = "Threads.$.Comments";
            var arrayFilters = new List<ArrayFilterDefinition>();

            if (!string.IsNullOrWhiteSpace(comment.ParentCommentId))
            {
                updateField += ".$[comment].comments";
                arrayFilters.Add((ArrayFilterDefinition<BsonDocument>) new BsonDocument("comment._id",
                    new BsonDocument("$eq", new ObjectId(comment.ParentCommentId))));
            }

            var newComment = new Comment(comment.Author, comment.Content);
            
            var a = await Collection.UpdateOneAsync(filterDefinition,
                Builders<Subwebbit>.Update.AddToSet(updateField, newComment),
                new UpdateOptions { ArrayFilters = arrayFilters });
            var b = 1;
        }

        public async Task<IEnumerable<CommentViewModel>> GetAll(string subwebbitId, string threadId)
        {
            var thread = await Get(subwebbitId).SelectMany(x => x.Threads).Where(x => x.Id == new ObjectId(threadId))
                .FirstAsync();
            var comments = thread.Comments.OrderByDescending(x => x.Rating);

            return comments.Select(x => new CommentViewModel(x, subwebbitId, threadId));
        }
    }
}