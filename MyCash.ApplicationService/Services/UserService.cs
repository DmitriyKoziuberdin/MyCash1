﻿using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var isExist = await _userRepository.AnyUserById(id);
            if (!isExist)
            {
                throw new InvalidOperationException("Id not found");
            }

            var userId = await _userRepository.GetUserById(id);
            var userResponse = new UserResponse
            {
                UserName = userId.UserName,
                UserEmail = userId.UserEmail,
                NumberPhone = userId.NumberPhone,
                UserAccounts = userId.UserAccounts.Select(ua => new UserAccountResponse
                {
                    Id = ua.AccountId,
                    AccountName = ua.Account.AccountName,
                    Balance = ua.Account.Balance
                }).ToList()
            };

            return userResponse;
        }

        public async Task CreateUser(UserRequest user)
        {
            var isExist = await _userRepository.AnyUserWithEmail(user.UserEmail);
            if (isExist)
            {
                throw new InvalidOperationException("This email already use");
            }

            await _userRepository.CreateUser(new User
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                NumberPhone = user.NumberPhone
            });
        }

        public async Task<UserResponse> UpdateUser(int userId, UserRequest user)
        {
            var isExist = await _userRepository.AnyUserById(userId);
            if (!isExist)
            {
                throw new InvalidOperationException("Id not found");
            }

            var isExistEmail = await _userRepository.AnyUserWithEmail(user.UserEmail);
            if (isExistEmail)
            {
                throw new InvalidOperationException("This email already use");
            }

            var newUser = new User
            {
                UserId = userId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
            };

            await _userRepository.UpdateUser(newUser);
            User userResponse = await _userRepository.GetUserById(newUser.UserId);
            return new UserResponse
            {
                UserName = userResponse.UserName,
                UserEmail = newUser.UserEmail,
                NumberPhone = userResponse.NumberPhone,
                PasswordHash = userResponse.PasswordHash,
            };
        }

        public async Task DeleteUser(int id)
        {
            var isExist = await _userRepository.AnyUserById(id);
            if (!isExist)
            {
                throw new InvalidOperationException("Id not found");
            }
            await _userRepository.DeleteUser(id);
        }
    }
}
