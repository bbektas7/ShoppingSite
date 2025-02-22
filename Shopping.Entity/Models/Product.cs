﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entity.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
