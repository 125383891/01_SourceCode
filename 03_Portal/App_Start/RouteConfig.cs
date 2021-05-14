
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LYY.EnterpriseWxMgt.WebHost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home_Error404",
                url: "Error/404",
                defaults: new { controller = "Error", action = "404", requestUrl = UrlParameter.Optional },
                namespaces: new[] { "LYY.EnterpriseWxMgt.WebHost.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LYY.EnterpriseWxMgt.WebHost.Controllers" }
            );

            // Add this code to handle non-existing urls
            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "Error", action = "404", requestUrl = UrlParameter.Optional },
                namespaces: new[] { "LYY.EnterpriseWxMgt.WebHost.Controllers" }
            );

           
        }
    }
}
