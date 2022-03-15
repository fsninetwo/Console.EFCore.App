using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EfCore.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Migrations
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
      
    }
}
