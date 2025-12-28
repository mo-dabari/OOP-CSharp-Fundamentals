using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.RealWorldScenarios
{
    public class Course
    {
        public string CourseId { get; }
        public string CourseName { get; }
        public int Credits { get; }

        public Course(string id, string name, int credits)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id, nameof(id));

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
        public IReadOnlyList<Grade> _gradesValues { get; }
        public IReadOnlyList<Course> _enrolledCoursesValues { get; }
        public Student(string studentId, string name, string email)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(studentId, nameof(studentId));
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            StudentId = studentId;
            Name = name;
            Email = email;
            EnrollmentDate = DateTime.Now;

            _gradesValues = Grades.AsReadOnly();
            _enrolledCoursesValues = EnrolledCourses.AsReadOnly();
        }
        public virtual void AddCourse(Course course)
        {
            ArgumentNullException.ThrowIfNull(course , nameof(course));
            EnrolledCourses.Add(course);
        }
        public virtual void AddGrade(Course course, double score)
        {
            ArgumentNullException.ThrowIfNull(course , nameof(course));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(score , nameof(score));

            Grade grade = new (course, score);
            Grades.Add(grade);
        }
        public virtual double CalculateGPA()
        {
            if(Grades.Count == 0) return 0;

            double totalPoint = 0;
            double totalCredits = 0;

            foreach (Grade grade in Grades)
            {
                totalPoint += grade.GetGradePoint() * grade.Course.Credits;
                totalCredits += grade.Course.Credits;
            }

            return totalCredits > 0 ? totalPoint / totalCredits: 0;

        }
        public virtual double CalculateTuitionFee()
        {
            return EnrolledCourses.Count * 1000;
        }
         public virtual bool IsGoodStanding()
        {
            return CalculateGPA() >= 2.0;
        }
        public virtual void DisplayAcademicInfo()
        {
            Console.WriteLine($"\n👤 الطالب: {Name}");
            Console.WriteLine($"   المعرف: {StudentId}");
            Console.WriteLine($"   البريد: {Email}");
            Console.WriteLine($"   تاريخ التسجيل: {EnrollmentDate:yyyy-MM-dd}");
            Console.WriteLine($"   المعدل التراكمي: {CalculateGPA():F2}");
            Console.WriteLine($"   الرسوم الدراسية: {CalculateTuitionFee():C}");
            Console.WriteLine($"   الحالة: {(IsGoodStanding() ? "ممتاز ✅" : "في خطر ⚠️")}");
        }
        public virtual string GetStudentType()
        {
            return "طالب عام";
        }
        public virtual void PrintTranscript()
        {
            Console.WriteLine($"\n📚 السجل الأكاديمي: {Name}");
            Console.WriteLine("════════════════════════════════");

            if (Grades.Count == 0)
            {
                Console.WriteLine("   لا توجد درجات حالياً");
                return;
            }

            foreach (var grade in Grades)
            {
                Console.WriteLine($"  {grade.Course.CourseName}: {grade.Score}/100 ({grade.TheEstimateInWord})");
            }

            Console.WriteLine($"\n  المعدل: {CalculateGPA():F2}");
        }
    }

    public class Undergraduate : Student
    {
        public byte Year {get;} //1-4
        public string Major {get;}
        public short InternshipHours {get; private set;}
        public Undergraduate(string studentId, string name, string email , byte year, string major )
            : base(studentId, name, email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(major, nameof(major));

            if(year <= 0 || year > 4)
                throw new ArgumentOutOfRangeException(nameof(year));

            Major = major;
            Year = year;
            InternshipHours = 0;
        }

        public override string GetStudentType()
        {
            return $"طالب عام (السنة {Year})";
        }

        public override double CalculateTuitionFee()
        {
            // رسوم أساسية + تخفيف حسب المعدل
            double baseFee = base.CalculateTuitionFee();
            double gpa = CalculateGPA();

            if (gpa >= 3.5) return baseFee * 0.8;
            if (gpa >= 3.0) return baseFee * 0.9;
            return baseFee;
        }

        public void CompleteInternship(short hours)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(hours , nameof(hours));

            InternshipHours += hours;
            Console.WriteLine($"🏢 {Name} أكمل {hours} ساعة تدريب (إجمالي: {InternshipHours})");
        }

        public bool CanGraduate()
        {
            // يجب 120 وحدة دراسية و GPA 2.0 على الأقل
            int totalCredits = _enrolledCoursesValues.Sum(c => c.Credits);
            return totalCredits >= 120 && CalculateGPA() >= 2.0;
        }

        public override void DisplayAcademicInfo()
        {
            base.DisplayAcademicInfo();
            Console.WriteLine($"   التخصص: {Major}");
            Console.WriteLine($"   السنة: {Year}");
            Console.WriteLine($"   ساعات التدريب: {InternshipHours}");
            Console.WriteLine($"   يمكن التخرج: {(CanGraduate() ? "نعم ✅" : "لا ❌")}");
        }
    }

    public class Graduate : Student
    {
        public enum Degrees { Master, PhD}
        public Degrees Degree {get;}
        public string ResearchTopic {get;}
        private List<string> _publications = new();
        public IReadOnlyList<string> PublicationsValues;


        public Graduate(string studentId, string name, string email, Degrees degree, string researchTopic)
            : base(studentId, name, email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(researchTopic , nameof(researchTopic));

            Degree = degree;
            ResearchTopic = researchTopic;
            PublicationsValues = _publications.AsReadOnly();
        }

        public override string GetStudentType()
        {
            return $"طالب دراسات عليا ({Degree})";
        }

        public override double CalculateTuitionFee()
        {
            // رسوم أعلى للدراسات العليا
            double baseFee = base.CalculateTuitionFee();
            return baseFee * 1.5;  // 50% أكثر
        }

        public override bool IsGoodStanding()
        {
            // معدل أعلى للدراسات العليا
            return CalculateGPA() >= 3.0;
        }

        public void PublishResearch(string title)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title , nameof(title));
            _publications.Add(title);
            Console.WriteLine($"📖 {Name} نشر بحث: {title}");
        }

        public void PresentAtConference(string conferenceName)
        {
            Console.WriteLine($"🎤 {Name} قدم بحث في مؤتمر {conferenceName}");
        }

        public override void DisplayAcademicInfo()
        {
            base.DisplayAcademicInfo();
            Console.WriteLine($"   الدرجة: {Degree}");
            Console.WriteLine($"   موضوع البحث: {ResearchTopic}");
            Console.WriteLine($"   عدد الأبحاث المنشورة: {PublicationsValues.Count}");
        }

        public override void PrintTranscript()
        {
            base.PrintTranscript();
            if (_publications.Count > 0)
            {
                Console.WriteLine($"\n  الأبحاث المنشورة:");
                foreach (var pub in _publications)
                    Console.WriteLine($"    • {pub}");
            }
        }

    }

    public class Exchange : Student
    {
        public string HomeCountry {get;}
        public string HomeUniversity {get;}
        public byte DurationMonths;
        public Exchange(string studentId, string name, string email, string homeCountry, string homeUniversity, byte duration)
            : base(studentId, name, email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(homeCountry , nameof(homeCountry));
            ArgumentException.ThrowIfNullOrWhiteSpace(homeUniversity , nameof(homeUniversity));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration);
            HomeCountry = homeCountry;
            HomeUniversity = homeUniversity;
            DurationMonths = duration;
        }

         public override string GetStudentType()
        {
            return $"طالب تبادل من {HomeCountry}";
        }

        public override double CalculateTuitionFee()
        {
            // قد لا يدفع الرسوم (حسب الاتفاق)
            return 0;  // معفى من الرسوم
        }

        public override double CalculateGPA()
        {
            // قد لا يكون لديه GPA تقليدي
            return base.CalculateGPA();
        }

        public void ReturnToHomeUniversity()
        {
            Console.WriteLine($"✈️  {Name} عاد إلى جامعة {HomeUniversity} في {HomeCountry}");
        }

        public void GetCultureExchange()
        {
            Console.WriteLine($"🌍 {Name} شارك في برنامج التبادل الثقافي");
        }

        public override void DisplayAcademicInfo()
        {
            base.DisplayAcademicInfo();
            Console.WriteLine($"   الدولة الأصلية: {HomeCountry}");
            Console.WriteLine($"   الجامعة الأصلية: {HomeUniversity}");
            Console.WriteLine($"   مدة البرنامج: {DurationMonths} شهر");
            Console.WriteLine($"   الرسوم: معفى");
        }

    }


