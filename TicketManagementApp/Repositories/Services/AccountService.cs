using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using  System.Data.Entity;
namespace TicketManagementApp.Repositories.Services
{
    public class AccountService : IAccountRepo
    {
        private TkContext _tkContext;
        private IUserGroupRepo _userRepo;
        public AccountService()
        {
            _tkContext = new TkContext();
            _userRepo = new UserGroupService();
        }
        public bool DeleteAccount(Accounts account)
        {
            try
            {
                _tkContext.Accounts.Remove(account);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Accounts GetAccountById(int id)
        {
            return _tkContext.Accounts.Find(id);
        }

        public IEnumerable<Accounts> GetAllAccounts()
        {
            return _tkContext.Accounts;
        }

        public UserGroup GetUserGroupByAccountId(int id)
        {
            return _tkContext.Accounts.Find(id).UserGroup;
        }

        public bool InsertAccount(Accounts account)
        {
            try
            {
                _tkContext.Accounts.Add(account);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateAccount(Accounts account)
        {
            try
            {
                _tkContext.Entry(account).State = EntityState.Modified;

                return true;
            }catch(Exception ex) { return false;}
        }
    }
}