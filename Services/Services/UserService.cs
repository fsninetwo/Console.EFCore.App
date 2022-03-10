using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Entities.Entities;
using EfCore.Repositories.IRepositories;
using EfCore.Services.IServices;

namespace EfCore.Services.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(long id)
        {
            var user = await _userRepository.GetUserAsync(id);

            return user;
        }

        public async Task<User> GetUserByCredentialsAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, password);

            return user;
        }
    }
}
