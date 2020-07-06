using System;
using System.Threading.Tasks;
using System.Web.Http;
using Server.Logic;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [RoutePrefix("api/subwebbits")]
    public class SubwebbitController : BaseController<SubwebbitLogic, Subwebbit>
    {
        public SubwebbitController() : base(new SubwebbitLogic())
        {
        }

        [Route("search/{name}")]
        [HttpGet]
        public IHttpActionResult GetAllByName(string name)
        {
            return Ok(Logic.GetAllByNameMatch(name));
        }

        [Route("create")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(NewSubwebbitViewModel subwebbit)
        {
            var id = await Logic.Create(subwebbit);
        
            return Ok(id);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var subwebbit = await Logic.Get(id);

            return Ok(new SubwebbitViewModel(subwebbit));
        }

        [Route("{subwebbitId}/threads/{index}/{amount}")]
        [HttpGet]
        public async Task<IHttpActionResult> CreateThread(string subwebbitId, int index, int amount)
        {
            return Ok(await Logic.GetThreads(new FetchThreadsViewModel(subwebbitId, index, amount)));
        }

        [Route("createThread")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateThread(NewThreadViewModel thread)
        {
            await Logic.CreateThread(thread);

            return Ok();
        }
    }
}