using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.Models
{
    public class ProductDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Developer { get; set; }

        public List<RatingDTO> Ratings { get; set; }
    }
}
