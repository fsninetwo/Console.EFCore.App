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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace EfCore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            var user = await _userRepository.GetUserAsync(id);

            var userModel = _mapper.Map<UserDTO>(user);
            _logger.LogDebug("User {0} is selected", userModel.Login);

            return userModel;
        }

        public async Task<UserCredentialsResult> GetUserByCredentialsAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, password);

            var userModel = _mapper.Map<UserCredentialsResult>(user);

            _logger.LogDebug("User {0} credentials is selected", userModel.Login);

            return userModel;
        }

        public async Task UpdateUserAsync(UserUpdateDTO updateUser)
        {
            try
            {
                var mapUser = UserHelper.ConvertUserDTOtoUser(updateUser);

                await _userRepository.UpdateUser(mapUser);

                _logger.LogDebug("New user is added");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error," + ex.Message);

                Console.WriteLine("Error, " + ex.Message);
            }
        }
    }
}
