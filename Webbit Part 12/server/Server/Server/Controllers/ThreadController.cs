using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [RoutePrefix("api/threads")]
    public class ThreadController : BaseController<ThreadLogic, Subwebbit>
    {
        public ThreadController() : base(new ThreadLogic())
        {
        }
        [Route("{subwebbitId}/recent/{index}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetRecentThreads(string subwebbitId, int index)
        {
            return Ok(await Logic.GetRecentThreads(subwebbitId, index));
        }

        [Route("topRated/{index}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTopThreadsFromSubscribed(int index)
        {
            var userId = GetAuthorizationToken();

            return Ok(await Logic.GetTopThreadsFromSubscribed(userId, index));
        }

        [Route("{subwebbitId}/{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string subwebbitId, string threadId)
        {
            var thread = await Logic.Get(subwebbitId, threadId);
        
            return Ok(thread);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewThreadViewModel thread)
        {
            await Logic.Create(thread);

            return Ok();
        }

        // TODO: should move to here create, delete, etc...?
    }
}