using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Data.IRepositories;
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
            var product = await _productRepository.GetProductAsync(id);

            return product;
        }

        public async Task<List<Product>> GetProductsAsync(string searchText)
        {
            var product = await _productRepository.GetProductsAsync(searchText);

            return product;
        }
    }
}
