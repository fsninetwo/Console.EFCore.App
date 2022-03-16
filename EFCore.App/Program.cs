using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EfCore.Migrations;
using EfCore.Migrations.Configuration;

namespace Console.EFCore.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = AppSettingsConfiguration.GetAppSettings(Directory.GetCurrentDirectory());

            var serviceProvider = new ServiceCollection();

            serviceProvider.AddDbContext<EfCoreContext>
                (options => options.UseSqlServer(appSettings.ConnectionString));
            serviceProvider.AddSingleton<IAppSettings, AppSettings>();
            serviceProvider.BuildServiceProvider();
        }
    }
}
