using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Entities.Entities;

namespace EfCore.Data.Models
{
    public class OrderUpdateDTO
    {
        public long Id { get; set; }

        public string Payment { get; set; }

        public DateTime PurchaseDate { get; set; }

        public Guid Guid { get; set; }

        public bool IsCompleted { get; set; }

        public long UserId { get; set; }

        public virtual List<OrderDetailsUpdateDTO> OrderDetails { get; set; }
    }
}
