using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Entities.Entities
{
    public class OrderDetails
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
    }
}
