using EfCore.Data.IRepositories;
using EfCore.Entities.Entities;
using EfCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EfCore.Data.Repositories;
using EfCore.Domain.Exceptions;
using MockQueryable.Moq;
using Xunit;

namespace EfCore.Data.Tests
{
    public class OrderRepositoryTests
    {
        private readonly Mock<EfCoreDbContext> _mockDbContext;
        private readonly Mock<DbSet<Order>> _mockOrderDbSet;
        private readonly IOrderRepository _orderRepository;

        private readonly List<Order> _orders = new List<Order>()
        {
            new Order()
            {
                Id = 1,
                UserId = 1,
                OrderDetails = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 1, ProductId = 1 },
                    new OrderDetails() { Id = 2, ProductId = 2 }
                }
            },
            new Order()
            {
                Id = 2,
                UserId = 1,
                OrderDetails = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 1, ProductId = 1 },
                    new OrderDetails() { Id = 3, ProductId = 3 },
                    new OrderDetails() { Id = 4, ProductId = 4 }
                }
            },
            new Order()
            {
                Id = 3,
                UserId = 2,
                OrderDetails = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 3, ProductId = 3 }
                }
            },
            new Order()
            {
                Id = 4,
                UserId = 2,
                OrderDetails = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 3, ProductId = 2 },
                    new OrderDetails() { Id = 4, ProductId = 4 }
                }
            }
        };

        private readonly List<OrderDetails> _orderDetails = new List<OrderDetails>()
        {
            new OrderDetails() {Id = 5, ProductId = 1},
            new OrderDetails() {Id = 6, ProductId = 2},
            new OrderDetails() {Id = 7, ProductId = 3},
            new OrderDetails() {Id = 8, ProductId = 4}
        };

        public OrderRepositoryTests()
        {
            _mockDbContext = new Mock<EfCoreDbContext>();

            _mockOrderDbSet = _orders
                .AsQueryable()
                .BuildMockDbSet();

            _mockDbContext
                .Setup(c => c.Set<Order>())
                .Returns(_mockOrderDbSet.Object);

            _mockOrderDbSet.Setup(x => x.Add(It.IsAny<Order>()))
                .Callback<Order>(addOrder => {
                    _orders.Add(new Order
                    {
                        Id = 5,
                        Guid = addOrder.Guid,
                        Payment = addOrder.Payment,
                        PurchaseDate = addOrder.PurchaseDate,
                        OrderDetails = addOrder.OrderDetails,
                        UserId = addOrder.UserId,
                    });
                });

            _mockOrderDbSet.Setup(x => x.Update(It.IsAny<Order>()))
                .Callback<Order>(updatedOrder =>
                {
                    var order = _orders.FirstOrDefault(x => x.Id == updatedOrder.Id);

                    if(order == null) throw new InternalException("Order is not exist");

                    order.Guid = updatedOrder.Guid;
                    order.Payment = updatedOrder.Payment;
                    order.PurchaseDate = updatedOrder.PurchaseDate;
                    order.OrderDetails = updatedOrder.OrderDetails;
                    order.UserId = updatedOrder.UserId;
                });

            _mockOrderDbSet.Setup(x => x.Remove(It.IsAny<Order>()))
                .Callback<Order>(removeOrder => { _orders.Remove(removeOrder); });

            _orderRepository = new OrderRepository(_mockDbContext.Object);
        }

        [Fact]
        public async void GetOrder_ShouldReturnValidData()
        {
            var orderId = 1;

            var result = await _orderRepository.GetOrderAsync(orderId);

            Assert.Equal(_orders.FirstOrDefault(), result);
        }

        [Fact]
        public async void GetOrders_ShouldReturnValidData()
        {
            var userId = 2;

            var result = await _orderRepository.GetOrdersAsync(userId);

            var expected = _orders.Where(x => x.UserId == userId).ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void GetOrder_ShouldAddOrder()
        {
            var orderId = 5;

            await _orderRepository.AddOrderAsync(new Order
            {
                UserId = 2,
                OrderDetails = _orderDetails
            });

            Assert.Equal(5, _orders.Count);
            Assert.Contains(_orders, x => x.Id.Equals(orderId));

            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

            _mockOrderDbSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async void GetOrders_ShouldUpdateOrder()
        {
            var userId = 3;

            await _orderRepository.UpdateOrderAsync(new Order()
            {
                Id = 1,
                UserId = userId,
                OrderDetails = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 1, ProductId = 1 },
                    new OrderDetails() { Id = 2, ProductId = 2 }
                }
            });

            Assert.Equal(4, _orders.Count);
            Assert.Contains(_orders, x => x.UserId == userId);

            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

            _mockOrderDbSet.Verify(m => m.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async void GetOrders_ShouldDeleteOrder()
        {
            var orderId = 1;

            await _orderRepository.DeleteOrderAsync(1);

            Assert.Equal(3, _orders.Count);
            Assert.Contains(_orders, x => x.Id != orderId);

            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

            _mockOrderDbSet.Verify(m => m.Remove(It.IsAny<Order>()), Times.Once);
        }
    }
}
