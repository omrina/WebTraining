using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [RoutePrefix("api/votes")]
    public class RatingController : BaseController<RatingLogic, Subwebbit>
    {
        public RatingController() : base(new RatingLogic())
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Vote(VoteOnItemViewModel voteInfo)
        {
            await Logic.Vote(voteInfo);

            return Ok();
        }
    }
}