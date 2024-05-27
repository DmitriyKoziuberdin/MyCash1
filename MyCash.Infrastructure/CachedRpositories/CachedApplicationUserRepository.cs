using Microsoft.Extensions.Caching.Memory;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.ApplicationService.InterfacesLoginRegistration;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;
using MyCash.Infrastructure.RepositoryLoginRegistration;

namespace MyCash.Infrastructure.CachedRpositories
{
    public class CachedApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationUserRepository _applicationUserRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "applicationuser";

        public CachedApplicationUserRepository(ApplicationUserRepository applicationUserRepository, IMemoryCache memoryCache)
        {
            _applicationUserRepository = applicationUserRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<ApplicationUser>> GetAllUserAccount()
        {
            var applicationUser = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _applicationUserRepository.GetAllUserAccount());
            return applicationUser!.ToList();
        }

        public async Task<ServiceResponse.GeneralResponse> CreateUserAccount(UserDTO userDTO)
        {
            _memoryCache.Remove(_cacheKey);
            return await _applicationUserRepository.CreateUserAccount(userDTO);
        }

        public async Task<ServiceResponse.LoginResponse> LoginUserAccount(LoginDTO loginDTO)
        {
            _memoryCache.Remove(_cacheKey);
            return await _applicationUserRepository.LoginUserAccount(loginDTO);
        }
    }
}
