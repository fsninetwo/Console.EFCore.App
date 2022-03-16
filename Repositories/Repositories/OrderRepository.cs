using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.IRepositories;
using EfCore.Entities.Entities;
using EfCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfCoreContext _dbContext;
        private readonly DbSet<Order> _OrderDbSet;

        public OrderRepository(EfCoreContext dbContext)
        {
            _dbContext = dbContext;
            _OrderDbSet = dbContext.Set<Order>();
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            var order = await GetOrderAsync(newOrder.Id);

            if (!(order is null))
            {
                throw InternalBufferOverflowException()
            }

            _OrderDbSet.Add(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            var order = await GetOrderAsync(orderId);

            _OrderDbSet.Remove(order);

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
                return;
            }

            _OrderDbSet.Update(order);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Order> GetOrder(long orderId, bool asNoTracking = false)
        {
            var order = _OrderDbSet
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return order;
        }

        private IQueryable<Order> GetOrders(long userId, bool asNoTracking = false)
        {
            var order = _OrderDbSet
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return order;
        }
    }
}
