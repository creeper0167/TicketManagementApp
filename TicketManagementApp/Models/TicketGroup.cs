using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class TicketGroup
    {
        [Key]
        public int TicketGroupID { get; set; }
        [Display(Name = "نوع تیکت")]
        public string TicketGroupTitle { get; set; }

        public TicketGroup()
        {
            
        }

        public virtual List<Ticket> Tickets { get; set; }
    }
}