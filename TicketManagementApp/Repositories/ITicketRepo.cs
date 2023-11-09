using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface ITicketRepo : IDisposable
    {
        IEnumerable<Ticket> GetAllTickets();
        bool InsertTicket(Ticket ticket);
        bool UpdateTicket(Ticket ticket);
        bool DeleteTicket(Ticket ticket);
        Ticket GetTicketById(int id);
        void Save();
    }
}
