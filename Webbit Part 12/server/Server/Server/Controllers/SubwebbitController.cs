using System.Linq;
using System.Web.Http;
using MongoDB.Driver.Linq;
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

        // [Route("create")]
        // [HttpGet]
        // public IHttpActionResult Create(NewSubwebbitViewModel subwebbit)
        // {
        //     Logic.Create(subwebbit);
        //
        //     return Ok();
        // }

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