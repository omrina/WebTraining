using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Exceptions;
using Server.Models;
using Server.WebApi.Authentication;
using Server.WebApi.Comments.Validation;
using Server.WebApi.Comments.ViewModels;
using Server.WebApi.Ratings;

namespace Server.WebApi.Comments
{
    // TODO: inherit from thread logic???
    public class CommentLogic : BaseLogic<Thread>
    {
        public async Task Post(NewCommentViewModel comment)
        {
            EnsureCommentDetails(comment);
            var updateInfo = new UpdatingOperation(comment.ThreadId);

            if (!string.IsNullOrWhiteSpace(comment.ParentCommentId))
            {
                updateInfo.AddNestedCommentFilter(ObjectId.Parse(comment.ParentCommentId));
            }

            var newComment = new Comment(comment.Content, UserSession.UserId, DateTime.Now);

            // TODO: make it work for nested comment!!
            await GetCollection().UpdateOneAsync(x => x.Id == ObjectId.Parse(comment.ThreadId),
                Builders<Thread>.Update.AddToSet(x => x.Comments, newComment));
        }

        private void EnsureCommentDetails(NewCommentViewModel comment)
        {
            if (!new CommentValidator().IsValid(comment))
            {
                throw new InvalidModelDetailsException();
            }
        }

        public async Task<IEnumerable<CommentViewModel>> GetAll(string threadId)
        {
            var comments = (await Get(ObjectId.Parse(threadId))).Comments
                .OrderByDescending(x => x.Upvoters.Count() - x.Downvoters.Count());

            return comments.Select(x => new CommentViewModel(x, threadId, UserSession.UserId));
        }
    }
}