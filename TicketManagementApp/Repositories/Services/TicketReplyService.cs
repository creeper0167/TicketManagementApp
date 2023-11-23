using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TicketManagementApp.Context;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories.Services
{
    public class TicketReplyService : ITicketReplyRepo
    {
        private TkContext _tkContext;
        public TicketReplyService()
        {
            _tkContext = new TkContext();
        }
        public bool DeleteTicketReply(TicketReply ticketReply)
        {
            try
            {
                _tkContext.TicketReplys.Remove(ticketReply);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _tkContext.Dispose();

        }

        public IEnumerable<TicketReply> GetTicketReplies()
        {
            return _tkContext.TicketReplys;
        }

        public IEnumerable<TicketReply> GetTicketRepliesByTicketId(int ticketId)
        {
            return _tkContext.TicketReplys.Where(item => item.TicketID == ticketId);
        }

        public TicketReply GetTicketReplyById(int id)
        {
            return _tkContext.TicketReplys.Find(id);
        }

        public bool InsertTicketReply(TicketReply ticketReply)
        {
            try
            {
                _tkContext.TicketReplys.Add(ticketReply);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateTicketReply(TicketReply ticketReply)
        {
            try
            {
                _tkContext.Entry(ticketReply).State = EntityState.Modified;
                return true;
            }catch  (Exception ex)
            {
                return false;
            }
        }

    }
}