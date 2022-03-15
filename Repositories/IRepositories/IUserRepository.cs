using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Data.IRepositories
{
    public interface IUserRepository
    {
        Task AddUser(User newUser);

        Task UpdateUser(User updatedUser);

        Task<User> GetUserAsync(long userId, bool asNoTracking = true);

        Task<User> GetUserByCredentialsAsync(string login, string password, bool asNoTracking = true);

        Task DeleteUser(long userId);
    }
}
