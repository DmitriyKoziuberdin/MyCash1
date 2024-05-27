using Microsoft.AspNetCore.Identity;

namespace MyCash.Domain.Entity
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<UserAccount>? UserAccounts { get; set; }


        //Один Пользователь может иметь много Счетов.
    }
}
