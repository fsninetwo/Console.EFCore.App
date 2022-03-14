using EfCore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Repositories.Models;

namespace EfCore.Services.IServices
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(long id);

        Task<UserCredentialsModel> GetUserByCredentialsAsync(string login, string password);
    }
}
