using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Airlines
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{page}/{typeId}",
                defaults: new { controller = "Companies", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional, typeId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Aircraft",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Companies", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional }
            );
        }
    }
}
