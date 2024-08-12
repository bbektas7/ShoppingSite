using Shopping.Entity.Models;
using ShoppingSite.Models.ViewModels;
using Shpping.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        DataContext db = new DataContext();
        public ActionResult Index(string start, string end)
        {
            var saleDetail = db.SaleDetails.ToList();

            DateTime startDate, endDate;

            if (!DateTime.TryParseExact(start, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) ||
                !DateTime.TryParseExact(end, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                startDate = DateTime.Today.AddDays(-7); 
                endDate = DateTime.Today;
            }
            DateTime newEndDate = endDate.AddDays(1);
            var sales = db.Sales.Where(s => s.Date >= startDate && s.Date <= newEndDate).ToList();
            foreach(var y in saleDetail) 
            {
                foreach (var x in sales)
                {
                    if(x.Date == y.Sale.Date) 
                    {   
                        if(GetReport().ReportLines.Count == 0)
                        {
                            GetReport().AddToReport(y.Product,y.Quantity,(y.Quantity*y.Product.Price));
                        }
                        else
                        {

                            var exist = GetReport().ReportLines.FirstOrDefault(b =>b.Product.Name == y.Product.Name);
                            if(exist != null)
                            {
                                exist.Quantity += y.Quantity;
                                exist.TotalPrice += y.Price; 
                            }
                            else
                            {
                                GetReport().AddToReport(y.Product, y.Quantity, (y.Quantity * y.Product.Price));
                            }
                        }
                    }
                }
            }

            return View(GetReport());
        }
        public ReportViewModel GetReport()
        {
            var reportViewModel = (ReportViewModel)TempData["ReportViewModel"];
            if (reportViewModel == null)
            {
                reportViewModel = new ReportViewModel();
                TempData["ReportViewModel"] = reportViewModel;
            }
            return reportViewModel;
        }
        /*[HttpGet]
        public JsonResult GetSalesReport(string date)
        {
            DateTime start = DateTime.Now.AddDays(-7);
            DateTime end = DateTime.Now;

            var salesDetails = db.Sales
                                 .Where(s => s.Date >= start && s.Date <= end)
                                 //.Select(s => new
                                 //{
                                 //    ProductName = s.Product.Name,
                                 //    Quantity = s.Quantity,
                                 //    TotalPrice = s.Quantity * s.Price,
                                 //    Date = s.Sale.Date.ToString("yyyy-MM-dd")
                                 //})
                                 .ToList();

            return Json(new { data = salesDetails }, JsonRequestBehavior.AllowGet);
        }*/

        /*[HttpPost]
        public ActionResult GetReport(String startDate, String endDate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var report = GetSalesReport(parsedStartDate, parsedEndDate);
            return Json(report, JsonRequestBehavior.AllowGet);
        }

        private List<ReportViewModel> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            // Yukarıdaki GetSalesReport metodu burada kullanılabilir
            using (var context = new DataContext())
            {
                var report = from sd in context.SaleDetails
                             join s in context.Sales on sd.SaleId equals s.Id
                             join p in context.Products on sd.ProductId equals p.Id
                             where s.Date >= startDate && s.Date <= endDate
                             group new { sd, p } by  p.Name  into g
                             select new ReportViewModel
                             {
                                 ProductName = g.Key,
                                 Quantity = g.Sum(x => x.sd.Quantity),
                                 TotalPrice = g.Sum(x => x.sd.Price)
                             };

                return report.ToList();
            }
        }*/
    }
}
