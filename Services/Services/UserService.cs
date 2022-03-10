using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Repositories.IRepositories;
using EfCore.Services.IServices;

namespace EfCore.Services.Services
{
    public class UserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
