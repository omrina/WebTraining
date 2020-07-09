using System.Web.Http;
using Server.Logic;
using Server.Models;

namespace Server.Controllers
{
    [RoutePrefix("api/threads")]
    public class ThreadController : BaseController<ThreadLogic, Thread>
    {
        public ThreadController() : base(new ThreadLogic())
        {
        }

        // TODO: should move to here create, delete, etc...?
        // [Route("{id}")]
        // [HttpGet]
        // public async Task<IHttpActionResult> Get(string id)
        // {
        //     var thread = await Logic.g(id, GetAuthorizationToken());
        //
        //     return Ok(thread);
        // }
    }
}