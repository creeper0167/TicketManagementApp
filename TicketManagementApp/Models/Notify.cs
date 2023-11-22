using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Notify
    {
        [Key]
        public int NotifyId { get; set; }
        public string NotifyText { get; set; }
        public bool isRead { get; set; }

        public Notify()
        {
            
        }
    }
}