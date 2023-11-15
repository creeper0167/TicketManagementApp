using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using System.Data.Entity;

namespace TicketManagementApp.Repositories.Services
{
    public class RoleService : IRoleRepo
    {
        private TkContext _tkContext;
        public RoleService()
        {
            _tkContext = new TkContext();
        }
        public bool DeleteRole(Role role)
        {
            try
            {
                _tkContext.Roles.Remove(role);
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

        public IEnumerable<Role> GetAllRoles()
        {
            return _tkContext.Roles;
        }

        public Role GetRoleById(int id)
        {
            return _tkContext.Roles.Find(id);
        }

        public bool InsertRole(Role role)
        {
            try
            {
                _tkContext.Roles.Add(role);
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                _tkContext.Entry(role).State = EntityState.Modified;
                return true;
            }catch(Exception ex){ return false; }
        }
    }
}