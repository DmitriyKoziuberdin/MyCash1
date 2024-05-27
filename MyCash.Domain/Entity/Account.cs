namespace MyCash.Domain.Entity
{
    public class Account : BaseEntity
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public List<UserAccount>? UserAccounts { get; set; }
        public List<AccountTransaction>? AccountTransactions { get; set; }


        //Один Счет может иметь много Транзакций.
    }
}
