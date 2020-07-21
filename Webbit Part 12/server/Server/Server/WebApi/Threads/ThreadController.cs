using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Threads.ViewModels;

namespace Server.WebApi.Threads
{
    [RoutePrefix("api/threads")]
    public class ThreadController : BaseController<ThreadLogic, Thread>
    {
        public ThreadController() : base(new ThreadLogic())
        {
        }

        [Route("{subwebbitId}/recent/{index}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetRecentThreads(string subwebbitId, int index)
        {
            return Ok(await Logic.GetRecentThreads(ObjectId.Parse(subwebbitId), index));
        }

        [Route("topRated/{index}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTopThreadsFromSubscribed(int index)
        {
            return Ok(await Logic.GetTopThreadsFromSubscribed(index));
        }

        [Route("{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string threadId)
        {
            var thread = await Logic.GetById(ObjectId.Parse(threadId));

            return Ok(thread);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NewThreadViewModel thread)
        {
            await Logic.Create(thread);

            return Ok();
        }

        [Route("{threadId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string threadId)
        {
            await Logic.Delete(ObjectId.Parse(threadId));

            return Ok();
        }
    }
}