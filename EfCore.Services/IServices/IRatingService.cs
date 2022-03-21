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
        Task AddRatingAsync(RatingCreateDTO rating);

        Task UpdateRatingAsync(RatingUpdateDTO rating);

        Task<RatingDTO> GetRatingAsync(long ratingId);

        Task<List<RatingDTO>> GetRatingsAsync(long productId);

        Task DeleteRatingAsync(long ratingId);
    }
}
