using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaleList()
        {
            var sales = db.Sales.ToList();
            return View(sales);
        }
        public ActionResult Details(int? Id)
        {
            var SaleDetail = db.SaleDetails.Where(s => s.SaleId == Id).ToList();
            return View(SaleDetail);
        }
        public ActionResult DeleteDetail(int Id)
        {
            var product = db.SaleDetails.FirstOrDefault(s => s.Id == Id);
            product.Product.Stock += product.Quantity;
            product.Sale.TotalAmount -= product.Price;
            db.SaleDetails.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Details",new {Id = product.SaleId });
        }
        public ActionResult DeleteSale(int Id)
        {
            var sale = db.Sales.FirstOrDefault(s => s.Id == Id);
            var products = db.SaleDetails.Where(s => s.SaleId == Id).ToList();
            foreach (var product in products)
            {
                product.Product.Stock += product.Quantity;
            }
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("SaleList");
        }
    }
}