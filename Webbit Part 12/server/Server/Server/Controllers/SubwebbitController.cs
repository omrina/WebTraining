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

        // [Route("")]
        // [HttpGet]
        // public IHttpActionResult GetAll()
        // {
        //     return Ok(Logic.GetAll());
        // }

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

        // [Route("{id}")]
        // [HttpGet]
        // public IHttpActionResult Get(string id)
        // {
        //     Logic.GetAll().Where();
        //
        //     return Ok();
        // }

    }
}