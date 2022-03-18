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
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IUserService userService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
        }

        public async Task AddOrderAsync(OrderCreateDTO order)
        {
            var newOrder = OrderHelper.ConvertOrderDTOtoRating(order);
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

            var orderModel = _mapper.Map<OrderDTO>(order);

            return orderModel;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync(long orderId)
        {
            var orders = await _orderRepository.GetOrdersAsync(orderId);

            var orderList = new List<OrderDTO>();

            foreach(var order in orders)
            {
                var orderModel = _mapper.Map<OrderDTO>(order);
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
                Console.WriteLine("Error, ", ex.Message);
            }
            
        }
    }
}
