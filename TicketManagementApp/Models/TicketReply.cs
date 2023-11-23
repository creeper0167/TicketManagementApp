using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class TicketReply
    {
        [Key]
        public int ReplyId { get; set; }

        [Display(Name ="شماره تیکت")]
        public int TicketID { get; set; }

        [Display(Name = "پاسخ دعنده")]
        public int? AccountID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [Display(Name ="تاریخ پاسخ")]
        public DateTime TicketDate { get; set; }

        public TicketReply()
        {
            
        }
        public virtual Ticket Ticket { get; set; }
        //public virtual Accounts Accounts { get; set; }
    }
}