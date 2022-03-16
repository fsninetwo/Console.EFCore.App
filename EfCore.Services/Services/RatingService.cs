using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Data.IRepositories;
using EfCore.Services.IServices;
using EfCore.Data.Models;
using AutoMapper;

namespace EfCore.Services.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IUserService userService)
        {
            _ratingRepository = ratingRepository;
            _userService = userService;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _ratingRepository.AddRatingAsync(rating);
        }

        public async Task<Rating> GetRatingAsync(long ratingId)
        {
            var rating = await _ratingRepository.GetRatingAsync(ratingId);

            var ratingModel = _mapper.Map<RatingDTO>(rating);

            var user = await _userService.GetUserAsync(rating.UserId);

            ratingModel.UserName = user.Login;

            return rating;
        }

        public async Task<List<RatingDTO>> GetRatingsAsync(long productId)
        {
            var ratings = await _ratingRepository.GetRatingsAsync(productId);

            return ratings;
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            await _ratingRepository.UpdateRatingAsync(rating);
        }
    }
}
