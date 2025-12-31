using System;
using System.Collections.Generic;

namespace Aggregation_Association.Examples
{
    // ════════════════════════════════════════════════════════════
    // ASSOCIATION - علاقة One-Way
    // ════════════════════════════════════════════════════════════

    public class Course
    {
        public string Code { get; set; }
        public string Title { get; set; }

        public Course(string code, string title)
        {
            Code = code;
            Title = title;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private List<Course> enrolledCourses;  // Association

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
            enrolledCourses = new List<Course>();
        }

        public void EnrollInCourse(Course course)
        {
            enrolledCourses.Add(course);
            Console.WriteLine($"✅ {Name} سجل في {course.Title}");
        }

        public void DropCourse(Course course)
        {
            enrolledCourses.Remove(course);
            Console.WriteLine($"❌ {Name} ترك {course.Title}");
            // Course تبقى موجود للطلاب الآخرين
        }

        public void DisplayCourses()
        {
            Console.WriteLine($"\n📚 مقررات {Name}:");
            foreach (var course in enrolledCourses)
                Console.WriteLine($"  • {course.Title}");
        }
    }
}
