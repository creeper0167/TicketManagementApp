using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupID { get; set; }
        [Display(Name ="نام منطقه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string UserGroupTitle { get; set; }
        public UserGroup()
        {
            
        }
        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<Accounts> Accounts { get; set; }
    }
}