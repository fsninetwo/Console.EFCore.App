using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.Models
{
    public class UserCreateDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
