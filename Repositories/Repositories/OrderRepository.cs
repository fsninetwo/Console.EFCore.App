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
            var Order = await GetOrderAsync(newOrder.Id);

            if (!(Order is null))
            {
                return;
            }

            _OrderDbSet.Add(Order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(long OrderId)
        {
            var Order = await GetOrderAsync(OrderId);

            _OrderDbSet.Remove(Order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(long OrderId, bool asNoTracking = true)
        {
            var Order = await GetOrder(OrderId, asNoTracking).FirstOrDefaultAsync();

            return Order;
        }

        public async Task<List<Order>> GetOrdersAsync(long userId, bool asNoTracking = true)
        {
            var Order = await GetOrders(userId, asNoTracking).ToListAsync();

            return Order;
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            var Order = await GetOrderAsync(updatedOrder.Id);

            if (Order is null)
            {
                return;
            }

            _OrderDbSet.Update(Order);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Order> GetOrder(long orderId, bool asNoTracking = false)
        {
            var Order = _OrderDbSet
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return Order;
        }

        private IQueryable<Order> GetOrders(long userId, bool asNoTracking = false)
        {
            var Order = _OrderDbSet
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderDetails)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return Order;
        }
    }
}
