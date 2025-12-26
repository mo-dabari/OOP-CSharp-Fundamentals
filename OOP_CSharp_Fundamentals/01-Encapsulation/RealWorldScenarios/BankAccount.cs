public enum TransactionType
{
    Deposit,
    Withdrawal
}

public class Transaction
{
    private decimal _amount;
    public decimal Amount => _amount;
    public TransactionType Type { get; }
       
    public DateTime Date { get; }

    public Transaction(decimal amount, TransactionType type)
    {
        if (amount <= 0)
            throw new ArgumentException("Transaction amount must be positive");

        _amount = amount;
        Date = DateTime.Now;
        Type = type;
    }
}

public class BankAccount
{
    private decimal _balance;
    public decimal Balance => _balance;

    private string _accountNumber ;
    public string AccountNumber => _accountNumber;

    private List<Transaction> _transactions;
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        if(string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Account number cannot be null or whitespace");
        }
        _accountNumber = accountNumber;
        _transactions = new List<Transaction>();
        Deposit(initialBalance);
    }

    void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit must be positive");

        _balance += amount;
        _transactions.Add( new Transaction(amount, "Deposit"));
    }

    void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal must be positive");
        if (amount > _balance)
            throw new InvalidOperationException("Insufficient funds");

        _balance -= amount;
        _transactions.Add( new Transaction(amount, "Withdrawal"));
    }
}