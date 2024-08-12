using Shopping.Entity.Models;
using ShoppingSite.Authorize;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    [AuthAdmin]
    public class ProductController : Controller
    {
        // GET: Product
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryList()
        {
            var category = db.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        public ActionResult AddCategory(int? Id)
        {
            if (Id == null)
            {
                return View();
            }
            else
            {
                var category = db.Categories.Find(Id);

                return View(category);
            }
        }
            [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            var dbCategory = db.Categories.FirstOrDefault(x=> x.Id == c.Id);
            if(dbCategory == null) 
            { 
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            else
            {
                dbCategory.Name = c.Name;
                db.SaveChanges();
                return RedirectToAction("CategoryList");
            }
        }
        public ActionResult DeleteCategory(int Id)
        {
            var category = db.Categories.Find(Id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public ActionResult AddProduct(int? Id) 
        { 
            ViewBag.Categories = db.Categories.ToList();
            if(Id == null)
            { 
                return View();
            }
            else
            {
                var product = db.Products.Find(Id);
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult AddProduct(Product p) 
        { 
            var dbProdcut = db.Products.FirstOrDefault(x => x.Id == p.Id);
            if(dbProdcut == null)
            { 
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
            else
            {
                dbProdcut.Name = p.Name;
                dbProdcut.Barcode = p.Barcode;
                dbProdcut.Stock = p.Stock;
                dbProdcut.Price = p.Price;
                dbProdcut.CategoryId = p.CategoryId;
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
        }
        public ActionResult ProductList()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        public ActionResult DeleteProduct(int Id)
        {
            var product = db.Products.Find(Id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
        public ActionResult ProductDataTables()
        {
            var products = db.Products.ToList();
            return View(products);
        }
    }
}