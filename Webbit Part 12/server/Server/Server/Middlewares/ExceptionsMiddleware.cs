using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Server.Exceptions;

namespace Server.Middlewares
{
    public class ExceptionsMiddleware : OwinMiddleware
    {
        private const HttpStatusCode UnknownExceptionHttpCode = HttpStatusCode.InternalServerError;
        private IDictionary<Type, HttpStatusCode> ExceptionsToHttpCodes { get; }

        public ExceptionsMiddleware(OwinMiddleware next) : base(next)
        {
            ExceptionsToHttpCodes = new Dictionary<Type, HttpStatusCode>
            {
                {typeof(UnauthenticatedRequestException), HttpStatusCode.BadRequest},
                {typeof(InvalidModelDetailsException), HttpStatusCode.BadRequest},
                {typeof(LoginFailedException), HttpStatusCode.Unauthorized},
                {typeof(UserNotOwnerException), HttpStatusCode.Unauthorized},
                {typeof(UsernameAlreadyTakenException), HttpStatusCode.Conflict},
                {typeof(SubwebbitNameAlreadyTakenException), HttpStatusCode.Conflict},
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
                // LOG HERE
                var exceptionType = exception.GetType();
                var httpStatusCode = ExceptionsToHttpCodes.ContainsKey(exceptionType)
                                            ? ExceptionsToHttpCodes[exceptionType]
                                            : UnknownExceptionHttpCode;

                context.Response.StatusCode = (int) httpStatusCode;
            }
        }
    }
}