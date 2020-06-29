using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.Owin;
using Server.Exceptions;

namespace Server.Middlewares
{
    public class ExceptionsMiddleware : OwinMiddleware
    {
        private IDictionary<Type, HttpStatusCode> ExceptionsToCodes { get; }

        public ExceptionsMiddleware(OwinMiddleware next) : base(next)
        {
            ExceptionsToCodes = new Dictionary<Type, HttpStatusCode>
            {
                {typeof(InvalidSignupDetailsException), HttpStatusCode.BadRequest},
                {typeof(LoginFailedException), HttpStatusCode.Unauthorized},
                {typeof(UsernameAlreadyTakenException), HttpStatusCode.Conflict},
                {typeof(Exception), HttpStatusCode.InternalServerError},
            };
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception exception)
            {
                // TODO: logger!?
                context.Response.StatusCode = (int) ExceptionsToCodes[exception.GetType()];
            }
        }
    }
}