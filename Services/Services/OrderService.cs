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

namespace EfCore.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository OrderRepository, IUserService userService)
        {
            _OrderRepository = OrderRepository;
            _userService = userService;
        }

        public async Task AddOrderAsync(Order Order)
        {
            await _OrderRepository.AddOrderAsync(Order);
        }

        public async Task<OrderDTO> GetOrderAsync(long orderId)
        {
            var order = await _OrderRepository.GetOrderAsync(orderId);

            var orderModel = _mapper.Map<OrderDTO>(order);

            return orderModel;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync(long productId)
        {
            var orders = await _OrderRepository.GetOrdersAsync(productId);

            var orderList = new List<OrderDTO>();

            foreach(var order in orders)
            {
                var orderModel = _mapper.Map<OrderDTO>(order);
                orderList.Add(orderModel);
            }

            return orderList;
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            await _OrderRepository.UpdateOrderAsync(updatedOrder);
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            await _OrderRepository.DeleteOrderAsync(orderId);
        }
    }
}
