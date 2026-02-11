// ✅ الطريقة الصحيحة
namespace InappropriateIntimacy.BankAccount.Good
{
    public class BankAccount
    {
        private decimal _balance;
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public int AccountNumber { get; }
        public decimal Balance => _balance; // ✅ Read-only

        public BankAccount(int accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            _balance = initialBalance;
        }

        // ✅ BankAccount مسؤول عن حالته
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive");
            }

            if (_balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            _balance -= amount;
            _transactions.Add(new Transaction
            {
                Amount = -amount,
                Date = DateTime.Now,
                Type = "Withdrawal"
            });
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive");
            }

            _balance += amount;
            _transactions.Add(new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                Type = "Deposit"
            });
        }

        public IReadOnlyList<Transaction> GetTransactionHistory()
        {
            return _transactions.AsReadOnly();
        }
    }

    public class TransferService
    {
        public void Transfer(BankAccount from, BankAccount to, decimal amount)
        {
            // ✅ استخدام الـ public methods فقط
            from.Withdraw(amount);
            to.Deposit(amount);

            // BankAccount يسجل الـ transactions داخلياً
        }
    }
}
