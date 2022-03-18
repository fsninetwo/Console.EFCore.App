using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfCore.Services.IServices;
using Moq;
using Xunit;
using EFCore.App.Mappers;
using EfCore.Data.IRepositories;
using EfCore.Data.Models;
using EfCore.Services.Services;
using EfCore.Entities.Entities;

namespace EfCore.Services.Tests
{
    public class OrderServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMappers()); });
            _mapper = mappingConfig.CreateMapper();

            _mockUserService = new Mock<IUserService>();
            _mockProductService = new Mock<IProductService>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mapper, _mockOrderRepository.Object, _mockUserService.Object, _mockProductService.Object);
        }

        #region TestData

        private readonly long validUserId = 1;

        private readonly List<long> validOrderDetailIds = new List<long>() {1, 2, 3, 4};

        private readonly User validUser = new User() { Id = 1, Login = "Test" };
        
        private readonly List<Order> validOrders = new List<Order>()
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
            }
        };

        private readonly List<OrderDTO> validOrdersDTO = new List<OrderDTO>()
        {
            new OrderDTO()
            {
                Id = 1,
                UserId = 1,
                OrderDetails = new List<OrderDetailsDTO>()
                {
                    new OrderDetailsDTO() { Id = 1, Product = "Test 1" },
                    new OrderDetailsDTO() { Id = 2, Product = "Test 2" }
                }
            },
            new OrderDTO()
            {
                Id = 2,
                UserId = 1,
                OrderDetails = new List<OrderDetailsDTO>()
                {
                    new OrderDetailsDTO() { Id = 1, Product = "Test 1" },
                    new OrderDetailsDTO() { Id = 3, Product = "Test 3" },
                    new OrderDetailsDTO() { Id = 4, Product = "Test 4" }
                }
            }
        };

        private readonly List<ProductDTO> validProducts = new List<ProductDTO>()
        {
            new ProductDTO()
            {
                Id = 1,
                Name = "Test 1",
                Ratings = new List<RatingDTO> { new RatingDTO() { Id = 1, UserName = "Test" }}

            },
            new ProductDTO()
            {
                Id = 2,
                Name = "Test 2",
                Ratings = new List<RatingDTO> { new RatingDTO() { Id = 2, UserName = "Test" }}
            },
            new ProductDTO()
            {
                Id = 3,
                Name = "Test 3",
                Ratings = new List<RatingDTO>()
            },
            new ProductDTO()
            {
                Id = 4,
                Name = "Test 4",
                Ratings = new List<RatingDTO>()
            }
        };



        #endregion


        [Fact]
        public async Task GetOrdersAsync_ShouldReturnValidOrders()
        {
            var userId = 1;

            _mockOrderRepository.Setup(x => x.GetOrdersAsync(1, It.IsAny<bool>()))
                .ReturnsAsync(() => validOrders);

            _mockProductService.Setup(x => x.GetProductsAsync(validOrderDetailIds))
                .ReturnsAsync(() => validProducts);

            var result = await _orderService.GetOrdersAsync(userId);

            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(result[i].Id, validOrdersDTO[i].Id);
                Assert.Equal(result[i].UserId, validOrdersDTO[i].UserId);
                Assert.Equal(result[i].OrderDetails, validOrdersDTO[i].OrderDetails);
            }

        }
    }
}