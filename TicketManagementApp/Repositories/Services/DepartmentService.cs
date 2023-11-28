using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketManagementApp.Context;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories.Services
{
    public class DepartmentService : IDepartmentRepo
    {
        private TkContext _tkContext;
        public DepartmentService()
        {
            _tkContext = new TkContext();
        }
        public bool DeleteDepartment(Department department)
        {
            try
            {
                _tkContext.Departments.Remove(department);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _tkContext.Departments;
        }

        public Department GetDepartmentById(int id)
        {
            return _tkContext.Departments.Find(id);
        }

        public bool InsertDepartment(Department department)
        {
            try
            {
                _tkContext.Departments.Add(department);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            _tkContext.SaveChanges();
        }

        public bool UpdateDepartment(Department department)
        {
            try
            {
                _tkContext.Entry(department).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}