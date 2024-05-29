namespace MyCash.ApplicationService.DTO.Response
{
    public class UserResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<UserAccountResponse>? UserAccounts { get; set; }
    }
}
