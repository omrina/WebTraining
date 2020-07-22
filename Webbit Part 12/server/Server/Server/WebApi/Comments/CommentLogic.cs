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
using Server.WebApi.RatingSystem;
using Server.WebApi.RatingSystem.ViewModels;
using Server.WebApi.Users;

namespace Server.WebApi.Comments
{
    public class CommentLogic<TNewCommentViewModel> : BaseLogic<Thread>
        where TNewCommentViewModel : NewCommentViewModel
    {
        public async Task Post(TNewCommentViewModel commentInfo)
        {
            EnsureCommentDetails(commentInfo);
            var comment = new Comment(commentInfo.Content, UserSession.UserId, DateTime.Now);

            await Add(comment, commentInfo);
        }

        protected void EnsureCommentDetails(TNewCommentViewModel comment)
        {
            if (!new CommentValidator().IsValid(comment))
            {
                throw new InvalidModelDetailsException();
            }
        }

        protected virtual async Task Add(Comment comment, TNewCommentViewModel commentInfo)
        {
            await GetCollection().UpdateOneAsync(x => x.Id == ObjectId.Parse(commentInfo.ThreadId),
                Builders<Thread>.Update.AddToSet(x => x.Comments, comment));
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

        public async Task<VoteInfoViewModel> Vote(CommentVoteViewModel commentVote)
        {
            var thread = await Get(ObjectId.Parse(commentVote.Id));
            var comment = thread.Comments.First(x => x.Id == ObjectId.Parse(commentVote.CommentId));
            new ItemVoter(comment, UserSession.UserId).Vote(commentVote.Vote);

            await GetCollection().ReplaceOneAsync(x => x.Id == thread.Id, thread);

            return new VoteInfoViewModel(comment, UserSession.UserId);
        }
    }
}