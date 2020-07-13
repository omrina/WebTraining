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
        }

        protected void SetUserId()
        {
            if (Request != null)
            {
                Logic.UserId = new ObjectId(Request.Headers.Authorization.Scheme);
            }
        }
    }
}