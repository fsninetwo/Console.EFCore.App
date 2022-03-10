using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task UpdateUser(long id, User user);

        Task<User> GetUserAsync(long id, bool asNoTracking = false);

        Task<User> GetUserByCredentialsAsync(string login, string password, bool asNoTracking = true);

        Task DeleteUser(long id);
    }
}
