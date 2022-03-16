using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EfCore.Domain.Exceptions;
using EfCore.Entities.Entities;
using EfCore.Data.IRepositories;
using EfCore.Data.Models;
using EfCore.Services.Helpers;
using EfCore.Services.IServices;

namespace EfCore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task AddUserAsync(UserCreateDTO newUser)
        {
            var mapUser = UserHelper.MapUserDTOtoUser(newUser);

            _userRepository.AddUser(mapUser);
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            var user = await _userRepository.GetUserAsync(id);

            var userModel = _mapper.Map<UserDTO>(user);

            return userModel;
        }

        public async Task<UserCredentialsResult> GetUserByCredentialsAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, password);

            var userModel = _mapper.Map<UserCredentialsResult>(user);

            return userModel;
        }
    }
}
