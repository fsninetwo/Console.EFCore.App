using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EfCore.Data.IRepositories;
using EfCore.Data.Repositories;
using EfCore.Migrations.Configuration;
using EfCore.Services.IServices;
using EfCore.Services.Services;
using EFCore.App.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.App.Extensions
{
    static class ServiceExtensions
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRatingService, RatingService>();
        }

        public static void AddAutoMapperService(this IServiceCollection services)
        {
            var autoMapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappers()); 
            });

            var mapper = autoMapper.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
