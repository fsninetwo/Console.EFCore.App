using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Repositories.IRepositories;
using EfCore.Repositories.Models;
using EfCore.Services.IServices;

namespace EfCore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserAsync(long id)
        {
            var user = await _userRepository.GetUserAsync(id);

            var userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task<UserCredentialsModel> GetUserByCredentialsAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, password);

            var userModel = _mapper.Map<UserCredentialsModel>(user);

            return userModel;
        }
    }
}
