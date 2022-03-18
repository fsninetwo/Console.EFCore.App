using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;
using EfCore.Entities.Entities;

namespace EfCore.Services.Helpers
{
    public static class RatingHelper
    {
        public static Rating ConvertRatingDTOtoRating(RatingCreateDTO rating, long userId)
        {
            var created = DateTime.Now;

            var newRating = new Rating
            {
                Rate = rating.Rate,
                Message = rating.Message,
                Created = created,
                Updated = created,
                UserId = userId,
                ProductId = rating.ProductId
            };

            return newRating;
        }

        public static Rating ConvertRatingDTOtoRating(RatingUpdateDTO rating)
        {
            var updated = DateTime.Now;

            var newRating = new Rating
            {
                Id = rating.Id,
                Rate = rating.Rate,
                Message = rating.Message,
                Updated = updated,
                ProductId = rating.ProductId
            };

            return newRating;
        }
    }
}
