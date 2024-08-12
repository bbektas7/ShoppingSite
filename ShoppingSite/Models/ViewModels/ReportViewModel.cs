using Microsoft.Ajax.Utilities;
using Shopping.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite.Models.ViewModels
{
    public class ReportViewModel
    {

        private List<ReportLine> _reportLines = new List<ReportLine>();
        public List<ReportLine> ReportLines
        {
            get { return _reportLines; }
        }
    
        public void AddToReport(Product product, int quantity, decimal totalPrice)
        {
            _reportLines.Add(new ReportLine { Product = product, Quantity = quantity, TotalPrice = totalPrice });
        }
    }
    public class ReportLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
   

}