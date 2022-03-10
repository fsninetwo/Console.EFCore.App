using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Entities.Entities
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Developer { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public virtual List<OrderDetails> OrderDetailses { get; set; }
    }
}
