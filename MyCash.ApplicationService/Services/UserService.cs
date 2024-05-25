using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UserService(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<UserGetAllResponse>> GetAllUsers()
        {

            return await _userRepository.GetAllUsers();
            //var usersResponse = new UserGetAllResponse
            //{
            //    UserName = usersResponse.UserName,
            //}
            //return await _userRepository.GetAllUsers();
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var isExist = await _userRepository.AnyUserById(id);
            if (!isExist)
            {
                throw new InvalidOperationException("Id not found");
            }

            var user = await _userRepository.GetUserByIdIncludingAccountsAndTransactions(id);

            var userResponse = new UserResponse
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                NumberPhone = user.NumberPhone,
                UserAccounts = user.UserAccounts.Select(ua =>
                {
                    var balance = ua.Account.AccountTransactions.Sum(at => at.Transaction.Amount);
                    return new UserAccountResponse
                    {
                        Id = ua.AccountId,
                        AccountName = ua.Account.AccountName,
                        Balance = balance
                    };
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

        public async Task AddAccount(int userId, int accountId)
        {
            var isExistForUserId = await _userRepository.AnyUserById(userId);
            if (!isExistForUserId)
            {
                throw new InvalidOperationException("UserId not found");
            }

            var isExistForAccountId = await _accountRepository.AnyAccountById(accountId);
            if (!isExistForAccountId)
            {
                throw new InvalidOperationException("AccountId not found");
            }

            await _userRepository.AddOrder(userId, accountId);
            
        }
    }
}
