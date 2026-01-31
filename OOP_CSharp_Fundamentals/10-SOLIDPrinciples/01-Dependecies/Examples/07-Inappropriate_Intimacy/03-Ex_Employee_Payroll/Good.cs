// ✅ الطريقة الصحيحة
namespace InappropriateIntimacy.EmployeePayroll.Good
{
    public class Employee
    {
        private readonly decimal _baseSalary;
        private decimal _bonus;
        private readonly int _yearsOfService;
        private readonly List<decimal> _deductions = new List<decimal>();

        public Employee(decimal baseSalary, int yearsOfService)
        {
            _baseSalary = baseSalary;
            _yearsOfService = yearsOfService;
        }

        public void AddBonus(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Bonus cannot be negative");
            }
            _bonus += amount;
        }

        public void AddDeduction(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Deduction cannot be negative");
            }
            _deductions.Add(amount);
        }

        // ✅ Employee يحسب راتبه بنفسه
        public decimal CalculateGrossSalary()
        {
            var gross = _baseSalary + _bonus;

            // Loyalty bonus
            if (_yearsOfService > 5)
            {
                gross += 1000;
            }

            return gross;
        }

        public decimal CalculateTotalDeductions()
        {
            return _deductions.Sum();
        }

        public decimal CalculateNetSalary()
        {
            return CalculateGrossSalary() - CalculateTotalDeductions();
        }
    }

    public class PayrollService
    {
        public void ProcessPayroll(Employee employee)
        {
            // ✅ فقط يطلب النتيجة النهائية
            var netSalary = employee.CalculateNetSalary();
            Console.WriteLine($"Paying employee: ${netSalary}");
        }
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
}

// ✅ القواعد الذهبية لتجنب Inappropriate Intimacy:
// 1. استخدم private للبيانات الحساسة
// 2. وفر public methods للتعامل مع البيانات
// 3. كل كلاس مسؤول عن حالته الخاصة
// 4. لا تعرض internal state بشكل مباشر
// 5. استخدم readonly properties بدل public setters
