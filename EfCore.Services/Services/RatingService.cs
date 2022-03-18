﻿using System;
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
using EfCore.Services.Helpers;

namespace EfCore.Services.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IUserService userService, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task AddRatingAsync(RatingCreateDTO rating)
        {
            var newRating = RatingHelper.ConvertRatingDTOtoRating(rating);

            try
            {
                await _ratingRepository.AddRatingAsync(newRating);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("Error, ", ex.Message);
            }       
        }

        public async Task<RatingDTO> GetRatingAsync(long ratingId)
        {
            var rating = await _ratingRepository.GetRatingAsync(ratingId);

            var ratingModel = _mapper.Map<RatingDTO>(rating);

            var user = await _userService.GetUserAsync(rating.UserId);

            ratingModel.UserName = user.Login;

            return ratingModel;
        }

        public async Task<List<RatingDTO>> GetRatingsAsync(long productId)
        {
            var ratings = await _ratingRepository.GetRatingsAsync(productId);

            return ratings;
        }

        public async Task UpdateRatingAsync(RatingUpdateDTO rating)
        {
            var updateRating = RatingHelper.ConvertRatingDTOtoRating(rating);

            try
            {
                await _ratingRepository.UpdateRatingAsync(updateRating);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("Error, ", ex.Message);
            }
        }

        public async Task DeleteRatingsAsync(long ratingId)
        {
            try
            {
                await _ratingRepository.DeleteRatingAsync(ratingId);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("Error, ", ex.Message);
            }
        }
    }
}
