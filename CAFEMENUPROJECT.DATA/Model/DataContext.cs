using CAFEMENUPROJECT.DATA.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base(GetOptions())
        {
        }


        private static DbContextOptions GetOptions()
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), "Server=YOURSERVERNAME;Database=CAFEMENUPROJECT;integrated security=true;").Options;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
