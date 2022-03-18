using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.Models
{
    public class OrderDetailsUpdateDTO
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

    }
}
