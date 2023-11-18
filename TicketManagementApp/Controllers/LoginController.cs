using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
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
            var result = new {isValid = true};
            if (ModelState.IsValid)
            {
                    var obj = _tkContext.Accounts.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AccountID"] = obj.AccountID.ToString();
                        Session["Username"] = obj.Username.ToString();
                        Session["FullName"] = obj.FullName.ToString();
                        Session["RoleID"] = obj.Role.RoleId.ToString();
                        Session["RoleName"] = obj.Role.RoleName.ToString();
                        //FormsAuthentication.Authenticate()
                        
                        return RedirectToAction("UserDashBoard");
                    //return View(result);
                    }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            int roldId = Int32.Parse(Session["RoleID"].ToString());
            if (Session["Username"] != null)
            {
                if (roldId == 1)
                    return RedirectToAction("TicketView", "Ticket");
                else
                    return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "Login details are wrong.");
            return View("Login");
        }

    }
}