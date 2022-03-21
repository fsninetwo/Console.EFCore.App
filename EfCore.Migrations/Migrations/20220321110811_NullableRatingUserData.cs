using System;
using EfCore.Entities.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class NullableRatingUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dbContextFactory = new EfCoreDbContextFactory();
            var context = dbContextFactory.CreateDbContext(null);
            context.SaveChanges();

            var rating = new Rating
            {
                Rate = 5,
                Message = "Nothing Special",
                ProductId = 1,
                Created = DateTime.Now,
                Updated = DateTime.Now,              
            };

            context.Add(rating);
            context.SaveChanges();
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
