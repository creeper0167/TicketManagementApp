using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories;
using TicketManagementApp.Repositories.Services;

namespace TicketManagementApp.Controllers
{
    public class UserController : Controller
    {
        private TkContext db = new TkContext();
        private ITicketReplyRepo _ticketReplyRepo = new TicketReplyService();
        private ITicketRepo _ticketRepo = new TicketService();
        private IMessageRepository emailService;

        // GET: User
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Account).Include(t => t.TicketGroup);
            try { return View(tickets.ToList()); }
            catch { return View(); }
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username");
            ViewBag.TicketGroupID = new SelectList(db.TicketGroups, "TicketGroupID", "TicketGroupTitle");
            ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupTitle");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentTitle");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TicketID,UserGroupID,TicketGroupID,AccountID,TicketSubject,TicketDescription,TicketAttachment,TicketStatus,TicketDate")] Ticket ticket, HttpPostedFileBase TicketAttachmentUpload, int usergroup, string departmentSelectList)
        {
            var result = new {isValid = true};
            var notValidResult = new { isValid = false };
            int departmentId = string.Compare(departmentSelectList, "softwareDepartment") == 0 ? 1 : 2;
            if (ModelState.IsValid)
            {

                ticket.TicketStatus = "در انتظار بررسی";
                ticket.TicketDate = DateTime.Now;
                ticket.AccountID = Int32.Parse(Session["AccountID"].ToString());
                ticket.UserGroupID = usergroup;
                ticket.TrackCode = GenerateTrackingCode();
                ticket.DepartmentId = departmentId;

                if (TicketAttachmentUpload != null)
                {
                    ticket.TicketAttachment = Guid.NewGuid() + Path.GetExtension(TicketAttachmentUpload.FileName);
                    TicketAttachmentUpload.SaveAs(Server.MapPath("/TicketAttachments/" + ticket.TicketAttachment));
                }

                db.Tickets.Add(ticket);
                db.SaveChanges();
                try
                {
                    emailService = new EmailService("test", "تیکت جدید ثبت شده است. لطفا سیستم تیکت را چک کنید" + "\n" + "متن پیام:" + "\n" + ticket.TicketDescription + "کد پیگیری:" + "\n" + ticket.TrackCode, "ticketing@kavehlogistics.com");
                    emailService.Send();
                }catch(Exception e)
                {
                    Console.WriteLine();
                }
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", ticket.AccountID);
            ViewBag.TicketGroupID = new SelectList(db.TicketGroups, "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupTitle");
            //return View(ticket);
            return Json(notValidResult);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", ticket.AccountID);
            ViewBag.TicketGroupID = new SelectList(db.TicketGroups, "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            return View(ticket);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TicketGroupID,AccountID,TicketSubject,TicketDescription,TicketAttachment,TicketStatus,TicketDate")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", ticket.AccountID);
            ViewBag.TicketGroupID = new SelectList(db.TicketGroups, "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            return View(ticket);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        //public ActionResult UserRouteAction() {
        //    if (Session["RoleID"])
        //    return View(); 
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TicketReply([Bind] Ticket ticket, string replyText)
        {
            Ticket ticket1 = db.Tickets.Find(ticket.TicketID);
            if (ModelState.IsValid)
            {
                TicketReply reply = new TicketReply();
                reply.TicketID = ticket.TicketID;
                reply.Text = replyText;
                reply.AccountID = Int32.Parse(Session["AccountID"].ToString());
                reply.ReplyDate = DateTime.Now;
                _ticketReplyRepo.InsertTicketReply(reply);
                _ticketReplyRepo.Save();
                ticket1.TicketReply.Add(reply);
                _ticketRepo.UpdateTicket(ticket1);
                _ticketRepo.Save();
            }
            return RedirectToAction("Index");
            //return View(ticket1);
        }


        [HttpPost]
        public JsonResult CreatePostAjax(Ticket ticket)
        {
            if(ModelState.IsValid)
            {
                return Json(ticket);
            }
            return Json(null);
        }

        #region function
        private string GenerateTrackingCode()
        {
            string code = "#" + Guid.NewGuid().ToString();
            return code;
        }
        #endregion
    }
}
