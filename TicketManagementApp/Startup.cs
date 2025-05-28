using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Hangfire;
[assembly: OwinStartup(typeof(TicketManagementApp.Startup))]
namespace TicketManagementApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("TkContext");

            app.UseHangfireServer();

            app.UseHangfireDashboard();

            app.MapSignalR();
        }
    }
}