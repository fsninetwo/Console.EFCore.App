using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EfCore.Data.Models;
using EfCore.Entities.Entities;
using EfCore.Services.IServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EFCore.App
{
    class ConsoleService : IHostedService
    {
        private readonly ILogger<ConsoleService> _logger;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IRatingService _ratingService;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public ConsoleService(ILogger<ConsoleService> logger, IUserService userService, IOrderService orderService, IProductService productService,
            IRatingService ratingService, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _userService = userService;
            _orderService = orderService;
            _productService = productService;
            _ratingService = ratingService;
            _applicationLifetime = applicationLifetime;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting project");

            _applicationLifetime.ApplicationStarted.Register(() => 
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var user = await _userService.GetUserAsync(2);
                        Console.WriteLine("User - " + JsonConvert.SerializeObject(user));

                        var userCredentials = await _userService.GetUserByCredentialsAsync("admin", "12345678");
                        Console.WriteLine("User credentials - " + JsonConvert.SerializeObject(userCredentials));

                        await _userService.UpdateUserAsync(
                            new UserUpdateDTO
                            {
                                Id = 2,
                                Login = "user", 
                                Email = "user@gmail.com",
                                Password = "qwerty",
                                UserGroup = UserGroup.User
                            });

                        Console.WriteLine("User is updated");

                        var updatedUser = await _userService.GetUserAsync(2);
                        Console.WriteLine("Updated user - " + JsonConvert.SerializeObject(updatedUser));

                        var product = await _productService.GetProductAsync(2);
                        Console.WriteLine("Product - " + JsonConvert.SerializeObject(product));

                        var products = await _productService.GetProductsAsync("c");
                        Console.WriteLine("Products - " + JsonConvert.SerializeObject(products));

                        await _ratingService.AddRatingAsync(new RatingCreateDTO
                        {
                            Rate = 6,
                            Message = "Good Product",
                            ProductId = 1,
                        });
                        Console.WriteLine("New rating is added");

                        var rating = await _ratingService.GetRatingAsync(1);
                        Console.WriteLine("Rating - " + JsonConvert.SerializeObject(rating));

                        var ratings = await _ratingService.GetRatingsAsync(1);
                        Console.WriteLine("Ratings - " + JsonConvert.SerializeObject(ratings));

                        await _orderService.AddOrderAsync(new OrderCreateDTO
                        {
                            Payment = "Paypal",
                            OrderDetails = new List<OrderDetailsCreateDTO>
                            { new OrderDetailsCreateDTO { ProductId = 1 } }
                        });
                        Console.WriteLine("New order is added");

                        await _orderService.AddOrderAsync(new OrderCreateDTO
                        {
                            Payment = "Paypal",
                            OrderDetails = new List<OrderDetailsCreateDTO>
                                { new OrderDetailsCreateDTO { ProductId = 2 } }
                        });
                        Console.WriteLine("New order is added");

                        var order = await _orderService.GetOrderAsync(1);
                        Console.WriteLine("Order - " + JsonConvert.SerializeObject(order));

                        var orders = await _orderService.GetOrdersAsync(1);
                        Console.WriteLine("Orders - " + JsonConvert.SerializeObject(orders));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unhandled Exception" + ex.Message);
                        _logger.LogError("Unhandled Exception" + ex.Message);
                    }
                    finally
                    {
                        _applicationLifetime.StopApplication();
                    }
                }, cancellationToken);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
