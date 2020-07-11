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
        public async Task<IHttpActionResult> GetAllByName(string name)
        {
            return Ok(await Logic.GetAllByName(name));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var subwebbit = await Logic.Get(id, GetAuthorizationToken());

            return Ok(subwebbit);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(NewSubwebbitViewModel subwebbit)
        {
            var id = await Logic.Create(subwebbit);
        
            return Ok(id);
        }
    }
}