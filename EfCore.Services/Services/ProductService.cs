using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Data.IRepositories;
using EfCore.Services.IServices;
using EfCore.Data.Models;

namespace EfCore.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IUserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ProductDTO> GetProductAsync(long id)
        {

            var product = await _productRepository.GetProductAsync(id);

            var productModel = _mapper.Map<ProductDTO>(product);

            var user = await _userService.GetUserAsync(1);

            productModel.Ratings.ForEach(f => f.UserName = user.Login);

            return productModel;
        }

        public async Task<List<ProductDTO>> GetProductsAsync(List<long> orderdetailsIds)
        {
            var products = await _productRepository.GetProductsAsync(orderdetailsIds);

            var user = await _userService.GetUserAsync(1);

            var productModelList = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productModel = _mapper.Map<ProductDTO>(product);

                productModel.Ratings.ForEach(f => f.UserName = user.Login);

                productModelList.Add(productModel);
            }
            
            return productModelList;
        }

        public async Task<List<ProductDTO>> GetProductsAsync(string searchText)
        {
            var products = await _productRepository.GetProductsAsync(searchText);

            var user = await _userService.GetUserAsync(1);

            var productModelList = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productModel = _mapper.Map<ProductDTO>(product);

                productModel.Ratings.ForEach(f => f.UserName = user.Login);

                productModelList.Add(productModel);
            }
            
            return productModelList;
        }
    }
}
