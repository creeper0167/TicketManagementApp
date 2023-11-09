using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories.Services
{
    public class TicketService : ITicketRepo
    {
        private TkContext _tkContext;
        public TicketService()
        {
            _tkContext = new TkContext();
        }
        public bool DeleteTicket(Ticket ticket)
        {
            try
            {
                _tkContext.Tickets.Remove(ticket);
                return true;
            }catch { return false; }
        }

        public void Dispose()
        {
            _tkContext.Dispose();
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _tkContext.Tickets;
        }

        public Ticket GetTicketById(int id)
        {
            return _tkContext.Tickets.Find(id);
        }

        public bool InsertTicket(Ticket ticket)
        {
            try
            {
                _tkContext.Tickets.Add(ticket);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateTicket(Ticket ticket)
        {
            try
            {
                _tkContext.Entry(ticket).State=EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}