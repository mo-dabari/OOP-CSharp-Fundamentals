// ❌ الطريقة الخاطئة
namespace Bad
{
    public class BankAccount
    {
        public decimal Balance { get; set; } // ❌ Public setter
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public class TransferService
    {
        public void Transfer(BankAccount from, BankAccount to, decimal amount)
        {
            // ❌ TransferService يتعامل مع البيانات الداخلية مباشرة
            from.Balance -= amount;
            to.Balance += amount;

            from.Transactions.Add(new Transaction { Amount = -amount });
            to.Transactions.Add(new Transaction { Amount = amount });
        }
    }
}