// ════════════════════════════════════════════════════════════
    // نظام إدارة الجامعة
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// نظام إدارة الطلاب والمعاملات الأكاديمية
    /// </summary>
    public class UniversityManagementSystem
    {
        private List<Student> students;
        private List<Course> courses;

        public UniversityManagementSystem()
        {
            students = new List<Student>();
            courses = new List<Course>();
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
            Console.WriteLine($"✅ تم تسجيل الطالب: {student.Name}");
        }

        public void AddCourse(Course course)
        {
            courses.Add(course);
            Console.WriteLine($"✅ تم إضافة المادة: {course.CourseName}");
        }

        public void DisplayAllStudents()
        {
            Console.WriteLine("\n👥 جميع الطلاب:");
            Console.WriteLine("════════════════════════════════");

            foreach (var student in students)
                Console.WriteLine($"  • {student.Name} ({student.GetStudentType()})");
        }

        public void PrintStudentsByType()
        {
            Console.WriteLine("\n📋 الطلاب حسب النوع:");
            Console.WriteLine("════════════════════════════════");

            var grouped = students.GroupBy(s => s.GetStudentType());

            foreach (var group in grouped)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var student in group)
                    Console.WriteLine($"  • {student.Name}");
            }
        }

        public void PrintAcademicReport()
        {
            Console.WriteLine("\n📊 التقرير الأكاديمي:");
            Console.WriteLine("════════════════════════════════");

            foreach (var student in students)
                student.DisplayAcademicInfo();
        }

        public void PrintTuitionReport()
        {
            Console.WriteLine("\n💰 تقرير الرسوم الدراسية:");
            Console.WriteLine("════════════════════════════════");

            decimal total = 0;
            foreach (var student in students)
            {
                decimal fee = (decimal)student.CalculateTuitionFee();
                total += fee;
                Console.WriteLine($"  {student.Name}: {fee:C}");
            }

            Console.WriteLine($"\nالإجمالي: {total:C}");
        }

        public double GetAverageGPA()
        {
            if (students.Count == 0) return 0;
            return students.Average(s => s.CalculateGPA());
        }

        public void PrintStatistics()
        {
            Console.WriteLine("\n📈 الإحصائيات:");
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"عدد الطلاب: {students.Count}");
            Console.WriteLine($"عدد المواد: {courses.Count}");
            Console.WriteLine($"المعدل الجامعي: {GetAverageGPA():F2}");

            var goodStanding = students.Count(s => s.IsGoodStanding());
            var atRisk = students.Count - goodStanding;

            Console.WriteLine($"الطلاب بتفوق: {goodStanding}");
            Console.WriteLine($"الطلاب في خطر: {atRisk}");
        }
    }


}
