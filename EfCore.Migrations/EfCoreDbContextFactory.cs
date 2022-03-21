using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using EfCore.Migrations;
using EfCore.Migrations.Configuration;

namespace Migrations
{
    class EfCoreDbContextFactory : IDesignTimeDbContextFactory<EfCoreDbContext>
    {
        public EfCoreDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true);

            IConfigurationRoot configuration = builder.Build();
            var settings = new AppSettings();
            configuration.Bind(settings);

            var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>();
            optionsBuilder.UseSqlServer(settings.ConnectionString,
                b => b.MigrationsAssembly(typeof(EfCoreDbContext).Assembly.FullName)
            );

            Console.WriteLine("Migration is started");
            return new EfCoreDbContext(optionsBuilder.Options);
        }
    }
}
