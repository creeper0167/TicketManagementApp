using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories;
using TicketManagementApp.Repositories.Services;

namespace TicketManagementApp.Controllers
{
    public class TicketController : Controller
    {
        private TkContext _tkContext;
        private ITicketReplyRepo _ticketReplyRepo;
        private ITicketRepo _ticketRepo;

        public TicketController()
        {
            _tkContext = new TkContext();
            _ticketRepo = new TicketService();
            _ticketReplyRepo = new TicketReplyService();
        }
        // GET: Ticket
        public ActionResult Index()
        {
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            ViewBag.UserGroupID = new SelectList(new UserGroupService().GetAllUserGroups(), "UserGroupID", "UserGroupTitle");

            return View();
        }
        public ActionResult TicketView(int? page)
        {
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            int departmentId = Int32.Parse(Session["DepartmentId"].ToString());
            ViewBag.UnreadCounterValue = _ticketRepo.GetAllTickets().Where(i => i.TicketStatus == "در انتظار بررسی" && i.DepartmentId == departmentId).Count();
            int pageNumber = (page ?? 1);
            
            return View(_ticketRepo.GetAllTickets().OrderByDescending(i=>i.TicketID).ToList().ToPagedList(pageNumber, 15));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="TicketID,TicketGroupID,TicketSubject,TicketDescription,TicketAttachment,TicketStatus")] Ticket ticket
            , HttpPostedFileBase TicketAttachmentUpload)
        {
            var result = new {isValid= true};
            
            if (ModelState.IsValid)
            {

                ticket.TicketStatus = "در انتظار بررسی";
                ticket.TicketDate = DateTime.Now;
                ticket.AccountID = Int32.Parse(Session["AccountID"].ToString());
                
                if(TicketAttachmentUpload != null)
                {
                    ticket.TicketAttachment = Guid.NewGuid() + Path.GetExtension(TicketAttachmentUpload.FileName);
                    TicketAttachmentUpload.SaveAs(Server.MapPath("/TicketAttachments/" + ticket.TicketAttachment));
                }

                _tkContext.Tickets.Add(ticket);
                _tkContext.SaveChanges();
                //await JS.InvokeVoidAsync("displayAlert");
                //return RedirectToAction("TicketView");
                return Json(result);
            }
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            ViewBag.IsSuccess = true;
            return View();
        }
        public ActionResult TicketReply(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Ticket ticket = _tkContext.Tickets.Find(id);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TicketReply([Bind]Ticket ticket, string replyText, HttpPostedFileBase TicketReplyAttachmentUpload)
        {
            Ticket ticket1 = _tkContext.Tickets.Find(ticket.TicketID);
            if (ModelState.IsValid)
            {
                TicketReply reply = new TicketReply();
                reply.TicketID = ticket.TicketID;
                reply.Text = replyText;
                reply.AccountID = Int32.Parse(Session["AccountID"].ToString());
                reply.ReplyDate = DateTime.Now;
                if (TicketReplyAttachmentUpload != null)
                {
                    reply.TicketReplyAttachment = Guid.NewGuid() + Path.GetExtension(TicketReplyAttachmentUpload.FileName);
                    TicketReplyAttachmentUpload.SaveAs(Server.MapPath("/TicketAttachments/" + reply.TicketReplyAttachment));
                }
                _ticketReplyRepo.InsertTicketReply(reply);
                _ticketReplyRepo.Save();
                ticket1.TicketReply.Add(reply);
                _ticketRepo.UpdateTicket(ticket1);
                _ticketRepo.Save();
            }
            return RedirectToAction("TicketView");
            //return View(ticket1);
        }
        public ActionResult TicketDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepo.GetTicketById(id.Value);
            
            if(ticket.TicketStatus == "در انتظار بررسی")
                ticket.TicketStatus = "در حال بررسی";
            
            _ticketRepo.UpdateTicket(ticket);
            _ticketRepo.Save();
            if (ticket == null)
                return HttpNotFound();
            return View(ticket);
        }

        public ActionResult CloseTicket(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepo.GetTicketById(id.Value);
            ticket.TicketStatus = "بسته شده";
            _ticketRepo.UpdateTicket(ticket);
            _ticketRepo.Save();
            return RedirectToAction("TicketView");

        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            if ((searchString.IsNullOrWhiteSpace()))
            {
                return RedirectToAction("TicketView");
            }
            int pageNumber = 1;
            var model = _ticketRepo.GetAllTickets().Where(item => item.TrackCode == searchString).ToPagedList(pageNumber, 15);
            return View("TicketView", model);
        }
    }
}