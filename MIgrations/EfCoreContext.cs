using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<Currency> Currencies;

        public DbSet<Order> Orders;

        public DbSet<OrderDetails> OrderDetails;

        public DbSet<Product> Products;

        public DbSet<Rating> Ratings;

        public DbSet<User> Users;
      
    }
}
