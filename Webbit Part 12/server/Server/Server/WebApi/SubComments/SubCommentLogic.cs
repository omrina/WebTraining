using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;
using Server.WebApi.Authentication;
using Server.WebApi.Comments;
using Server.WebApi.SubComments.ViewModels;

namespace Server.WebApi.SubComments
{
    public class SubCommentLogic : CommentLogic
    {
        public async Task Post(NewSubCommentViewModel comment)
        {
            EnsureCommentDetails(comment);
            var newComment = new Comment(comment.Content, UserSession.UserId, DateTime.Now);

            await GetCollection().UpdateOneAsync(
                thread => thread.Comments.Any(x => x.Id == ObjectId.Parse(comment.ParentCommentId)),
                Builders<Thread>.Update.AddToSet("Comments.$.comments", newComment));
        }
    }
}