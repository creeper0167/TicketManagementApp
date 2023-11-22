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
        public int TicketId { get; set; }
        //[Display(Name ="پاسخ دعنده")]
        //public int AccountID { get; set; }

        [Required]
        public string Text { get; set; }

        
        public TicketReply()
        {
            
        }
        public virtual Ticket Ticket { get; set; }

    }
}