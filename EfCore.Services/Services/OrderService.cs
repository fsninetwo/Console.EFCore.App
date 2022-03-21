using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Data.IRepositories;
using EfCore.Services.IServices;
using EfCore.Data.Models;
using AutoMapper;
using EfCore.Services.Helpers;

namespace EfCore.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IUserService userService, IProductService productService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderCreateDTO order)
        {
            var user = await _userService.GetUserAsync(1);

            var newOrder = OrderHelper.ConvertOrderDTOtoRating(order, user.Id);

            try
            {
                await _orderRepository.AddOrderAsync(newOrder);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("Error, ", ex.Message);
            }
        }

        public async Task<OrderDTO> GetOrderAsync(long orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);

            if (order is null)
            {
                return null;
            }

            var orderDetailsIds = order.OrderDetails.Select(x => x.Id).ToList();

            var orderModel = _mapper.Map<OrderDTO>(order);

            var products = await _productService.GetProductsAsync(orderDetailsIds);

            if (products is null)
            {
                throw new InternalException("No existing products for this entity");
            }

            foreach (var orderDetails in orderModel.OrderDetails)
            {
                var product = products.FirstOrDefault(x => x.Id == orderDetails.Id);

                orderDetails.Product = product.Name;
            }

            return orderModel;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync(long orderId)
        {
            var orders = await _orderRepository.GetOrdersAsync(orderId);

            if (orders is null)
            {
                return new List<OrderDTO>();
            }

            var orderList = new List<OrderDTO>();

            var orderDetailsIds = orders.SelectMany(x => x.OrderDetails.Select(xl => xl.Id)).Distinct().ToList();

            var products = await _productService.GetProductsAsync(orderDetailsIds);

            if (products is null)
            {
                throw new InternalException("No existing products for this entity");
            }

            foreach(var order in orders)
            {
                var orderModel = _mapper.Map<OrderDTO>(order);

                foreach (var orderDetailsModel in orderModel.OrderDetails)
                {
                    var orderDetails = order.OrderDetails.FirstOrDefault(x => x.Id == orderDetailsModel.Id);

                    var product = products.FirstOrDefault(x => x.Id == orderDetails.ProductId);

                    orderDetailsModel.Product = product.Name;
                }

                orderList.Add(orderModel);
            }

            return orderList;
        }

        public async Task UpdateOrderAsync(OrderUpdateDTO order)
        {
            var updatedOrder = OrderHelper.ConvertOrderDTOtoRating(order);
            try
            {
                await _orderRepository.UpdateOrderAsync(updatedOrder);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("Error, " + ex.Message);
            }
            
        }
    }
}
