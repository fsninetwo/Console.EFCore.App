using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;

namespace EfCore.Services.IServices
{
    public interface IRatingService
    {
        Task AddRatingAsync(Rating rating);

        Task UpdateRatingAsync(Rating rating);

        Task<Rating> GetRatingAsync(long ratingId);

        Task<List<RatingDTO>> GetRatingsAsync(long productId);
    }
}
