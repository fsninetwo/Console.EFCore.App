using System;
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
        private readonly EfCoreDbContext _dbContext;
        private readonly DbSet<Order> _orderDbSet;

        public OrderRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _orderDbSet = dbContext.Set<Order>();
        }

        public async Task AddOrderAsync(Order order)
        {
            _orderDbSet.Add(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            var order = await GetOrderAsync(orderId);

            if (order is null)
            {
                throw new InternalException("Order is not found");
            }

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
                throw new InternalException("Order is not found");
            }

            _orderDbSet.Update(updatedOrder);

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
