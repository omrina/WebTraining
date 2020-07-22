﻿using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Comments.ViewModels;

namespace Server.WebApi.Comments
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseController<CommentLogic<NewCommentViewModel>, Thread>
    {
        public CommentController() : base(new CommentLogic<NewCommentViewModel>())
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewCommentViewModel comment)
        {
            await Logic.Post(comment);

            return Ok();
        }

        [Route("{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll(string threadId)
        {
            return Ok(await Logic.GetAll(ObjectId.Parse(threadId)));
        }

        [Route("vote")]
        [HttpPost]
        public async Task<IHttpActionResult> Vote(CommentVoteViewModel commentVote)
        {
            return Ok(await Logic.Vote(commentVote));
        }
    }
}