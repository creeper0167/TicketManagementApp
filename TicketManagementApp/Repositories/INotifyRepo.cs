using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface INotifyRepo
    {
        IEnumerable<Notify> GetAllNotifs();
        bool InsertNotif(Notify notify);
        bool UpdateNotif(Notify notify);
        bool DeleteNotif(Notify notify);
        Notify GetNotifById(int id);
        //UserGroup GetUserGroupByAccountId(int id);
        void Save();
    }
}
