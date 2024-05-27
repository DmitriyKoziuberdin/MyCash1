using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserGetAllResponse>> GetAllUsers();
        public Task<UserResponse> GetUserById(int id);
        public Task CreateUser(UserRequest user);
        public Task<UserResponse> UpdateUser(int userId, UserRequest user);
        public Task DeleteUser(int id);
        public Task AddAccount(int userId, int accountId);


    }
}
