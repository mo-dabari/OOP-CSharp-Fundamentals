using System;
using System.Collections.Generic;
using System.Linq;

namespace Encapsulation.RealWorldScenarios
{
    public enum TransactionType
    {
        Deposit, Withdrawal
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

        private string _accountNumber;
        public string AccountNumber => _accountNumber;

        private List<Transaction> _transactions;
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber, nameof(accountNumber));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialBalance, nameof(initialBalance));

            _accountNumber = accountNumber;
            _transactions = new List<Transaction>();
            Deposit(initialBalance);
        }

        void Deposit(decimal amount)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

            _balance += amount;
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));
        }

        void Withdraw(decimal amount)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

            if (amount > _balance)
                throw new InvalidOperationException("Insufficient funds");

            _balance -= amount;
            _transactions.Add(new Transaction(amount, TransactionType.Withdrawal));
        }
    }
}