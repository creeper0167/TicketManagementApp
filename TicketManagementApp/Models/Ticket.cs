﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        [Display(Name ="واحد")]
        public int? UserGroupID { get; set; }
        [Display(Name ="نوع تیکت")]
        public int TicketGroupID { get; set; }
        [Display(Name = "کاربر")]
        public int AccountID { get; set; }
        [Display(Name ="موضوع تیکت")]
        public string TicketSubject { get; set; }
        [Display(Name ="شرح تیکت")]
        [DataType(DataType.MultilineText)]
        public string TicketDescription { get; set; }
        [Display(Name ="ضمیمه")]
        [DataType(DataType.Upload)]
        public string TicketAttachment {  get; set; }
        
        [Display(Name ="وضعیت تیکت")]
        public string TicketStatus { get; set; }
        [Display(Name ="تاریخ ایجاد تیکت")]
        public DateTime TicketDate { get; set; }
        [Display(Name ="کد رهگیری")]
        public string TrackCode { get; set; }
        [Display(Name ="واحد گیرنده")]
        public int? DepartmentId { get; set; }
        public Ticket()
        {
            
        }
        public virtual TicketGroup TicketGroup { get; set; }
        public virtual Accounts Account { get; set; }
        public virtual List<TicketReply> TicketReply { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual Department Department { get; set; }
    }
}