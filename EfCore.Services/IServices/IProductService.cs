using EfCore.Data.Models;
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
        Task<ProductDTO> GetProductAsync(long id);

        Task<List<ProductDTO>> GetProductsAsync(string searchText);

        Task<List<ProductDTO>> GetProductsAsync(List<long> orderDetailsIds);
    }
}
