using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Subwebbits.ViewModels;

namespace Server.WebApi.Subwebbits
{
    [RoutePrefix("api/subwebbits")]
    public class SubwebbitController : BaseController<SubwebbitLogic, Subwebbit>
    {
        public SubwebbitController() : base(new SubwebbitLogic())
        {
        }

        [Route("search/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllByName(string name)
        {
            return Ok(await Logic.GetAllByName(name));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            SetUserId();
            var subwebbit = await Logic.GetViewModel(id);

            return Ok(subwebbit);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(NewSubwebbitViewModel subwebbit)
        {
            var id = await Logic.Create(subwebbit);
        
            return Ok(id);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            SetUserId();
            await Logic.Delete(ObjectId.Parse(id));

            return Ok();
        }

        [Route("{subwebbitId}/subscribe")]
        [HttpPost]
        public async Task<IHttpActionResult> Subscribe(string id)
        {
            SetUserId();
            await Logic.Subscribe(ObjectId.Parse(id));

            return Ok();
        }

        [Route("{subwebbitId}/unsubscribe")]
        [HttpPost]
        public async Task<IHttpActionResult> Unsubscribe(string id)
        {
            SetUserId();
            await Logic.Unsubscribe(ObjectId.Parse(id));

            return Ok();
        }
    }
}