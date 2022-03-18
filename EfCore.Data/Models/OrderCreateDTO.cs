using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Entities.Entities;

namespace EfCore.Data.Models
{
    public class OrderCreateDTO
    {
        public long Id { get; set; }

        public string Payment { get; set; }

        public virtual List<OrderDetailsCreateDTO> OrderDetails { get; set; }
    }
}
