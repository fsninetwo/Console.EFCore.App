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
    public class ProductRepository : IProductRepository
    {
        private readonly EfCoreDbContext _dbContext;
        private readonly DbSet<Product> _productDbSet;

        public ProductRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _productDbSet = dbContext.Set<Product>();
        }

        public async Task AddProduct(Product product)
        {
            _productDbSet.Add(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsAsync(List<long> orderDetailIds, bool asNoTracking = true)
        {
            var products = await GetProducts(orderDetailIds, asNoTracking).ToListAsync();

            return products;
        }

        public async Task DeleteProduct(long productId)
        {
            var product = await GetProductAsync(productId);

            if (product is null)
            {
                throw new InternalException("Product is not found");
            }

            _productDbSet.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(long productId, bool asNoTracking = true)
        {
            var products = await GetProduct(productId, asNoTracking).FirstOrDefaultAsync();

            return products;
        }

        public async Task<List<Product>> GetProductsAsync(string searchText, bool asNoTracking = true)
        {
            var product = await GetProducts(searchText, asNoTracking).ToListAsync();

            return product;
        }

        public async Task UpdateProduct(Product updatedProduct)
        {
            var product = await GetProductAsync(updatedProduct.Id);

            if (product is null)
            {
                throw new InternalException("Product is not found");
            }

            _productDbSet.Update(updatedProduct);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Product> GetProduct(long productId, bool asNoTracking = false)
        {
            var product = _productDbSet
                .Where(x => x.Id == productId)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return product;
        }

        private IQueryable<Product> GetProducts(string searchText, bool asNoTracking = false)
        {
            var products = _productDbSet
                .Where(x => x.Name.ToLower().Contains(searchText))
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return products;
        }

        private IQueryable<Product> GetProducts(List<long> orderDetailIds, bool asNoTracking = false)
        {
            var products = _productDbSet
                .Where(x => orderDetailIds.Contains(x.Id))
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return products;
        }
    }
}
