using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Server.Logic;
using Server.ViewModels;

namespace Server.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private UserLogic Logic { get; }

        public AuthController()
        {
            Logic = new UserLogic();
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginViewModel user)
        {
            return Ok(Logic.Login(user));
        }
    }
}
