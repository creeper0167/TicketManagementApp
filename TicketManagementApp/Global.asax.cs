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
