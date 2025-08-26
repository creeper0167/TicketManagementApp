using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagementApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        // <-- Session handling -->
        //protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        //{
        //    var context = HttpContext.Current;

        //    if (context.Session != null && context.Session["AccountID"] == null)
        //    {
        //        string[] allowedPaths = new[] { "/login/login", "/login/logout" }; // URLs that don't require session
        //        var currentPath = context.Request.Path.ToLower();

        //        if (!allowedPaths.Any(p => currentPath.StartsWith(p)) &&
        //            !currentPath.Contains("content") && !currentPath.Contains("scripts"))
        //        {
        //            context.Response.Redirect("~/Login/Login");
        //        }
        //    }
        //}

        //---
        protected void Application_Error()
        {
            var error = Server.GetLastError();
            if((error as HttpException)?.GetHttpCode() == 404)
            {
                Server.ClearError();
                Response.StatusCode = 404;
            }
            if((error as HttpException)?.GetHttpCode() == 500)
            {
                Server.ClearError();
                Response.StatusCode= 500;
            }
        }
    }
}
