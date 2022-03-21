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
        public string Payment { get; set; }

        public List<OrderDetailsCreateDTO> OrderDetails { get; set; }
    }
}
