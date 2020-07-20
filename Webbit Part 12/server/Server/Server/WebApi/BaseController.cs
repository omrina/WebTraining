using System.Net.Http;
using System.Web;
using System.Web.Http;
using MongoDB.Bson;
using Server.Models;

namespace Server.WebApi
{
    public abstract class BaseController<TLogic, TModel> : ApiController
        where TLogic : BaseLogic<TModel>
        where TModel : BaseModel
    {
        protected TLogic Logic { get; }
        // protected ObjectId UserToken => 
            // Request.GetOwinContext().Get<ObjectId>("Token");

        protected BaseController(TLogic logic)
        {
            Logic = logic;
            // TODO: check if got the token!
            // Logic.Token = HttpContext.Current.GetOwinContext().Get<ObjectId>("Token");
        }

        protected void SetUserId()
        {
            if (Request != null)
            {
                // Logic.Token = ObjectId.Parse(Request.Headers.Authorization.Scheme);
            }
        }
    }
}