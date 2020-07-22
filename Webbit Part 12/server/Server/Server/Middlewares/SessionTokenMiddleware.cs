using System.CodeDom;
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

            await SetSessionToken(context);
        }

        private bool ShouldIncludeToken(IOwinRequest request)
        {
            const string signupActionName = "/signup";
            const string loginActionName = "/login";
            var requestUrl = request.Uri.AbsoluteUri;

            return !requestUrl.EndsWith(signupActionName) &&
                   !requestUrl.EndsWith(loginActionName);
        }

        private async Task SetSessionToken(IOwinContext context)
        {
            const string tokenHeader = "Authorization";
            var token = ObjectId.Empty;

            if (ObjectId.TryParse(context.Request.Headers.Get(tokenHeader), out token))
            {
                await new UserSession().SetToken(token);
                await Next.Invoke(context);

                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}