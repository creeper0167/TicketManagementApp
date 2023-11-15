using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "کد نفش")]
        [Required(ErrorMessage ="لطفا {0} را تعیین کنید")]
        public int RoleId { get; set; }
        [Display(Name ="نام نقش")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }

        public Role()
        {
            
        }

        public virtual List<Accounts> Accounts { get; set; }

    }
}