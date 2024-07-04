using Shopping.Entity.Models;
using ShoppingSite.Models.ViewModels;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class SaleController : Controller
    {
        DataContext db = new DataContext();
        
        // GET: Sale
        public ActionResult Index()
        {
            //Ürünleri Getir

            var urunler = db.Products.ToList();

            ViewBag.Products = urunler;

            return View();
        }

        [HttpGet]
        public ActionResult AddToCart()
        {
            var products = db.Products.ToList();
            var users = db.Users.ToList();

            ViewBag.Products = products;
            ViewBag.Users = users;

            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(Sale s)
        { 
            s.Date = DateTime.Now;
            db.Sales.Add(s);
            db.SaveChanges();
            return RedirectToAction("AddToCart");
        }
           
        [HttpGet]
        public ActionResult GetProductPrice(int productId)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                return Json(product.Price, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Ürün bulunamadı", JsonRequestBehavior.AllowGet);
            }
        }
    }
}