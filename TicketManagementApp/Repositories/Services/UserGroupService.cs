using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using System.Data.Entity;
namespace TicketManagementApp.Repositories.Services
{
    public class UserGroupService : IUserGroupRepo
    {
        private TkContext _tkContext;
        public UserGroupService()
        {
            _tkContext = new TkContext();
        }
        public bool DeleteUserGroup(UserGroup userGroup)
        {
            try
            {
                _tkContext.UserGroups.Remove(userGroup);
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

        public IEnumerable<UserGroup> GetAllUserGroups()
        {
            return _tkContext.UserGroups;
        }

        public UserGroup GetUserGroupById(int id)
        {
            return _tkContext.UserGroups.Find(id);
        }

        public bool InsertUserGroup(UserGroup userGroup)
        {
            try
            {
                _tkContext.UserGroups.Add(userGroup); 
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

        public bool UpdateUserGroup(UserGroup userGroup)
        {
            try
            {
                _tkContext.Entry(userGroup).State = EntityState.Modified;
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}