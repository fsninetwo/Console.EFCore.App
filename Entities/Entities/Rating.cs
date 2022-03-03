using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Rating
    {
        public long Id { get; set; }

        public int Rate { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
