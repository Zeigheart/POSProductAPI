using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Product.WebAPI.Models
{
    public class POSStoreDbContext : DbContext
    {

        private static string ConnectionString { get; set; }
        public DbSet<Product> Products { get; set; }


        public POSStoreDbContext(DbContextOptions<POSStoreDbContext> options) : base(options)
        {
            Load();
        }

        public List<Product> GetProducts() => Products.Local.ToList<Product>();

        public static void SetConnectionString(String value)
        {
            ConnectionString = value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase();
        }

        private void Load()
        {
            //this.Database.GetDbConnection();
        }

        public int RemoveFromStock(SoldProduct item)
        {
            var p = Products.Where(x => x.productId.ToLower().Equals(item.productId.ToLower())).FirstOrDefault();
            if (p == null)
                return -1;
            if (item.units > p.units)
                return -2;

            p.units -= item.units;
            //this.SaveChanges();
            return 0;
        }
    }
}
