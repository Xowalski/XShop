using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Models;

namespace XShop.DataAccess.SQL
{
    public class DataContext : DbContext 
    {
        public DataContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
