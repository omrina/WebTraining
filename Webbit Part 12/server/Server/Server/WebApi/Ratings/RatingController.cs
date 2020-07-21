using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;
using Server.WebApi.Ratings.ViewModels;

namespace Server.WebApi.Ratings
{
    // TODO: move the 'vote' func to the other controllers!
    [RoutePrefix("api/ratings")]
    public class RatingController : BaseController<RatingLogic, Thread>
    {
        public RatingController() : base(new RatingLogic())
        {
        }

        // [Route("")]
        // [HttpPost]
        // public async Task<IHttpActionResult> Vote(UserVoteViewModel userVoteInfo)
        // {
        //     await Logic.Vote(userVoteInfo);
        //
        //     return Ok();
        // }
    }
}