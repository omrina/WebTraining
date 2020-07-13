using System.Threading.Tasks;
using System.Web.Http;
using Server.BL.Ratings.ViewModels;
using Server.Models;

namespace Server.BL.Ratings
{
    [RoutePrefix("api/ratings")]
    public class RatingController : BaseController<RatingLogic, Subwebbit>
    {
        public RatingController() : base(new RatingLogic())
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Vote(UserVoteViewModel userVoteInfo)
        {
            SetUserId();
            await Logic.Vote(userVoteInfo);

            return Ok();
        }
    }
}