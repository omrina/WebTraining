
using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;

namespace Server.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : BaseController<UserLogic, User>
    {
        public UserController() : base(new UserLogic())
        {
        }

        [Route("subscribe/{subwebbitId}")]
        [HttpPost]
        public async Task<IHttpActionResult> Subscribe(string subwebbitId)
        {
            SetUserId();
            await Logic.Subscribe(subwebbitId);

            return Ok();
        }

        [Route("unsubscribe/{subwebbitId}")]
        [HttpPost]
        public async Task<IHttpActionResult> Unsubscribe(string subwebbitId)
        {
            SetUserId();
            await Logic.Unsubscribe(subwebbitId);

            return Ok();
        }

    }
}