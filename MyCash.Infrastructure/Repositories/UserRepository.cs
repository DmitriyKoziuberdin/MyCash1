using Microsoft.EntityFrameworkCore;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<UserGetAllResponse>> GetAllUsers()
        {

            return await _appDbContext.Users
                .Select(user => new UserGetAllResponse
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    NumberPhone = user.NumberPhone,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var getUserId = await _appDbContext.Users
                .Include(userAccount => userAccount.UserAccounts)
                .ThenInclude(account => account.Account)
                .FirstOrDefaultAsync(userId => userId.UserId == id);
            return getUserId;
            
        }

        public async Task CreateUser(User user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteUser(int id)
        {
            var deleteUser = await _appDbContext.Users
                .Where(userId => userId.UserId == id)
                .ExecuteDeleteAsync();
            await _appDbContext.SaveChangesAsync();
            return deleteUser;
        }

        public async Task UpdateUser(User user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyUserById(int id)
        {
            return await _appDbContext.Users.AnyAsync(userId => userId.UserId == id);
        }

        public async Task<bool> AnyUserWithEmail(string userEmail)
        {
            return await _appDbContext.Users.AnyAsync(email => email.UserEmail == userEmail);
        }

        public async Task AddOrder(int userId, int accountId)
        {
            _appDbContext.Set<UserAccount>().Add(new UserAccount
            {
                UserId = userId,
                AccountId = accountId
            });
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdIncludingAccountsAndTransactions(int id)
        {
           var userBudget = await _appDbContext.Users
                .Include(u => u.UserAccounts)
                    .ThenInclude(ua => ua.Account)
                        .ThenInclude(a => a.AccountTransactions)
                            .ThenInclude(at => at.Transaction)
                .FirstOrDefaultAsync(u => u.UserId == id);
            return userBudget;
        }
    }
}
