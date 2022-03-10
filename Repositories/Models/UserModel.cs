using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Repositories.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }
    }
}
