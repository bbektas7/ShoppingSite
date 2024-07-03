using Shopping.Entity.Models;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
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
            db.Users.Remove(user);
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

            if (dbUser == null)
            {
                db.Users.Add(user);
                db.SaveChanges();

                TempData["OK"] = "User added successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                dbUser.FullName = user.FullName;
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.IsAdmin = user.IsAdmin;
                db.SaveChanges() ;

                TempData["OK"] = "User updated!";
                return RedirectToAction("Index");
            }
        }
    }


}