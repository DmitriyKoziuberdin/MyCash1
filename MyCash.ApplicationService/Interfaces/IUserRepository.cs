using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserGetAllResponse>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<int> DeleteUser(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task<bool> AnyUserById(int id);
        Task<bool> AnyUserWithEmail(string userEmail);
        Task AddOrder(int userId, int accountId);
        Task<User> GetUserByIdIncludingAccountsAndTransactions(int id);
    }
}
