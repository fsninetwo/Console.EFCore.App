using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore.Entities.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public UserGroup UserGroup { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
