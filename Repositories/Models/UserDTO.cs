using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.Models
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }
    }
}
