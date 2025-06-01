using Kavenegar.Exceptions;
using Kavenegar;
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
using System.Web.Hosting;
using Hangfire;

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
            var userGroupId = Int32.Parse(Session["UserGroupId"].ToString());
            if (userGroupId == 1) //if it was manager
            {
                var tickets = db.Tickets.Include(t => t.Account).Include(t => t.TicketGroup).Include(t => t.UserGroup).Where(i => i.TicketGroupID == 5).ToList(); //مشاهده درخواست ها فقط
                return View(tickets.ToList());
            }
            else
            {
                var tickets = db.Tickets.Include(t => t.Account).Include(t => t.TicketGroup).Include(i => i.UserGroup).Where(i => i.UserGroupID == userGroupId);
                try { return View(tickets.ToList()); }
                catch { return View(); }
            }
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
            var result = new { isValid = true };
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
                BackgroundJob.Schedule(() => SendReplyToUser(ticket.TicketID), TimeSpan.FromSeconds(30));
                try
                {
                    var receptors = new List<string> { "09132451970", "09353880336", "09331283198", "09380457496" };

                    var api = new KavenegarApi("46537A513461493231475167624E615873464B726D5449554A42364D57777062445A6E35556C71784653383D");
                    var r = api.Send("20001327", receptors, "تیکت جدیدی از طرف " + Session["FullName"].ToString() + " ثبت شد");

                }
                catch (ApiException ex)
                {
                    // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.

                }
                catch (Kavenegar.Exceptions.HttpException ex)
                {
                    // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد

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



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

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
        public ActionResult TicketReply([Bind] Ticket ticket, string replyText, HttpPostedFileBase TicketReplyAttachmentUpload)
        {
            Ticket ticket1 = db.Tickets.Find(ticket.TicketID);
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
            try
            {
                var api = new KavenegarApi("46537A513461493231475167624E615873464B726D5449554A42364D57777062445A6E35556C71784653383D");

                var receptors = new List<string> { "09132451970", "09353880336" };
                var r = api.Send("20001327", receptors, "تیکت شما پاسخ داده شد " + "\nشماره تیکت" + ticket1.TicketID.ToString());

            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.

            }
            catch (Kavenegar.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد

            }
            return RedirectToAction("Index");
            //return View(ticket1);
        }


        [HttpPost]
        public JsonResult CreatePostAjax(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                return Json(ticket);
            }
            return Json(null);
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
            return RedirectToAction("Details");

        }

        #region function
        private string GenerateTrackingCode()
        {
            string code = "#" + Guid.NewGuid().ToString();
            return code;
        }

        public void SendReplyToUser(int ticketId)
        {
            var ticket1 = db.Tickets.Where(i => i.TicketID == ticketId).FirstOrDefault();
            TicketReply reply = new TicketReply();

            reply.TicketID = ticketId;
            reply.Text = "سلام. تیکت شما در دست بررسی قرار گرفت. لطفا شکیبا باشید.";
            reply.AccountID = db.Accounts.Where(i => String.Compare("software", i.Username) == 0).FirstOrDefault().AccountID;
            reply.ReplyDate = DateTime.Now;
            _ticketReplyRepo.InsertTicketReply(reply);
            //ticket1.TicketReply.Add(reply);
            //_ticketRepo.UpdateTicket(ticket1);
            _ticketReplyRepo.Save();
            var ticket2 = db.Tickets.Where(i=>i.TicketID == ticketId).FirstOrDefault();
            ticket2.LastReplyDateTime = DateTime.Now;
            _ticketRepo.UpdateTicket(ticket2);
            _ticketReplyRepo.Save();

            try
            {
                var api = new KavenegarApi("46537A513461493231475167624E615873464B726D5449554A42364D57777062445A6E35556C71784653383D");

                var receptor = db.Tickets.Where(i => i.AccountID == ticket1.AccountID).FirstOrDefault().Account.Phonenumber;
                var receptorName = db.Tickets.Where(i => i.AccountID == ticket1.AccountID).FirstOrDefault().Account.FullName;
                var r = api.Send("20001327", receptor, "تیکت شما پاسخ داده شد " + receptorName + "" + "\nشماره تیکت" + ticket1.TicketID.ToString());

            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.

            }
            catch (Kavenegar.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد

            }
            #endregion
        }
    }
}
