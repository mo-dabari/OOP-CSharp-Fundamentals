using System;
using System.Collections.Generic;
using Aggregation_Association.Examples;

namespace Aggregation_Association.RealWorldScenarios
{
    public class University
    {
        public string Name { get; set; }
        private List<Department> departments;  // Aggregation

        public University(string name)
        {
            Name = name;
            departments = new List<Department>();
        }

        public void AddDepartment(Department dept)
        {
            departments.Add(dept);
        }

        public void DisplayStructure()
        {
            Console.WriteLine($"\n🏫 {Name}:");
            foreach (var dept in departments)
                dept.DisplayEmployees();
        }
    }
}
