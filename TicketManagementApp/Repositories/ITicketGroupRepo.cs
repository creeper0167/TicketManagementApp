using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface ITicketGroupRepo : IDisposable
    {
        IEnumerable<TicketGroup> GetAllTicketGroups();
        bool InsertTicketGroup(TicketGroup ticketGroup);
        bool DeleteTicketGroup(TicketGroup ticketGroup);
        bool UpdateTicketGroup(TicketGroup ticketGroup);
        TicketGroup GetTicketGroupById(int id);
        void Save();
    }
}
