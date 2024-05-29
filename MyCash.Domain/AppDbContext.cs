using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCash.Domain.Entity;

namespace MyCash.Domain
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankCard> BankCards { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(userId => userId.UserId);

            modelBuilder.Entity<Account>()
                .HasKey(accountId => accountId.AccountId);

            modelBuilder.Entity<Transaction>()
                .HasKey(transaction => transaction.TransactionId);

            modelBuilder.Entity<BankCard>()
                .HasKey(bankCard => bankCard.CardId);

            modelBuilder.Entity<User>()
               .HasMany(userAccount => userAccount.UserAccounts)
               .WithOne(user => user.User)
               .HasForeignKey(userId => userId.UserId);

            modelBuilder.Entity<Account>()
                .HasMany(userAccount => userAccount.UserAccounts)
                .WithOne(account => account.Account)
                .HasForeignKey(accountId => accountId.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(accountTransaction => accountTransaction.AccountTransactions)
                .WithOne(account => account.Account)
                .HasForeignKey(accountId => accountId.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(bankCardAccount => bankCardAccount.BankCardAccounts)
                .WithOne(account => account.Account)
                .HasForeignKey(accountId => accountId.AccountId);

            modelBuilder.Entity<Transaction>()
                .HasMany(accountTransaction => accountTransaction.AccountTransactions)
                .WithOne(transaction => transaction.Transaction)
                .HasForeignKey(transactionId => transactionId.TransactionId);

            modelBuilder.Entity<BankCard>()
                .HasMany(bankCardAccount => bankCardAccount.BankCardAccounts)
                .WithOne(bankCard => bankCard.BankCard)
                .HasForeignKey(cardId => cardId.CardId);

            modelBuilder.Entity<UserAccount>()
                .HasKey(x => new
                {
                    x.UserId,
                    x.AccountId
                });

            modelBuilder.Entity<AccountTransaction>()
                .HasKey(x => new
                {
                    x.AccountId,
                    x.TransactionId
                });

            modelBuilder.Entity<BankCardAccount>()
               .HasKey(x => new
               {
                   x.AccountId,
                   x.CardId
               });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateStatistics();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        private void UpdateStatistics()
        {
            var update = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in update)
            {
                if (entityEntry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedAt = DateTime.UtcNow;
                }
            }

            var create = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in create)
            {
                if (entityEntry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}

