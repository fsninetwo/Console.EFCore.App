using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Repositories.IRepositories;
using EfCore.Services.IServices;

namespace EfCore.Services.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductAsync(long id)
        {
            var Product = await _productRepository.GetProductAsync(id);

            return Product;
        }

        public async Task<List<Product>> GetProductsAsync(string searchText)
        {
            var Product = await _productRepository.GetProductsAsync(searchText);

            return Product;
        }
    }
}
