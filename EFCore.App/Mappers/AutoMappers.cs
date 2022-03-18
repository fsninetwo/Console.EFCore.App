using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EfCore.Data.Models;
using EfCore.Entities.Entities;

namespace EFCore.App.Mappers
{
    public class AutoMappers: Profile
    {
        public AutoMappers()
        {
            CreateMap<Order, OrderDTO>().ForPath(dest => dest.OrderDetails, 
                opt => opt.MapFrom(
                    src => src.OrderDetails
                        .Select(x => new OrderDetailsDTO {Id = x.Id})
                    ));

            CreateMap<OrderDetails, OrderDetailsDTO>();
            CreateMap<Rating, RatingDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserCredentialsResult>();
            CreateMap<Product, ProductDTO>().ForPath(dest => dest.Ratings, 
                opt => opt.MapFrom(
                    src => src.Ratings
                        .Select(x => new RatingDTO
                        { 
                            Id = x.Id,
                            Rate = x.Rate,
                            Message = x.Message,
                            Created = x.Created,
                            Updated = x.Updated
                        })
                    ));
        }
    }
}
