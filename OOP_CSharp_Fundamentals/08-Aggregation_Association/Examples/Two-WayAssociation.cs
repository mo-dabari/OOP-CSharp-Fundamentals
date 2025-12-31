using System;
using System.Collections.Generic;

namespace Aggregation_Association.Examples
{
    // ════════════════════════════════════════════════════════════
    // ASSOCIATION - علاقة Two-Way
    // ════════════════════════════════════════════════════════════

    public class Teacher
    {
        public string Name { get; set; }
        private List<Student> students;

        public Teacher(string name)
        {
            Name = name;
            students = new List<Student>();
        }

        public void Teach(Student student)
        {
            students.Add(student);
            Console.WriteLine($"👨‍🏫 {Name} يدرس {student.Name}");
        }

        public void DisplayStudents()
        {
            Console.WriteLine($"\n📝 طلاب {Name}:");
            foreach (var student in students)
                Console.WriteLine($"  • {student.Name}");
        }
    }
}
