using System;
using System.IO;
using AutoMapper;
using EfCore.Data.IRepositories;
using EfCore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EfCore.Migrations;
using EfCore.Migrations.Configuration;
using EfCore.Services.IServices;
using EfCore.Services.Services;
using EFCore.App.Extensions;
using EFCore.App.Mappers;

namespace EFCore.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

        }

        static IServiceCollection ConfigureServices()
        {
            var appSettings = AppSettingsConfiguration.GetAppSettings(Directory.GetCurrentDirectory());

            var services = new ServiceCollection();

            
            services.AddDbContext<EfCoreDbContext>
                (options => options.UseSqlServer(appSettings.ConnectionString));
            services.AddLogging();
            services.AddAutoMapperService();
            services.AddDependencyInjectionServices();
            services.BuildServiceProvider();

            return services;
        }
    }
}
