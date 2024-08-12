using ShoppingSite.Models.ViewModels;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        DataContext db = new DataContext();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.IsDeleted == false);
                if (user != null)
                {
                    Session["User"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}