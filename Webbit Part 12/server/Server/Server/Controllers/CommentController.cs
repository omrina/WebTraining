using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
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
            return Ok(await Logic.GetAll(subwebbitId, threadId, GetUserIdFromRequest()));
        }
    }
}