using System;
using System.Collections.Generic;
using EfCore.Entities.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class DataInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dbContextFactory = new EfCoreDbContextFactory();
            var context = dbContextFactory.CreateDbContext(null);
            context.SaveChanges();

            var users = new List<User>
            {
                new User
                {
                    Login = "admin",
                    Email = "admin@admin.com",
                    Password = "12345678",
                    UserGroup = UserGroup.Administrator
                },
                new User
                {
                    Login = "user",
                    Email = "user@user.com",
                    Password = "qwerty",
                    UserGroup = UserGroup.User
                },
            };

            context.AddRange(users);
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Name = "EfCore",
                    Developer = "Microsoft",
                    Publisher = "Microsoft",
                },
                new Product
                {
                    Name = "Context",
                    Developer = "Microsoft",
                    Publisher = "Microsoft",
                }
            };
            context.AddRange(products);
            context.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
