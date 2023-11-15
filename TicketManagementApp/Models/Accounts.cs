using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Accounts
    {
        [Key]
        public int AccountID { get; set; }
        [Display(Name ="منطقه گروه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int UserGroupID { get; set; }
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Username { get; set; }
        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        [Display(Name ="نام")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string FullName { get; set; }

        public Accounts()
        {
            
        }

        public virtual UserGroup UserGroup { get; set; }
        public virtual List<Ticket> Ticket { get; set; }
        public virtual Role Role { get; set; }
    }
}