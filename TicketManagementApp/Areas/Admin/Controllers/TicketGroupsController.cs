using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketManagementApp.Context;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories;
using TicketManagementApp.Repositories.Services;

namespace TicketManagementApp.Areas.Admin.Controllers
{
    public class TicketGroupsController : Controller
    {
        private ITicketGroupRepo ticketGroupRepository;

        public TicketGroupsController()
        {
            ticketGroupRepository = new TicketGroupService();
        }
        // GET: Admin/TicketGroups
        public ActionResult Index()
        {
            return View(ticketGroupRepository.GetAllTicketGroups());
        }

        // GET: Admin/TicketGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = ticketGroupRepository.GetTicketGroupById(id.Value);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return View(ticketGroup);
        }

        // GET: Admin/TicketGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TicketGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketGroupID,TicketGroupTitle")] TicketGroup ticketGroup)
        {
            if (ModelState.IsValid)
            {
                ticketGroupRepository.InsertTicketGroup(ticketGroup);
                ticketGroupRepository.Save();
                return RedirectToAction("Index");
            }

            return View(ticketGroup);
        }

        // GET: Admin/TicketGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = ticketGroupRepository.up(id);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return View(ticketGroup);
        }

        // POST: Admin/TicketGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketGroupID,TicketGroupTitle")] TicketGroup ticketGroup)
        {
            if (ModelState.IsValid)
            {
                ticketGroupRepository.UpdateTicketGroup(ticketGroup);
                ticketGroupRepository.Save();
                return RedirectToAction("Index");
            }
            return View(ticketGroup);
        }

        // GET: Admin/TicketGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = db.TicketGroups.Find(id);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return View(ticketGroup);
        }

        // POST: Admin/TicketGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketGroup ticketGroup = db.TicketGroups.Find(id);
            db.TicketGroups.Remove(ticketGroup);
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
    }
}
