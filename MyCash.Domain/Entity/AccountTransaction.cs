namespace MyCash.Domain.Entity
{
    public class AccountTransaction : BaseEntity
    {
        public int AccountId { get; set; }
        public Account? Account { get; set; }

        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
