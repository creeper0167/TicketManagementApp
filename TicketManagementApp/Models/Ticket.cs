using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        [Display(Name ="عنوان گروه")]
        public int TicketGroupID { get; set; }
        [Display(Name ="موضوع تیکت")]
        public string TicketSubject { get; set; }
        public string TicketDescription { get; set; }

        public Ticket()
        {
            
        }
        public virtual TicketGroup TicketGroup { get; set; }
    }
}