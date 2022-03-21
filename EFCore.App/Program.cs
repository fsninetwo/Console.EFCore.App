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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace EFCore.App
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        private static AppSettings appSettings;

        static async Task Main(string[] args)
        {
            appSettings = AppSettingsConfiguration.GetAppSettings(Directory.GetCurrentDirectory());

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.local.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting console host");
                await CreateHostBuilder(args).RunConsoleAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                
                .ConfigureServices(services =>
                {
                    services.AddHostedService<ConsoleService>();
                    services.AddDbContext<EfCoreDbContext>
                        (options => options.UseSqlServer(appSettings.ConnectionString));
                    services.AddLogging();
                    services.AddAutoMapperService();
                    services.AddDependencyInjectionServices();
                })
                .UseSerilog((hostContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
                });       
    }
}
