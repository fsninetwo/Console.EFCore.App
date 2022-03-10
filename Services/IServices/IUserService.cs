using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Services.IServices
{
    public interface IUserService
    {
        Task<User> GetUserAsync(long id);

        Task<User> GetUserByCredentialsAsync(string login, string password);
    }
}
