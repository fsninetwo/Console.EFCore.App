using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.IRepositories
{
    public interface IProductRepository
    {
        Task AddProduct(Product newProduct);

        Task UpdateProduct(Product updatedProduct);

        Task<Product> GetProductAsync(long productId, bool asNoTracking = true);

        Task<List<Product>> GetProductsAsync(string searchText, bool asNoTracking = true);

        Task<List<Product>> GetProductsAsync(List<long> orderDetailIds, bool asNoTracking = true);

        Task DeleteProduct(long productId);
    }
}
