using System.Web.Http;
using MongoDB.Bson;
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
            // SetUserId();
            // Logic.UserId = new ObjectId(GetUserIdFromRequest());
        }

        // TODO: rename?
        //
        // protected void SetUserId()
        // {
        //     if (Request != null)
        //     {
        //         Logic.UserId = new ObjectId(GetUserIdFromRequest());
        //     }
        // }

        protected string GetUserIdFromRequest()
        {
            return Request.Headers.Authorization.Scheme;
        }
    }
}