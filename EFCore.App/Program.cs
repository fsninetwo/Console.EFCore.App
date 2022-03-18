using System;
using System.IO;
using EfCore.Data.IRepositories;
using EfCore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EfCore.Migrations;
using EfCore.Migrations.Configuration;
using EfCore.Services.IServices;
using EfCore.Services.Services;
using EFCore.App.Extensions;

namespace EFCore.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = BuildServices();

        }

        static IServiceCollection BuildServices()
        {
            var appSettings = AppSettingsConfiguration.GetAppSettings(Directory.GetCurrentDirectory());

            var services = new ServiceCollection();
            services.AddDbContext<EfCoreContext>
                (options => options.UseSqlServer(appSettings.ConnectionString));
            services.AddDependencyInjectionService();
            services.BuildServiceProvider();

            return services;
        }
    }
}
