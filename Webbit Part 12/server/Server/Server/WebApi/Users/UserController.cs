using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;
using Server.WebApi.Authentication.ViewModels;

namespace Server.WebApi.Users
{
    [RoutePrefix("api/users")]
    public class UserController : BaseController<UserLogic, User>
    {
        public UserController() : base(new UserLogic())
        {
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IHttpActionResult> Signup(UserAuthViewModel user)
        {
            await Logic.Signup(user);

            return Ok();
        }
    }
}