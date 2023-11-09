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
        [Display(Name ="شرح تیکت")]
        public string TicketDescription { get; set; }
        [Display(Name ="ضمیمه")]
        public string TicketAttachment {  get; set; }

        public Ticket()
        {
            
        }
        public virtual TicketGroup TicketGroup { get; set; }
    }
}