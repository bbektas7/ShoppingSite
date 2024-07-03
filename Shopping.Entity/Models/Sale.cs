using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Entity.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
