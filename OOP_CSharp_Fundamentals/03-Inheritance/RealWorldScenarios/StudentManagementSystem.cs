using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.RealWorldScenarios
{
    public class Course
    {
        public int CourseId { get; }
        public string CourseName { get; }
        public int Credits { get; }

        public Course(int id, string name, int credits)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(credits, nameof(credits));

            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            CourseId = id;
            CourseName = name;
            Credits = credits;
        }

    }

    public class Grade
    {
        public enum Estimate
        {
            Excellent, VeryGood, Good, Acceptable, Fail,
        }
        public Course Course { get; }
        public double Score { get; }

        public Estimate TheEstimateInWord;
        public Grade(Course course, double score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score), "Must Be Positive ");

            ArgumentNullException.ThrowIfNull(course, nameof(course));

            Course = course;
            Score = score;
            TheEstimateInWord = CalculateTheEstimateInWords(score);
        }

        private Estimate CalculateTheEstimateInWords(double score)
        {
            return score switch
            {
                >= 90 => Estimate.Excellent,
                >= 80 => Estimate.VeryGood,
                >= 70 => Estimate.Good,
                >= 50 => Estimate.Acceptable,
                _ => Estimate.Fail
            };
        }

        public double GetGradePoint()
        {
            return TheEstimateInWord switch
            {
                Estimate.Excellent => 4.0,
                Estimate.VeryGood => 3.0,
                Estimate.Good => 2.0,
                Estimate.Acceptable => 1.0,
                _ => 0.0
            };
        }
    }

    public abstract class Student
    {
        public string StudentId { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime EnrollmentDate { get; }
        private readonly List<Grade> Grades = new();
        private readonly List<Course> EnrolledCourses = new();

        public IReadOnlyList<Grade> gradesValues { get; }
        public IReadOnlyList<Course> enrolledCoursesValues { get; }


        public Student(string studentId, string name, string email)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(studentId, nameof(studentId));
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            StudentId = studentId;
            Name = name;
            Email = email;
            EnrollmentDate = DateTime.Now;

            gradesValues = Grades;
            enrolledCoursesValues = EnrolledCourses;
        }
        public abstract double CalculateStudentGrade();

    }

    public class Undergraduate : Student
    {
        public Undergraduate(string studentId, string name, string email) : base(studentId, name, email) { }

        public override double CalculateStudentGrade()
        {
            throw new NotImplementedException();
        }
    }

    public class Graduate : Student
    {
        public Graduate(string studentId, string name, string email) : base(studentId, name, email) { }

        public override double CalculateStudentGrade()
        {
            throw new NotImplementedException();
        }
    }

    public class Exchange : Student
    {
        public Exchange(string studentId, string name, string email) : base(studentId, name, email) { }

        public override double CalculateStudentGrade()
        {
            throw new NotImplementedException();
        }
    }
}