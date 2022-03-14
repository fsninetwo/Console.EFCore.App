using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Repositories.Models
{
    public class UserCredentialsModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
