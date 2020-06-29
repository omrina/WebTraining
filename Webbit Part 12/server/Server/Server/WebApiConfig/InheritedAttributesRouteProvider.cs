using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Server.WebApiConfig
{
    public class InheritedAttributesRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }
}