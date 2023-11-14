using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    interface IUserGroupRepo : IDisposable
    {
        IEnumerable<UserGroup> GetAllUserGroups();
        bool InsertUserGroup(UserGroup userGroup);
        bool UpdateUserGroup(UserGroup userGroup);
        bool DeleteUserGroup(UserGroup userGroup);
        UserGroup GetUserGroupById(int id);
        void Save();
    }
}
