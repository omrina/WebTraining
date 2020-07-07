
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

        public IHttpActionResult GetSubscribtions()
        {
            return Ok();
        }

    }
}