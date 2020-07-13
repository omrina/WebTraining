using System.Threading.Tasks;
using System.Web.Http;
using Server.BL.Comments.ViewModels;
using Server.Models;

namespace Server.BL.Comments
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseController<CommentLogic, Subwebbit>
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

            return Ok(await Logic.GetAll(subwebbitId, threadId));
        }
    }
}