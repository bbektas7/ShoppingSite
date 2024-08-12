using Shopping.Entity.Models;
using ShoppingSite.Authorize;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    [AuthAdmin]
    public class UserController : Controller
    {
        // GET: User
        DataContext db = new DataContext();
        
        public ActionResult Index() //Listele
        {
           var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult Delete(int Id)
        {
            var user = db.Users.Find(Id);
            user.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ActiveAgain (int Id)
        {
            var user = db.Users.Find(Id);
            user.IsDeleted = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            if (Id == null)
            {
                return View();
            }
            else
            {
                var user = db.Users.Find(Id);

                return View(user);
            }
        }
        [HttpPost]
        public ActionResult Add(User user) 
        {
            var dbUser = db.Users.FirstOrDefault(x=> x.Id == user.Id);
            var existingUser = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Id != user.Id);

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(user);
            }


            if (dbUser == null)
            {
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                dbUser.FullName = user.FullName;
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.IsAdmin = user.IsAdmin;
                db.SaveChanges() ;

                return RedirectToAction("Index");
            }
        }
    }


}