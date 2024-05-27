namespace MyCash.Domain.Entity
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryTransaction { get; set; } = string.Empty;
        public string CategoryTypeTransaction { get; set; } = string.Empty;

        public List<AccountTransaction>? AccountTransactions { get; set; }

        //Каждая Транзакция относится к одной Категории и к одному счету
    }
}
