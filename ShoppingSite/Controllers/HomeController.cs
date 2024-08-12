using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoppingSite.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        DataContext db = new DataContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var totalStock = TotalStock();
            ViewBag.TotalStock = totalStock;

            var totalProduct = ProductCount();
            ViewBag.TotalProduct = totalProduct;

            var totalAmount = TotaLAmount();
            ViewBag.TotalAmount = totalAmount;
            return View();
        }
        public int TotalStock()
        {
            var products = db.Products.ToList();
            var totalStock = products.Sum(p => p.Stock);
            return totalStock;
        }
        public int ProductCount()
        {
            var products = db.Products.ToList();
            var totalProduct = products.Count();
            return totalProduct;
        }
        public decimal TotaLAmount()
        {
            var sales = db.Sales.ToList();
            var totalAmount = sales.Sum( x => x.TotalAmount);
            return totalAmount;
        }
    }
}