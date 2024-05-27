using Microsoft.Extensions.Caching.Memory;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;

namespace MyCash.Infrastructure.CachedRpositories
{
    public class CachedUserRepository : IUserRepository
    {
        private readonly UserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "user";

        public CachedUserRepository(UserRepository userRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<UserGetAllResponse>> GetAllUsers()
        {
            var user = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _userRepository.GetAllUsers());
            return user!.ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<int> DeleteUser(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _userRepository.DeleteUser(id);
        }

        public async Task CreateUser(User user)
        {
            _memoryCache.Remove(_cacheKey);
            await _userRepository.CreateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            _memoryCache.Remove(_cacheKey);
            await _userRepository.UpdateUser(user);
        }

        public async Task<bool> AnyUserById(int id)
        {
            return await _userRepository.AnyUserById(id);
        }

        public async Task<bool> AnyUserWithEmail(string userEmail)
        {
            return await _userRepository.AnyUserWithEmail(userEmail);
        }

        public async Task AddOrder(int userId, int accountId)
        {
            _memoryCache.Remove(_cacheKey);
            await _userRepository.AddOrder(userId, accountId);
        }

        public async Task<User> GetUserByIdIncludingAccountsAndTransactions(int id)
        {
            return await _userRepository.GetUserByIdIncludingAccountsAndTransactions(id);
        }
    }
}
