using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductList()
        {
            var products = db.Products.ToList();
            return PartialView(products);
        }
    }
}