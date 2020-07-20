using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;
using Server.WebApi.Comments.ViewModels;

namespace Server.WebApi.Comments
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseController<CommentLogic, Thread>
    {
        public CommentController() : base(new CommentLogic())
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewCommentViewModel comment)
        {
            await Logic.Post(comment);

            return Ok();
        }

        [Route("{subwebbitId}/{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll(string subwebbitId, string threadId)
        {
            SetUserId();

            return Ok(await Logic.GetAll(threadId));
        }
    }
}