using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.IRepositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order newOrder);

        Task UpdateOrderAsync(Order updatedOrder);

        Task<Order> GetOrderAsync(long OrderId, bool asNoTracking = true);

        Task<List<Order>> GetOrdersAsync(long userId, bool asNoTracking = true);

        Task DeleteOrderAsync(long OrderId);
    }
}
