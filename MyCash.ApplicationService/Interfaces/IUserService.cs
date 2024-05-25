using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<UserResponse> GetUserById(int id);
        public Task CreateUser(UserRequest user);
        public Task<UserResponse> UpdateUser(int userId, UserRequest user);
        public Task DeleteUser(int id);
        public Task AddAccount(int userId, int accountId);
    }
}
