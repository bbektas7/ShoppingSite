using ShoppingSite.Models.ViewModels;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Entity.Models;

namespace ShoppingSite.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            var products = db.Products.ToList();

            ViewBag.Products = products;
            ViewBag.Users = users;
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null) 
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public CartItemViewModel GetCart()
        {
            var cartItemViewModel = (CartItemViewModel)Session["CartItemViewModel"];
            if (cartItemViewModel == null)
            {
                cartItemViewModel = new CartItemViewModel();
                Session["cartItemViewModel"] = cartItemViewModel;
            }
            return cartItemViewModel;
        }
        [HttpGet]
        public ActionResult AddSale()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddSale(Sale s)
        {
            s.Date = DateTime.Now;
            s.TotalAmount = GetCart().Total();
            db.Sales.Add(s);
            foreach (var item in GetCart().CartLines)
            {
                var product = db.Products.SingleOrDefault(x => x.Id == item.Product.Id);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }
            db.SaveChanges();
            GetCart().Clear();
            return View();
        }
    }
}