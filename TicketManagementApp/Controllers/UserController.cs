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

namespace TicketManagementApp.Controllers
{
    public class UserController : Controller
    {
        private TkContext db = new TkContext();

        // GET: User
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Account).Include(t => t.TicketGroup);
            return View(tickets.ToList());
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
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TicketID,TicketGroupID,AccountID,TicketSubject,TicketDescription,TicketAttachment,TicketStatus,TicketDate")] Ticket ticket, HttpPostedFileBase TicketAttachmentUpload)
        {
            var result = new {isValid = true};
            var notValidResult = new { isValid = false };
            if (ModelState.IsValid)
            {

                ticket.TicketStatus = "در انتظار بررسی";
                ticket.TicketDate = DateTime.Now;
                ticket.AccountID = Int32.Parse(Session["AccountID"].ToString());

                if (TicketAttachmentUpload != null)
                {
                    ticket.TicketAttachment = Guid.NewGuid() + Path.GetExtension(TicketAttachmentUpload.FileName);
                    TicketAttachmentUpload.SaveAs(Server.MapPath("/TicketAttachments/" + ticket.TicketAttachment));
                }

                db.Tickets.Add(ticket);
                db.SaveChanges();
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
    }
}
