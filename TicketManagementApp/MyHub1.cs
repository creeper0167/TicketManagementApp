using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;

namespace TicketManagementApp
{
    public class MyHub1 : Hub
    {
        private TkContext _tkContext = new TkContext();
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send()
        {
            Clients.All.updateNotificationBar();
        }

        public void Notify()
        {
            Clients.All.update();
        }
    }
}