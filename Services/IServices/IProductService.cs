using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Services.IServices
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(long id);

        Task<List<Product>> GetProductsAsync(string searchText);
    }
}
