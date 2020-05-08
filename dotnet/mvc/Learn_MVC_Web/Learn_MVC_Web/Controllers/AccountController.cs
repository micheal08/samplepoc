using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_MVC_Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string userName, string password)
        {
            if (userName == "admin" && password == "manager")
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("InvalidLogin");
        }

        public ActionResult InvalidLogin()
        {
            return View();
        }
    }
}