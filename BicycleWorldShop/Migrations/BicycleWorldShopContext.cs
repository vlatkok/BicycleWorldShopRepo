using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BicycleWorldShop.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BicycleWorldShop.DAL
{
    public class BicycleWorldShopContext:DbContext

    {
        public BicycleWorldShopContext() : base("BicycleWorldShop2")
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }
      

        public DbSet<OrderedProduct> Orderedproducts { get; set; }    

        public DbSet<Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<BicycleWorldShop.Models.Review> Reviews { get; set; }
    }
}