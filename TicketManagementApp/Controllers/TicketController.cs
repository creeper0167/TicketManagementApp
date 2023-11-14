using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
        private ITicketRepo _ticketRepo;

        public TicketController()
        {
            _tkContext = new TkContext();
            _ticketRepo = new TicketService();
        }
        // GET: Ticket
        public ActionResult Index()
        {
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            ViewBag.UserGroupID = new SelectList(new UserGroupService().GetAllUserGroups(), "UserGroupID", "UserGroupTitle");

            return View();
        }
        public ActionResult TicketView()
        {
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");

            return View(_ticketRepo.GetAllTickets());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="TicketID,TicketGroupID,TicketSubject,TicketDescription,TicketAttachment,TicketStatus")] Ticket ticket
            , HttpPostedFileBase TicketAttachmentUpload)
        {
            if (ModelState.IsValid)
            {

                ticket.TicketStatus = "در انتظار بررسی";
                ticket.TicketDate = DateTime.Now;
                ticket.AccountID = 1;
                
                if(TicketAttachmentUpload != null)
                {
                    ticket.TicketAttachment = Guid.NewGuid() + Path.GetExtension(TicketAttachmentUpload.FileName);
                    TicketAttachmentUpload.SaveAs(Server.MapPath("/TicketAttachments/" + ticket.TicketAttachment));
                }

                _tkContext.Tickets.Add(ticket);
                _tkContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            return View();
        }

        public ActionResult TicketDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepo.GetTicketById(id.Value);
            ticket.TicketStatus = "در حال بررسی";
            _ticketRepo.UpdateTicket(ticket);
            _ticketRepo.Save();
            if (ticket == null)
                return HttpNotFound();
            return View(ticket);
        }
    }
}