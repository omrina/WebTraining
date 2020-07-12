using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
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
            await Logic.Vote(userVoteInfo, new ObjectId(GetUserIdFromRequest()));

            return Ok();
        }
    }
}