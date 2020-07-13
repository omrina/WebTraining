using System.Threading.Tasks;
using System.Web.Http;
using Server.BL.Authentication.ViewModels;
using Server.Models;

namespace Server.BL.Authentication
{
    [RoutePrefix("auth")]
    public class AuthController : BaseController<AuthLogic, User>
    {
        public AuthController() : base(new AuthLogic())
        {
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginViewModel user)
        {
            var loggedUser = await Logic.Login(user);

            return Ok(loggedUser);
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IHttpActionResult> Signup(UserSignupViewModel user)
        {
            await Logic.Signup(user);

            return Ok();
        }
    }
}