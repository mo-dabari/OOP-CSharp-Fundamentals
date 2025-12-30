
/*
 * Exercises.cs
 * ════════════════════════════════════════════════════════════
 * تمارين عملية لمفهوم الكبسولة (Encapsulation)
 *
 * التمارين تغطي:
 * - حماية البيانات الحساسة
 * - Validation و Data Integrity
 * - Properties و Backing Fields
 * - حل مشاكل حقيقية
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Encapsulation.Exercises
{
    public class Customer
    {
        // Private fields - البيانات الحساسة
        private int id;
        private string name;
        private string email;
        private DateTime registrationDate;

        // Constructor
        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            registrationDate = DateTime.Now;
        }

        // Properties مع Validation
        public int Id
        {
            get { return id; }
            private set  // لا يمكن التعديل من الخارج
            {
                if (value > 0)
                    id = value;
                else
                    throw new ArgumentException("رقم العميل يجب أن يكون موجب");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 3)
                    name = value;
                else
                    throw new ArgumentException("الاسم يجب أن يكون 3 أحرف على الأقل");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
                    email = value;
                else
                    throw new ArgumentException("البريد الإلكتروني غير صحيح");
            }
        }

        public DateTime RegistrationDate => registrationDate;  // Read-only

        public override string ToString()
        {
            return $"العميل: {Name} (ID: {Id})";
        }
    }

    public class BankAccount
    {
        // Private fields
        private string accountNumber;
        private decimal balance;
        private List<string> transactions;

        public BankAccount(string number, decimal initialBalance)
        {
            AccountNumber = number;
            Balance = initialBalance;
            transactions = new List<string>();
            transactions.Add($"فتح الحساب: {initialBalance:C}");
        }

        // Properties
        public string AccountNumber
        {
            get { return accountNumber; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 10)
                    accountNumber = value;
                else
                    throw new ArgumentException("رقم الحساب غير صحيح");
            }
        }

        public decimal Balance
        {
            get { return balance; }
            private set
            {
                if (value >= 0)
                    balance = value;
                else
                    throw new ArgumentException("الرصيد لا يمكن أن يكون سالب");
            }
        }

        // Methods
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("❌ المبلغ يجب أن يكون موجب");
                return false;
            }

            balance += amount;
            transactions.Add($"إيداع: {amount:C} | الرصيد: {balance:C}");
            Console.WriteLine($"✅ تم إيداع {amount:C}");
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("❌ المبلغ يجب أن يكون موجب");
                return false;
            }

            if (amount > balance)
            {
                Console.WriteLine($"❌ رصيد غير كافي (الرصيد: {balance:C})");
                return false;
            }

            balance -= amount;
            transactions.Add($"سحب: {amount:C} | الرصيد: {balance:C}");
            Console.WriteLine($"✅ تم سحب {amount:C}");
            return true;
        }

        public void PrintBalance()
        {
            Console.WriteLine($"💰 الرصيد: {balance:C}");
        }

        public void PrintTransactions()
        {
            Console.WriteLine($"\n📋 سجل المعاملات ({accountNumber}):");
            foreach (var trans in transactions)
            {
                Console.WriteLine($"  • {trans}");
            }
        }
    }
}
