using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    public interface IAccountRepo : IDisposable
    {
        IEnumerable<Accounts> GetAllAccounts();
        bool InsertAccount(Accounts account);
        bool UpdateAccount(Accounts account);
        bool DeleteAccount(Accounts account);
        Accounts GetAccountById(int id);
        UserGroup GetUserGroupByAccountId(int id);
        void Save();
    }
}

