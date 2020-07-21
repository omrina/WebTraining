using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;
using Server.WebApi.Comments.ViewModels;
using Server.WebApi.SubComments.ViewModels;

namespace Server.WebApi.SubComments
{
    // TODO: inherit from commentController?
    [RoutePrefix("api/subComments")]
    public class SubCommentController : BaseController<SubCommentLogic, Thread>
    {
        public SubCommentController() : base(new SubCommentLogic())
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewSubCommentViewModel comment)
        {
            await Logic.Post(comment);

            return Ok();
        }

        // TODO: add vote method
    }
}