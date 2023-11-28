using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Display(Name ="نام دپارتمان")]
        public string DepartmentTitle { get; set; }
        public Department()
        {
            
        }
        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<Accounts> Accounts { get; set; }
    }
}