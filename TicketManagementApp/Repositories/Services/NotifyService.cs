using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using System.Data.Entity;

namespace TicketManagementApp.Repositories.Services
{
    public class NotifyService : INotifyRepo
    {
        private TkContext tkContext;
        public NotifyService()
        {
            tkContext = new TkContext();
        }
        public bool DeleteNotif(Notify notify)
        {
            try
            {
                tkContext.notifies.Remove(notify);
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Notify> GetAllNotifs()
        {
            return tkContext.notifies;
        }

        public Notify GetNotifById(int id)
        {
            return tkContext.notifies.Find(id);
        }

        public bool InsertNotif(Notify notify)
        {
            try {
                tkContext.notifies.Add(notify);
                return true;
            }catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
                return false; 
            }
        }

        public void Save()
        {
            tkContext.SaveChanges();
        }

        public bool UpdateNotif(Notify notify)
        {
            try
            {
                tkContext.Entry(notify).State = EntityState.Modified;
                return true;
            }catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
                return false; }
        }
    }
}