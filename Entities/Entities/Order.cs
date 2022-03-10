using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Entities.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Payment { get; set; }

        public string Guid { get; set; }

        public decimal Price { get; set; }

        public long Currency { get; set; }

        public bool IsCompleted { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
