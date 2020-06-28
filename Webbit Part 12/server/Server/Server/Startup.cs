using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Server.Startup))]

namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // TODO: make sure the client recognizes the server
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);

            var physicalFileSystem = new PhysicalFileSystem(ConfigurationManager.AppSettings["ClientPath"]);

            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem,
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
