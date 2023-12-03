using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagementApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
           // routes.MapRoute(
           //     name: "Error",
           //     url: "{controller}/{action}/{id}",
           //     defaults: new { controller = "Erro", action = "Index", id = UrlParameter.Optional }
           //);
        }
    }
}
