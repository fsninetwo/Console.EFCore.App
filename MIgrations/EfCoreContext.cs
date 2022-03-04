using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Migrations
{
    public class EfCoreContext : DbContext
    {
        public DbSet<Currency> Currencies;

        public DbSet<Order> Orders;

        public DbSet<OrderDetails> OrderDetails;

        public DbSet<Product> Products;

        public DbSet<Rating> Ratings;

        public DbSet<User> Users;

        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {

        }
    }
}
