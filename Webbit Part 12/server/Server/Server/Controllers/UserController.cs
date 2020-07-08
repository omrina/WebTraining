﻿
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

        // public IHttpActionResult GetSubscribtions()
        // {
        //     return Ok();
        // }

        [Route("subscribe/{subwebbitId}")]
        [HttpPost]
        public async Task<IHttpActionResult> Subscribe(string subwebbitId)
        {
            await Logic.Subscribe(GetAuthorizationToken(), subwebbitId);

            return Ok();
        }

        [Route("unsubscribe/{subwebbitId}")]
        [HttpPost]
        public async Task<IHttpActionResult> Unsubscribe(string subwebbitId)
        {
            await Logic.Unsubscribe(GetAuthorizationToken(), subwebbitId);

            return Ok();
        }

    }
}