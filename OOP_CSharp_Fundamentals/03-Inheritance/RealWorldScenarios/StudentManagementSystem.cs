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
        
        public Course(string id, string name, int credits)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id),"Must Be Positive ");

            if(credits <= 0)
                throw new ArgumentOutOfRangeException(nameof(credits),"Must Be credits Positive ");

            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            CourseId = id;
            CourseName = name;
            Credits = credits;
        }

    }

    public class Grade
    {
        private enum Estimate
        {
            Excellent, VeryGood, Good, Acceptable, Fail,
        }
        public Course Course { get; }
        public double Score { get; } 

        public string Estimate {get;}
        public Grade(Course course, double score)
        {
            if(score < 0)
                throw new ArgumentOutOfRangeException(nameof(score),"Must Be Positive ");

            ArgumentException.ThrowIfNullOrEmpty(course, nameof(course));

            Course = course;
            Score = score;
            TheEstimateInWord = CalculateTheEstimateInWords(scoure);
        }

        private Estimate CalculateTheEstimateInWords(double score)
        {
            return score switch
            {
                >= 90 => Estimate.Excellent,
                >= 80 => Estimate.VeryGood,
                >= 70 => Estimate.Good,
                >= 50 => Estimate.Acceptable,
                _     => Estimate.Fail
            };
        }

        public double GetGradePoint()
        {
            return LetterGrade switch
            {
                Estimate.Excellent  => 4.0,
                Estimate.VeryGood   => 3.0,
                Estimate.Good       => 2.0,
                Estimate.Acceptable => 1.0,
                _                   => 0.0
            };
        }
    }

    public abstract class Student
    {
        public string StudentId {get;}
        public string Name {get;}
        public string Email {get;}
        public DateTime EnrollmentDate {get;}
        private readonly List<Grade> Grades = new();
        private readonly List<Course> EnrolledCourses = new();

        public IReadOnlyList<Grade> _gradesValues {get;}
        public IReadOnlyList<Course> _enrolledCoursesValues {get;}


        public Student(string studentId, string name, string email)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(studentId , nameof(studentId));
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
        
    }

    public class Graduate : Student
    {
        
    }

    public class Exchange : Student
    {
        
    }
}