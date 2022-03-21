using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.IRepositories;
using EfCore.Data.Models;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly EfCoreDbContext _dbContext;
        private readonly DbSet<Rating> _ratingDbSet;

        public RatingRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _ratingDbSet = dbContext.Set<Rating>();
        }

        public async Task AddRatingAsync(Rating rating)
        {
            _ratingDbSet.Add(rating);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(long rateId)
        {
            var rating = await GetRatingAsync(rateId);

            if (rating is null)
            {
                throw new InternalException("Rating is not found");
            }

            _ratingDbSet.Remove(rating);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Rating> GetRatingAsync(long rateId, bool asNoTracking = true)
        {
            var rating = await GetRating(rateId, asNoTracking).FirstOrDefaultAsync();

            return rating;
        }

        public async Task<List<RatingDTO>> GetRatingsAsync(long productId, bool asNoTracking = true)
        {
            var ratings = await (from rating in _ratingDbSet
                                 join product in _dbContext.Set<Product>() on rating.ProductId equals product.Id
                                 join users in _dbContext.Set<User>() on rating.UserId equals users.Id into Users
                                 from user in Users.DefaultIfEmpty()
                                 where product.Id == productId
                                 select new RatingDTO 
                                 {
                                     Id = rating.Id,
                                     Message = rating.Message,
                                     Rate = rating.Rate,
                                     Created = rating.Created,
                                     Updated = rating.Updated,
                                     UserName = user == null ? "Deleted" : user.Login
                                 }).ToListAsync();

            return ratings;

        }

        public async Task UpdateRatingAsync(Rating updatedRate)
        {
            var rating = await GetRatingAsync(updatedRate.Id);

            if (rating is null)
            {
                throw new InternalException("Rating is not found");
            }

            _ratingDbSet.Update(updatedRate);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<Rating> GetRating(long ratingId, bool asNoTracking = false)
        {
            var rating = _ratingDbSet
                .Where(x => x.Id == ratingId)
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll);   

            return rating;
        }
    }
}