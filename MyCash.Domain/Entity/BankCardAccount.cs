
namespace MyCash.Domain.Entity
{
    public class BankCardAccount : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CardId { get; set; }
        public BankCard BankCard { get; set; }
    }
}
