using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagementApp.Context;
using TicketManagementApp.Models;

namespace TicketManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private TkContext _tkContext;
        public LoginController()
        {
            _tkContext = new TkContext();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Accounts objUser)
        {
            if (ModelState.IsValid)
            {
                    var obj = _tkContext.Accounts.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AccountID"] = obj.AccountID.ToString();
                        Session["Username"] = obj.Username.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            return View("~/Views/Ticket/TicketView.cshtml");
        }

    }
}