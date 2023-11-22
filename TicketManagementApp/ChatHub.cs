using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories;
using TicketManagementApp.Repositories.Services;

namespace TicketManagementApp
{
    public class ChatHub : Hub
    {
        private TkContext tkContext = new TkContext();
        private INotifyRepo notifyService = new NotifyService();
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string message)
        {
            Notify notify = new Notify();
            notify.isRead = false;
            notify.NotifyText = message;
            notifyService.InsertNotif(notify);

            Clients.All.updatePage();
            //tkContext.notifies.
            Clients.All.addNewMessageToPage(message);

        }
    }
}