using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Repositories
{
    interface IDepartmentRepo
    {
        IEnumerable<Department> GetAllDepartments();
        bool InsertDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        Department GetDepartmentById(int id);
        void Save();
    }
}
