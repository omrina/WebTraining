using System.Web.Http;
using Server.Logic;
using Server.Models;

namespace Server.Controllers
{
    [RoutePrefix("api")]
    public abstract class BaseController<TLogic, TModel> : ApiController 
        where TLogic : BaseLogic<TModel> 
        where TModel : BaseModel
    {
        protected TLogic Logic { get; }

        protected BaseController(TLogic logic)
        {
            Logic = logic;
        }
    }
}