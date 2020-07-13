using System.Threading.Tasks;
using System.Web.Http;
using Server.BL.Threads.ViewModels;
using Server.Models;

namespace Server.BL.Threads
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
            SetUserId();

            return Ok(await Logic.GetRecentThreads(subwebbitId, index));
        }

        [Route("topRated/{index}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTopThreadsFromSubscribed(int index)
        {
            SetUserId();

            return Ok(await Logic.GetTopThreadsFromSubscribed(index));
        }

        [Route("{subwebbitId}/{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string subwebbitId, string threadId)
        {
            SetUserId();
            var thread = await Logic.GetViewModel(subwebbitId, threadId);

            return Ok(thread);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewThreadViewModel thread)
        {
            await Logic.Create(thread);

            return Ok();
        }

        [Route("{subwebbitId}/{threadId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string subwebbitId, string threadId)
        {
            SetUserId();
            await Logic.Delete(subwebbitId, threadId);

            return Ok();
        }
    }
}