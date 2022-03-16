﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.IRepositories;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfCoreContext _dbContext;
        private readonly DbSet<Order> _orderDbSet;

        public OrderRepository(EfCoreContext dbContext)
        {
            _dbContext = dbContext;
            _orderDbSet = dbContext.Set<Order>();
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            var order = await GetOrderAsync(newOrder.Id);

            if (!(order is null))
            {
                throw new InternalException("Order is already exist");
            }

            _orderDbSet.Add(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            var order = await GetOrderAsync(orderId);

            _orderDbSet.Remove(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(long orderId, bool asNoTracking = true)
        {
            var order = await GetOrder(orderId, asNoTracking).FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<Order>> GetOrdersAsync(long userId, bool asNoTracking = true)
        {
            var order = await GetOrders(userId, asNoTracking).ToListAsync();

            return order;
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            var order = await GetOrderAsync(updatedOrder.Id);

            if (order is null)
            {
                throw new InternalException("Order doesn't exist");
            }

            _orderDbSet.Update(order);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Order> GetOrder(long orderId, bool asNoTracking = false)
        {
            var order = _orderDbSet
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return order;
        }

        private IQueryable<Order> GetOrders(long userId, bool asNoTracking = false)
        {
            var order = _orderDbSet
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return order;
        }
    }
}
