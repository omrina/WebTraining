using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using MongoDB.Bson;
using Server.WebApi.Authentication;

namespace Server.Middlewares
{
    public class SessionTokenMiddleware : OwinMiddleware

    {
        public SessionTokenMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (!ShouldIncludeToken(context.Request))
            {
                await Next.Invoke(context);

                return;
            }

            var token = ObjectId.Empty;

            if (ObjectId.TryParse(context.Request.Headers.Get("Authorization"), out token))
            {
                await new UserSession().SetToken(token);
                await Next.Invoke(context);

                return;
            }

            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }

        private bool ShouldIncludeToken(IOwinRequest request)
        {
            var requestUrl = request.Uri.AbsoluteUri;

            return !requestUrl.EndsWith("/signup") && !requestUrl.EndsWith("/login");
        }
    }
}