/*
 * AdvancedPolymorphism.cs
 * ════════════════════════════════════════════════════════════
 * مثال متقدم: Polymorphism في الأنظمة المعقدة
 *
 * يوضح:
 * - Polymorphism مع Abstract Classes
 * - Polymorphism مع Interfaces
 * - Multiple Inheritance مع Interfaces
 * - قوائم Polymorphic
 * - الحسابات المعقدة
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism.Examples.Advanced
{
    // ════════════════════════════════════════════════════════════
    // الموظفون (مثال آخر للـ Polymorphism)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الموظف - الأب
    /// </summary>
    public abstract class Employee
    {
        public string name { get; }
        protected decimal salary;

        public Employee(string name, decimal salary)
        {
            this.name = name;
            this.salary = salary;
        }

        public virtual void Work()
        {
            Console.WriteLine($"👤 {name} يعمل");
        }

        public virtual decimal CalculateSalary()
        {
            return salary;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"الموظف: {name}");
            Console.WriteLine($"الراتب: {CalculateSalary():C}");
        }
    }

    /// <summary>
    /// مطور برامج
    /// </summary>
    public class Developer : Employee
    {
        private int projectsCompleted;

        public Developer(string name, decimal salary, int projects)
            : base(name, salary)
        {
            projectsCompleted = projects;
        }

        public override void Work()
        {
            Console.WriteLine($"💻 {name} يبرمج");
        }

        public override decimal CalculateSalary()
        {
            // راتب أساسي + مكافأة المشاريع
            return salary + (projectsCompleted * 500);
        }
    }

    /// <summary>
    /// مدير
    /// </summary>
    public class Manager : Employee
    {
        private int teamSize;

        public Manager(string name, decimal salary, int team)
            : base(name, salary)
        {
            teamSize = team;
        }

        public override void Work()
        {
            Console.WriteLine($"📊 {name} يدير الفريق");
        }

        public override decimal CalculateSalary()
        {
            // راتب أساسي + بدل إدارة
            return salary + (teamSize * 1000);
        }
    }

    /// <summary>
    /// مصمم
    /// </summary>
    public class Designer : Employee
    {
        private int designsCreated;

        public Designer(string name, decimal salary, int designs)
            : base(name, salary)
        {
            designsCreated = designs;
        }

        public override void Work()
        {
            Console.WriteLine($"🎨 {name} يصمم");
        }

        public override decimal CalculateSalary()
        {
            // راتب أساسي + مكافأة التصاميم
            return salary + (designsCreated * 300);
        }
    }


    /// <summary>
    /// نظام إدارة الموظفين
    /// </summary>
    public class HRManagementSystem
    {
        private List<Employee> employees = new();

        public void AddEmployee(Employee emp)
        {
            employees.Add(emp);
        }

        public void MakeEveryoneWork()
        {
            Console.WriteLine("\n👥 جميع الموظفين يعملون:");
            foreach (var emp in employees)
            {
                emp.Work();
            }
        }

        public void PrintPayroll()
        {
            Console.WriteLine("\n💰 قائمة الرواتب:");
            decimal totalPayroll = 0;

            foreach (var emp in employees)
            {
                decimal salary = emp.CalculateSalary();
                Console.WriteLine($"  {emp.name}: {salary:C}");
                totalPayroll += salary;
            }

            Console.WriteLine($"\nإجمالي الرواتب: {totalPayroll:C}");
        }

        public void PrintAllInfo()
        {
            Console.WriteLine("\n📋 معلومات جميع الموظفين:");
            foreach (var emp in employees)
            {
                emp.PrintInfo();
                Console.WriteLine("────────────────────");
            }
        }

        public decimal GetTotalPayroll()
        {
            return employees.Sum(e => e.CalculateSalary());
        }

        public Employee GetHighestPaid()
        {
            return employees.OrderByDescending(e => e.CalculateSalary()).FirstOrDefault();
        }
    }

}
