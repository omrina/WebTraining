using System.Web.Http;
using Server.Logic;
using Server.Models;

namespace Server.Controllers
{
    public abstract class BaseController<TLogic, TModel> : ApiController 
        where TLogic : BaseLogic<TModel> 
        where TModel : BaseModel
    {
        protected TLogic Logic { get; }

        protected BaseController(TLogic logic)
        {
            Logic = logic;
        }

        // TODO: rename?
        protected string GetAuthorizationToken()
        {
            return Request.Headers.Authorization.Scheme;
        }
    }
}