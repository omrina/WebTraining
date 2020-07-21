using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using Server.Models;

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
        public async Task<IHttpActionResult> Search(string name)
        {
            return Ok(await Logic.Search(name));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var subwebbit = await Logic.GetById(ObjectId.Parse(id));

            return Ok(subwebbit);
        }

        [Route("{name}")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(string name)
        {
            var id = await Logic.Create(name);
        
            return Ok(id);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            await Logic.Delete(ObjectId.Parse(id));

            return Ok();
        }

        [Route("{id}/subscribe")]
        [HttpPost]
        public async Task<IHttpActionResult> Subscribe(string id)
        {
            await Logic.Subscribe(ObjectId.Parse(id));

            return Ok();
        }

        [Route("{id}/unsubscribe")]
        [HttpPost]
        public async Task<IHttpActionResult> Unsubscribe(string id)
        {
            await Logic.Unsubscribe(ObjectId.Parse(id));

            return Ok();
        }
    }
}