using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;
using Server.WebApi.Comments;
using Server.WebApi.SubComments.ViewModels;

namespace Server.WebApi.SubComments
{
    public class SubCommentLogic : CommentLogic<NewSubCommentViewModel>
    {
        protected override async Task AddComment(Comment comment, NewSubCommentViewModel commentInfo)
        {
            var commentsFieldHierarchy = $"{nameof(Thread.Comments)}.$.{nameof(Comment.Comments).ToLower()}";

            await GetCollection().UpdateOneAsync(
                Builders<Thread>.Filter.And(
                    Builders<Thread>.Filter.Eq(x => x.Id, ObjectId.Parse(commentInfo.ThreadId)),
                    Builders<Thread>.Filter.ElemMatch(x => x.Comments,
                                                      x => x.Id == ObjectId.Parse(commentInfo.ParentCommentId))),
                Builders<Thread>.Update.AddToSet(commentsFieldHierarchy, comment));
        }
    }
}