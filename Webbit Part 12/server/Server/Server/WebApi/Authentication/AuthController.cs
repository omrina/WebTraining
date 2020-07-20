using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;
using Server.WebApi.Authentication.ViewModels;

namespace Server.WebApi.Authentication
{
    [RoutePrefix("auth")]
    public class AuthController : BaseController<AuthLogic, OnlineUser>
    {
        public AuthController() : base(new AuthLogic())
        {
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(UserAuthViewModel user)
        {
            var loggedUser = await Logic.Login(user);

            return Ok(loggedUser);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IHttpActionResult> Logout()
        {
            await Logic.Logout();

            return Ok();
        }
    }
}