using System;
using System.Collections.Generic;
using System.Linq;

namespace Encapsulation.Exercises
{
    public class Employee
    {
        // Private fields - البيانات الحساسة جداً
        private string name;
        private decimal salary;
        private string socialSecurityNumber;  // رقم ضمان اجتماعي
        private DateTime hireDate;

        public Employee(string name, decimal salary, string ssn)
        {
            Name = name;
            Salary = salary;
            SocialSecurityNumber = ssn;
            hireDate = DateTime.Now;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new ArgumentException("الاسم لا يمكن أن يكون فارغ");
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value >= 1000)  // حد أدنى معقول
                    salary = value;
                else
                    throw new ArgumentException("الراتب يجب أن يكون 1000 على الأقل");
            }
        }

        public string SocialSecurityNumber
        {
            get
            {
                // إخفاء معظم الرقم
                if (socialSecurityNumber.Length >= 4)
                    return "***-**-" + socialSecurityNumber.Substring(socialSecurityNumber.Length - 4);
                return "****";
            }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length == 11)  // XXX-XX-XXXX
                    socialSecurityNumber = value;
                else
                    throw new ArgumentException("رقم الضمان الاجتماعي غير صحيح");
            }
        }

        public DateTime HireDate => hireDate;  // Read-only

        public int GetExperienceYears()
        {
            return (int)(DateTime.Now - hireDate).TotalDays / 365;
        }

        public decimal CalculateBonus()
        {
            int years = GetExperienceYears();
            return salary * (years * 0.05m);  // 5% لكل سنة
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"👤 {Name}");
            Console.WriteLine($"   الراتب: {Salary:C}");
            Console.WriteLine($"   SSN: {SocialSecurityNumber}");
            Console.WriteLine($"   سنوات الخبرة: {GetExperienceYears()}");
            Console.WriteLine($"   المكافأة: {CalculateBonus():C}");
        }
    }

    public class Department
    {
        private string name;
        private List<Employee> employees;

        public Department(string name)
        {
            this.name = name;
            employees = new List<Employee>();
        }

        public string Name => name;  // Read-only

        public void AddEmployee(Employee emp)
        {
            if (emp != null)
            {
                employees.Add(emp);
                Console.WriteLine($"✅ تم إضافة {emp.Name} للقسم");
            }
        }

        public void DisplayAllEmployees()
        {
            Console.WriteLine($"\n📋 موظفو قسم {name}:");
            foreach (var emp in employees)
            {
                emp.DisplayInfo();
                Console.WriteLine();
            }
        }

        public decimal GetTotalSalaries()
        {
            return employees.Sum(e => e.Salary);
        }

        public decimal GetTotalBonuses()
        {
            return employees.Sum(e => e.CalculateBonus());
        }

        public void PrintPayrollReport()
        {
            Console.WriteLine($"\n💰 تقرير الرواتب - قسم {name}:");
            Console.WriteLine($"  عدد الموظفين: {employees.Count}");
            Console.WriteLine($"  إجمالي الرواتب: {GetTotalSalaries():C}");
            Console.WriteLine($"  إجمالي المكافآت: {GetTotalBonuses():C}");
            Console.WriteLine($"  الإجمالي: {GetTotalSalaries() + GetTotalBonuses():C}");
        }
    }
}
