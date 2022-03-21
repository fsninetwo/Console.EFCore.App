using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;
using EfCore.Entities.Entities;

namespace EfCore.Services.Helpers
{
    class UserHelper
    {
        public static User ConvertUserDTOtoUser(UserUpdateDTO user, UserGroup userGroup = UserGroup.User)
        {
            var newUser = new User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Created = DateTime.Now,
                UserGroup = userGroup,
            };

            return newUser;
        }
    }
}
