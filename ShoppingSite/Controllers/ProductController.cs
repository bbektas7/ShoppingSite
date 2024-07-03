using Shopping.Entity.Models;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CategoryList()
        {
            var category = db.Categories.ToList();
            return PartialView(category);
        }
        [HttpGet]
        public ActionResult AddCategory(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("AddCategory");
        }
        [HttpGet]
        public ActionResult AddProduct() 
        { 
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p) 
        { 
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("AddProduct");
        }
        public ActionResult ProductList()
        {
            var products = db.Products.ToList();
            return View(products);
        }

    }
}