using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface IRoleRepo : IDisposable
    {
        IEnumerable<Role> GetAllRoles();
        bool InsertRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Role role);
        Role GetRoleById(int id);
        //UserGroup GetRoleById(int id);
        void Save();
    }
}
