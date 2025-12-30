namespace Polymorphism.RealWorldScenarios
{
    /*
 * BankingSystem.cs
 * ════════════════════════════════════════════════════════════
 * حالة واقعية متقدمة: نظام بنكي متكامل
 *
 * السيناريو:
 * ────────
 * بنك يدير عدة أنواع حسابات:
 * - حساب جاري (Checking): بدون فائدة
 * - حساب توفير (Savings): فائدة شهرية
 * - حساب استثماري (Investment): فائدة عالية
 *
 * هذا يوضح الـ Polymorphism في نظام حقيقي معقد
 */

    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace OOP_CSharp_Fundamentals
    {
        // ════════════════════════════════════════════════════════════
        // نماذج البيانات
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// معاملة بنكية
        /// </summary>
        public class Transaction
        {
            public DateTime Date { get; set; }
            public string Type { get; set; }  // Deposit, Withdrawal, Interest
            public decimal Amount { get; set; }

            public Transaction(string type, decimal amount)
            {
                Date = DateTime.Now;
                Type = type;
                Amount = amount;
            }
        }

        // ════════════════════════════════════════════════════════════
        // الحساب البنكي - الأب
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// الحساب الأساسي
        /// </summary>
        public abstract class BankAccount
        {
            public string accountNumber { get; }
            protected string accountHolder;
            protected decimal balance;
            protected List<Transaction> transactions;
            protected decimal interestRate;
            protected decimal monthlyFee;

            public BankAccount(string number, string holder, decimal initialBalance)
            {
                accountNumber = number;
                accountHolder = holder;
                balance = initialBalance;
                transactions = new List<Transaction>();
                interestRate = 0;
                monthlyFee = 0;
            }

            // الدوال الأساسية
            public virtual bool Deposit(decimal amount)
            {
                if (amount <= 0) return false;
                balance += amount;
                transactions.Add(new Transaction("إيداع", amount));
                Console.WriteLine($"✅ تم إيداع {amount:C}");
                return true;
            }

            public virtual bool Withdraw(decimal amount)
            {
                if (amount <= 0 || amount > balance) return false;
                balance -= amount;
                transactions.Add(new Transaction("سحب", amount));
                Console.WriteLine($"✅ تم سحب {amount:C}");
                return true;
            }

            public virtual decimal CalculateMonthlyInterest()
            {
                return 0;  // لا فائدة بشكل افتراضي
            }

            public virtual decimal CalculateMonthlyFee()
            {
                return monthlyFee;
            }

            public virtual void ApplyMonthlyOperations()
            {
                // تطبيق الفائدة
                decimal interest = CalculateMonthlyInterest();
                if (interest > 0)
                {
                    balance += interest;
                    transactions.Add(new Transaction("فائدة", interest));
                }

                // خصم الرسوم
                decimal fee = CalculateMonthlyFee();
                if (fee > 0)
                {
                    balance -= fee;
                    transactions.Add(new Transaction("رسوم", fee));
                }
            }

            public virtual string GetAccountType() => "حساب عادي";

            public decimal GetBalance() => balance;

            public void PrintBalance()
            {
                Console.WriteLine($"💰 الرصيد الحالي: {balance:C}");
            }

            public void PrintAccountInfo()
            {
                Console.WriteLine($"الرقم: {accountNumber} | الاسم: {accountHolder}");
                Console.WriteLine($"النوع: {GetAccountType()} | الرصيد: {balance:C}");
            }

            public void PrintTransactionHistory()
            {
                Console.WriteLine($"\n📋 سجل المعاملات ({accountNumber}):");
                foreach (var trans in transactions.TakeLast(5))
                {
                    Console.WriteLine($"  {trans.Date:yyyy-MM-dd HH:mm} | {trans.Type}: {trans.Amount:C}");
                }
                if (transactions.Count > 5)
                    Console.WriteLine($"  ... و {transactions.Count - 5} معاملات أخرى");
            }
        }

        /// <summary>
        /// حساب جاري - بدون فائدة
        /// </summary>
        public class CheckingAccount : BankAccount
        {
            private decimal overdraftLimit;

            public CheckingAccount(string number, string holder, decimal initialBalance)
                : base(number, holder, initialBalance)
            {
                overdraftLimit = 5000;  // سماح بتجاوز
                monthlyFee = 50;
            }

            public override bool Withdraw(decimal amount)
            {
                // يمكن السحب أكثر من الرصيد
                if (amount <= 0 || amount > balance + overdraftLimit) return false;
                balance -= amount;
                transactions.Add(new Transaction("سحب", amount));

                if (balance < 0)
                    Console.WriteLine($"⚠️  حسابك بسالب {Math.Abs(balance):C}");
                else
                    Console.WriteLine($"✅ تم سحب {amount:C}");

                return true;
            }

            public override decimal CalculateMonthlyFee()
            {
                return monthlyFee;
            }

            public override string GetAccountType() => "حساب جاري";
        }

        /// <summary>
        /// حساب توفير - فائدة شهرية
        /// </summary>
        public class SavingsAccount : BankAccount
        {
            private decimal minimumBalance;

            public SavingsAccount(string number, string holder, decimal initialBalance)
                : base(number, holder, initialBalance)
            {
                interestRate = 0.02m;  // 2% سنوياً = 0.167% شهرياً
                minimumBalance = 1000;
            }

            public override bool Withdraw(decimal amount)
            {
                // لا يمكن السحب أقل من الحد الأدنى
                if (balance - amount < minimumBalance)
                {
                    Console.WriteLine($"❌ لا يمكن السحب (الحد الأدنى: {minimumBalance:C})");
                    return false;
                }
                return base.Withdraw(amount);
            }

            public override decimal CalculateMonthlyInterest()
            {
                return balance * (interestRate / 12);
            }

            public override string GetAccountType() => "حساب توفير";
        }

        /// <summary>
        /// حساب استثماري - فائدة عالية
        /// </summary>
        public class InvestmentAccount : BankAccount
        {
            private int investmentMonths;
            private decimal minInvestment;

            public InvestmentAccount(string number, string holder, decimal initialBalance)
                : base(number, holder, initialBalance)
            {
                interestRate = 0.08m;  // 8% سنوياً
                investmentMonths = 12;
                minInvestment = 10000;
                monthlyFee = 100;
            }

            public override bool Deposit(decimal amount)
            {
                if (amount < minInvestment)
                {
                    Console.WriteLine($"❌ الحد الأدنى للاستثمار: {minInvestment:C}");
                    return false;
                }
                return base.Deposit(amount);
            }

            public override bool Withdraw(decimal amount)
            {
                Console.WriteLine("⚠️  تحذير: الانسحاب من حساب الاستثمار قد يكون له تأثير");
                return base.Withdraw(amount);
            }

            public override decimal CalculateMonthlyInterest()
            {
                // فائدة مركبة
                return balance * (interestRate / 12);
            }

            public override decimal CalculateMonthlyFee()
            {
                return monthlyFee;
            }

            public override string GetAccountType() => "حساب استثماري";
        }

        // ════════════════════════════════════════════════════════════
        // النظام البنكي
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// نظام البنك
        /// </summary>
        public class BankSystem
        {
            private List<BankAccount> accounts = new();

            public void AddAccount(BankAccount account)
            {
                accounts.Add(account);
                Console.WriteLine($"✅ تم فتح حساب جديد: {account.GetAccountType()}");
            }

            public BankAccount FindAccount(string number)
            {
                return accounts.FirstOrDefault(a => a.accountNumber == number);
            }

            // ─────────────────────────────────────────
            // العمليات البنكية
            // ─────────────────────────────────────────

            public void PrintAllAccounts()
            {
                Console.WriteLine("\n📋 جميع الحسابات:");
                Console.WriteLine("════════════════════════════════");
                foreach (var account in accounts)
                {
                    account.PrintAccountInfo();
                    account.PrintBalance();
                    Console.WriteLine();
                }
            }

            public void PrintAccountDetails(string number)
            {
                var account = FindAccount(number);
                if (account != null)
                {
                    Console.WriteLine($"\n📊 تفاصيل الحساب:");
                    Console.WriteLine("════════════════════════════════");
                    account.PrintAccountInfo();
                    account.PrintBalance();
                    account.PrintTransactionHistory();
                }
                else
                {
                    Console.WriteLine("❌ الحساب غير موجود");
                }
            }

            public void ApplyMonthlyOperations()
            {
                Console.WriteLine("\n⏰ تطبيق العمليات الشهرية:");
                Console.WriteLine("════════════════════════════════");
                foreach (var account in accounts)
                {
                    Console.WriteLine($"\n🔄 معالجة {account.GetAccountType()} ({account.accountNumber}):");

                    decimal interestBefore = account.CalculateMonthlyInterest();
                    decimal feeBefore = account.CalculateMonthlyFee();

                    account.ApplyMonthlyOperations();

                    if (interestBefore > 0)
                        Console.WriteLine($"  💰 فائدة: {interestBefore:C}");
                    if (feeBefore > 0)
                        Console.WriteLine($"  💸 رسوم: {feeBefore:C}");

                    account.PrintBalance();
                }
            }

            // ─────────────────────────────────────────
            // التقارير
            // ─────────────────────────────────────────

            public void PrintBankReport()
            {
                Console.WriteLine("\n📈 تقرير البنك:");
                Console.WriteLine("════════════════════════════════");

                decimal totalByType = 0;

                var grouped = accounts.GroupBy(a => a.GetAccountType());

                foreach (var group in grouped)
                {
                    Console.WriteLine($"\n{group.Key}:");
                    decimal subtotal = 0;
                    foreach (var account in group)
                    {
                        subtotal += account.GetBalance();
                    }
                    Console.WriteLine($"  عدد الحسابات: {group.Count()}");
                    Console.WriteLine($"  الإجمالي: {subtotal:C}");
                    totalByType += subtotal;
                }

                Console.WriteLine($"\n💰 إجمالي الأموال في البنك: {totalByType:C}");
                Console.WriteLine($"👥 عدد الحسابات: {accounts.Count}");
                Console.WriteLine($"📊 متوسط الرصيد: {(accounts.Count > 0 ? totalByType / accounts.Count : 0):C}");
            }

            public void PrintInterestSummary()
            {
                Console.WriteLine("\n📊 ملخص الفوائد الشهرية:");
                Console.WriteLine("════════════════════════════════");

                decimal totalInterest = 0;
                foreach (var account in accounts)
                {
                    decimal interest = account.CalculateMonthlyInterest();
                    if (interest > 0)
                    {
                        Console.WriteLine($"  {account.GetAccountType()}: {interest:C}");
                        totalInterest += interest;
                    }
                }

                Console.WriteLine($"\n💵 إجمالي الفوائد: {totalInterest:C}");
            }
        }
    }
}
