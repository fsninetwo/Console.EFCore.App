using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Data.Models;

namespace EfCore.Services.IServices
{
    public interface IUserService
    {
        Task UpdateUserAsync(UserUpdateDTO updateUser);

        Task<UserDTO> GetUserAsync(long id);

        Task<UserCredentialsResult> GetUserByCredentialsAsync(string login, string password);
    }
}
