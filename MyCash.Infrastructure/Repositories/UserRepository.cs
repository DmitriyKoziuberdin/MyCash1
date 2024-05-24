﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<User>> GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
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
    }
}
