// ❌ الطريقة الخاطئة
namespace Bad
{
    public class Employee
    {
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public int YearsOfService { get; set; }
        public List<decimal> DeductionsList { get; set; } = new List<decimal>();
    }

    public class PayrollCalculator
    {
        public decimal CalculateNetSalary(Employee employee)
        {
            // ❌ يعرف كل التفاصيل الداخلية
            var gross = employee.BaseSalary + employee.Bonus;

            if (employee.YearsOfService > 5)
            {
                gross += 1000; // loyalty bonus
            }

            var deductions = employee.DeductionsList.Sum();
            return gross - deductions;
        }
    }
}
