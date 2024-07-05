using Shopping.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite.Models.ViewModels
{
    public class CartItemViewModel
    {
        private List<Cart> _cartLines = new List<Cart>();

        public List<Cart> CartLines 
            { 
            get { return _cartLines; } 
        }

        public void AddProduct(Product product, int quantity)
        {
            var line = _cartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line == null)
            {
                _cartLines.Add(new Cart() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            _cartLines.RemoveAll(x => x.Product.Id == product.Id);
        }

        public decimal Total()
        {
            return _cartLines.Sum(x => x.Product.Price * x.Quantity);
        }
        public void Clear()
        {
            _cartLines.Clear(); 
        }


    }


    public class Cart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }


    }
}