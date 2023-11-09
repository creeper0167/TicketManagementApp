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
    public class TicketsController : Controller
    {
        private ITicketRepo ticketRepository;

        public TicketsController()
        {
            ticketRepository = new TicketService();
        }
        // GET: Admin/Tickets
        public ActionResult Index()
        {
            //var tickets = ticket.Tickets.Include(t => t.TicketGroup);
            return View(ticketRepository.GetAllTickets());
        }

        // GET: Admin/Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Admin/Tickets/Create
        public ActionResult Create()
        {
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle");
            return View();
        }

        // POST: Admin/Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,TicketGroupID,TicketSubject,TicketDescription")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticketRepository.InsertTicket(ticket);
                ticketRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            return View(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TicketGroupID,TicketSubject,TicketDescription")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticketRepository.UpdateTicket(ticket);
                ticketRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.TicketGroupID = new SelectList(new TicketGroupService().GetAllTicketGroups(), "TicketGroupID", "TicketGroupTitle", ticket.TicketGroupID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = ticketRepository.GetTicketById(id);
            ticketRepository.DeleteTicket(ticket);
            ticketRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ticketRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
