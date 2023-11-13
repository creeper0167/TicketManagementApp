using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface ITicketReplyRepo : IDisposable
    {
        IEnumerable<TicketReply> GetTicketReplies();
        IEnumerable<TicketReply> GetTicketRepliesByTicketId(int ticketId);
        bool InsertTicketReply(TicketReply ticketReply);
        bool UpdateTicketReply(TicketReply ticketReply);
        bool DeleteTicketReply(TicketReply ticketReply);
        TicketReply GetTicketReplyById(int id);
        void Save();
    }
}
