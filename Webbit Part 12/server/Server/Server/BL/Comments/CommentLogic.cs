using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Server.BL.Comments.ViewModels;
using Server.BL.Ratings;
using Server.Models;

namespace Server.BL.Comments
{
    public class CommentLogic : BaseLogic<Subwebbit>
    {
        public async Task Post(NewCommentViewModel comment)
        {
            // TODO: add validation???
            var updateInfo = new UpdatingOperation(comment.SubwebbitId, comment.ThreadId);

            if (!string.IsNullOrWhiteSpace(comment.ParentCommentId))
            {
                updateInfo.AddNestedCommentFilter(ObjectId.Parse(comment.ParentCommentId));
            }

            var newComment = new Comment(comment.Author, comment.Content);
                
            await Collection.UpdateOneAsync(updateInfo.Filter,
                UpdateBuilder.AddToSet(updateInfo.ModelHierarchy + "." + nameof(Thread.Comments).ToLower(), newComment),
                updateInfo.Options);
        }

        public async Task<IEnumerable<CommentViewModel>> GetAll(string subwebbitId, string threadId)
        {
            var thread = await Get(subwebbitId).SelectMany(x => x.Threads).Where(GenerateByIdFilter<Thread>(threadId))
                .FirstAsync();
            var comments = thread.Comments.OrderByDescending(x => x.Rating);

            return comments.Select(x => new CommentViewModel(x, subwebbitId, threadId, UserId));
        }
    }
}