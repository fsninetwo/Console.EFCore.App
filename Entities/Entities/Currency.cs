using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Entities.Entities
{
    public class Currency
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
