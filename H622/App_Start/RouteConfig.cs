using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace H622
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "myPost",
                url: "Blog/{year}/{month}/{title}",
                defaults: new { controller = "Home", action = "Post" }
                
                    
            );
            routes.MapRoute(
                name: "page",
                url: "Page/{page}/",
                defaults: new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                name: "tags",
                url: "Tags/{tagname}/",
                defaults: new { controller = "Home", action = "Tag" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
