using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;

namespace EfCore.Data.IRepositories
{
    public interface IRatingRepository
    {
        Task AddRatingAsync(Rating newRate);

        Task UpdateRatingAsync(Rating updatedRate);

        Task<Rating> GetRatingAsync(long rateId, bool asNoTracking = true);

        Task<List<RatingDTO>> GetRatingsAsync(long productId, bool asNoTracking = true);

        Task DeleteRatingAsync(long rateId);
    }
}
