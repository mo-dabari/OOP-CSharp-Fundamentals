using System;
using System.Collections.Generic;

namespace Aggregation_Association.Examples
{
    // ════════════════════════════════════════════════════════════
    // AGGREGATION - علاقة Weak
    // ════════════════════════════════════════════════════════════

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Department
    {
        public string Name { get; set; }
        private List<Employee> employees;  // Aggregation

        public Department(string name)
        {
            Name = name;
            employees = new List<Employee>();
        }

        public void HireEmployee(Employee emp)
        {
            employees.Add(emp);
            Console.WriteLine($"✅ {emp.Name} تم توظيفها في {Name}");
        }

        public void RemoveEmployee(Employee emp)
        {
            employees.Remove(emp);
            Console.WriteLine($"❌ {emp.Name} غادرت {Name}");
            // لكن emp تبقى موجودة!
        }

        public void DisplayEmployees()
        {
            Console.WriteLine($"\n👥 موظفو {Name}:");
            foreach (var emp in employees)
                Console.WriteLine($"  • {emp.Name}");
        }
    }
}
