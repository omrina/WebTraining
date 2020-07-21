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
using Server.WebApi.Ratings.ViewModels;
using Server.WebApi.Users;

namespace Server.WebApi.Comments
{
    // TODO: inherit from thread logic???
    public class CommentLogic : BaseLogic<Thread>
    {
        public async Task Post(NewCommentViewModel comment)
        {
            EnsureCommentDetails(comment);
            var newComment = new Comment(comment.Content, UserSession.UserId, DateTime.Now);

            await GetCollection().UpdateOneAsync(x => x.Id == ObjectId.Parse(comment.ThreadId),
                Builders<Thread>.Update.AddToSet(x => x.Comments, newComment));
        }

        protected void EnsureCommentDetails(NewCommentViewModel comment)
        {
            if (!new CommentValidator().IsValid(comment))
            {
                throw new InvalidModelDetailsException();
            }
        }

        public async Task<IEnumerable<CommentInfoViewModel>> GetAll(ObjectId threadId)
        {
            var comments = (await Get(threadId)).Comments
                .OrderByDescending(x => x.Upvoters.Count - x.Downvoters.Count);

            return await Task.WhenAll(comments.Select(async x => await ConvertToViewModel(x, threadId)));
        }

        private async Task<CommentInfoViewModel> ConvertToViewModel(Comment comment, ObjectId threadId)
        {
            var subComments = await Task.WhenAll(
                comment.Comments.Select(async x => await ConvertToViewModel(x, threadId)));
            var author = await new UserLogic().GetName(comment.AuthorId);

            return new CommentInfoViewModel(comment, threadId, UserSession.UserId, author, subComments);
        }

        public async Task<ItemVoteInfoViewModel> Vote(CommentVoteViewModel commentVote)
        {
            var thread = await Get(ObjectId.Parse(commentVote.Id));
            var comment = thread.Comments.First(x => x.Id == ObjectId.Parse(commentVote.CommentId));
            new ItemVoter(comment, UserSession.UserId).Vote(commentVote.Vote);

            await GetCollection().ReplaceOneAsync(x => x.Id == thread.Id, thread);

            return new ItemVoteInfoViewModel(comment, UserSession.UserId);
        }
    }
}