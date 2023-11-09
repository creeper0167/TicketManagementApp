using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories.Services
{
    public class TicketGroupService : ITicketGroupRepo
    {
        private TkContext _tkContext;
        public TicketGroupService()
        {
            _tkContext = new TkContext();
        }

        public bool DeleteTicketGroup(TicketGroup ticketGroup)
        {
            try
            {
                _tkContext.TicketGroups.Remove(ticketGroup);
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public void Dispose()
        {
            _tkContext.Dispose();
        }

        public IEnumerable<TicketGroup> GetAllTicketGroups()
        {
            return _tkContext.TicketGroups;
        }

        public TicketGroup GetTicketGroupById(int id)
        {
            return _tkContext.TicketGroups.Find(id);
        }

        public bool InsertTicketGroup(TicketGroup ticketGroup)
        {
            try {
                _tkContext.TicketGroups.Add(ticketGroup);
                return true; 
            }catch(Exception ex) { return false; }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateTicketGroup(TicketGroup ticketGroup)
        {
            try
            {
                _tkContext.Entry(ticketGroup).State=EntityState.Modified;
                return true;
            }catch(Exception ex) { return false;}
        }
    }
}