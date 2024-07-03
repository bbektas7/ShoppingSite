using Shopping.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpping.DataAccess.Context
{
    public class DataContext : DbContext
    { 
        public DataContext()
            : base(@"data source=LAPTOP-UI82RB7Q\SQLEXPRESS;initial catalog=ShoppingSiteDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
            Database.SetInitializer(new ShoppingInitializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

    }
        public class ShoppingInitializer : CreateDatabaseIfNotExists<DataContext>
        {
            protected override void Seed(DataContext db)
            {
                db.Users.Add(new User
                {
                    FullName = "Berkay Bektaş",
                    Username = "bbektas7",
                    Password = "123456",
                    IsAdmin = true,
                });
                db.SaveChanges();
            }
        }

}
