using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using Server.Middlewares;
using Server.WebApiConfig;

namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<ExceptionsMiddleware>();

            var config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.MapHttpAttributeRoutes(new InheritedAttributesRouteProvider());
            config.Services.Replace(typeof(IExceptionHandler), new PassThroughExceptionsHandler());

            appBuilder.UseWebApi(config);

            var physicalFileSystem = new PhysicalFileSystem(ConfigurationManager.AppSettings["ClientPath"]);

            var options = new FileServerOptions
            {
                StaticFileOptions =
                {
                    FileSystem = physicalFileSystem,
                    ServeUnknownFileTypes = true
                }
            };

            appBuilder.UseFileServer(options);
        }
    }
}
