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

        public DbSet<Item> Products { get; set; }
        public DbSet<ItemCategory> ProductCategories { get; set; }
    }
}
