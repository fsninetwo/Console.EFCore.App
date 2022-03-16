using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;

namespace EfCore.Services.IServices
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order newOrder);

        Task UpdateOrderAsync(Order newOrder);

        Task<OrderDTO> GetOrderAsync(long id);

        Task<List<OrderDTO>> GetOrdersAsync(long userId);

        Task DeleteOrderAsync(long orderId);
    }
}
