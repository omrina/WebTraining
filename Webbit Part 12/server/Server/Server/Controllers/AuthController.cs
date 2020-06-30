using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : BaseController<AuthLogic, User>
    {
        public AuthController() : base(new AuthLogic())
        {
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginViewModel user)
        {
            return Ok(Logic.Login(user));
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