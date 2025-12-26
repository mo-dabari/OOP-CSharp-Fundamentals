/*
 * BasicInheritance.cs
 * ============================================
 * مثال بسيط لمفهوم الوراثة (Inheritance)
 * 
 * هذا الملف يوضح:
 * - الوراثة البسيطة من فئة أب
 * - Constructor و الوراثة
 * - virtual و override
 * - استخدام base
 * - الوراثة المتعددة المستويات
 * 
 * التشبيه: الطالب يرث من الإنسان
 * لكنه يضيف خصائص جديدة
 */

using System;
using System.Collections.Generic;

namespace OOP_CSharp_Fundamentals
{
    // ════════════════════════════════════════════════════════════
    // المستوى 1: الفئة الأب الأساسية
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// فئة الإنسان (الأب)
    /// تحتوي على الخصائص المشتركة لجميع الناس
    /// </summary>
    public class Person
    {
        // الخصائص
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        
        // Constructor
        public Person(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Console.WriteLine($"✅ تم إنشاء شخص: {Name}");
        }
        
        // الدوال
        public virtual void Introduce()
        {
            Console.WriteLine($"مرحباً، أنا {Name}، عمري {Age} سنة");
        }
        
        public virtual void Work()
        {
            Console.WriteLine($"{Name} يعمل");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"😴 {Name} نائم");
        }
        
        public void Eat()
        {
            Console.WriteLine($"🍽️  {Name} يأكل");
        }
        
        public virtual string GetInfo()
        {
            return $"{Name} ({Age} سنة)";
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // المستوى 2: الفئات الوارثة (الأطفال)
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// الطالب - يرث من Person
    /// يضيف خصائص جديدة مثل رقم الجامعة والمعدل
    /// </summary>
    public class Student : Person
    {
        // خصائص إضافية
        public string UniversityId { get; set; }
        public double GPA { get; set; }
        public string Major { get; set; }
        
        // Constructor
        public Student(string name, int age, string gender,
            string universityId, double gpa, string major)
            : base(name, age, gender)  // استدعاء constructor الأب
        {
            UniversityId = universityId;
            GPA = gpa;
            Major = major;
            Console.WriteLine($"   وهو طالب في {Major}");
        }
        
        // Override للدالة Work من الأب
        public override void Work()
        {
            Console.WriteLine($"📚 {Name} يدرس {Major}");
        }
        
        // Introduce مخصص للطالب
        public override void Introduce()
        {
            base.Introduce();  // استدعاء الأب أولاً
            Console.WriteLine($"   أنا طالب في {Major}");
            Console.WriteLine($"   رقمي الجامعي: {UniversityId}");
            Console.WriteLine($"   معدلي: {GPA:F2}");
        }
        
        // دالة جديدة خاصة بالطالب
        public void StudyForExam()
        {
            Console.WriteLine($"📖 {Name} يذاكر الامتحانات");
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" - طالب ({GPA:F2})";
        }
    }
    
    /// <summary>
    /// الموظف - يرث من Person
    /// يضيف خصائص الوظيفة والراتب
    /// </summary>
    public class Employee : Person
    {
        // خصائص إضافية
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeId { get; set; }
        
        // Constructor
        public Employee(string name, int age, string gender,
            string jobTitle, decimal salary, int employeeId)
            : base(name, age, gender)
        {
            JobTitle = jobTitle;
            Salary = salary;
            EmployeeId = employeeId;
            Console.WriteLine($"   وهو يعمل كـ {JobTitle}");
        }
        
        // Override Work
        public override void Work()
        {
            Console.WriteLine($"💼 {Name} يعمل كـ {JobTitle}");
            Console.WriteLine($"   الراتب: {Salary:C}");
        }
        
        // Override Introduce
        public override void Introduce()
        {
            base.Introduce();
            Console.WriteLine($"   أنا {JobTitle}");
            Console.WriteLine($"   رقم الموظف: {EmployeeId}");
        }
        
        // دوال خاصة بالموظف
        public void AttendMeeting()
        {
            Console.WriteLine($"📊 {Name} يحضر اجتماع");
        }
        
        public void SubmitReport(string report)
        {
            Console.WriteLine($"📄 {Name} قدم تقرير: {report}");
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" - {JobTitle}";
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // المستوى 3: الوراثة المتعددة المستويات
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// المدير - يرث من Employee
    /// يضيف مسؤوليات إدارية
    /// </summary>
    public class Manager : Employee
    {
        // خصائص إضافية
        public int TeamSize { get; set; }
        public List<string> TeamMembers { get; set; }
        
        // Constructor
        public Manager(string name, int age, string gender,
            string jobTitle, decimal salary, int employeeId, int teamSize)
            : base(name, age, gender, jobTitle, salary, employeeId)
        {
            TeamSize = teamSize;
            TeamMembers = new List<string>();
            Console.WriteLine($"   وهو مدير فريق من {teamSize} أشخاص");
        }
        
        // Override Work
        public override void Work()
        {
            base.Work();  // استدعاء Employee.Work
            Console.WriteLine($"   يدير فريق من {TeamSize} موظفين");
        }
        
        // Override Introduce
        public override void Introduce()
        {
            base.Introduce();  // استدعاء Employee.Introduce
            Console.WriteLine($"   أدير فريق من {TeamSize} أشخاص");
        }
        
        // دوال إدارية
        public void AssignTask(string memberName, string task)
        {
            Console.WriteLine($"📋 {Name} أسند مهمة لـ {memberName}: {task}");
        }
        
        public void EvaluateEmployee(string memberName, double score)
        {
            Console.WriteLine($"⭐ {Name} قيّم {memberName} بـ {score}/10");
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" (مدير فريق)";
        }
    }
    
    /// <summary>
    /// طالب دراسات عليا - يرث من Student
    /// يضيف بحث أكاديمي وإشراف
    /// </summary>
    public class GraduateStudent : Student
    {
        // خصائص جديدة
        public string ResearchTopic { get; set; }
        public string Advisor { get; set; }
        public int PublishedPapers { get; set; }
        
        // Constructor
        public GraduateStudent(string name, int age, string gender,
            string universityId, double gpa, string major,
            string researchTopic, string advisor)
            : base(name, age, gender, universityId, gpa, major)
        {
            ResearchTopic = researchTopic;
            Advisor = advisor;
            PublishedPapers = 0;
            Console.WriteLine($"   وهو يقوم بأبحاث في {researchTopic}");
        }
        
        // Override Work
        public override void Work()
        {
            base.Work();  // استدعاء Student.Work
            Console.WriteLine($"   يقوم بأبحاث في {ResearchTopic}");
        }
        
        // دوال بحثية
        public void PublishPaper(string paperTitle)
        {
            PublishedPapers++;
            Console.WriteLine($"📚 {Name} نشر ورقة: {paperTitle}");
        }
        
        public void AttendConference(string conferenceName)
        {
            Console.WriteLine($"🎓 {Name} حضر مؤتمر: {conferenceName}");
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" - باحث في {ResearchTopic}";
        }
    }
    
    
    // ════════════════════════════════════════════════════════════
    // مثال على sealed class (لا يمكن الوراثة منها)
    // ════════════════════════════════════════════════════════════
    
    /// <summary>
    /// فئة مختومة - لا يمكن الوراثة منها
    /// </summary>
    public sealed class Doctor : Person
    {
        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        
        public Doctor(string name, int age, string gender,
            string licenseNumber, string specialization)
            : base(name, age, gender)
        {
            LicenseNumber = licenseNumber;
            Specialization = specialization;
        }
        
        public override void Work()
        {
            Console.WriteLine($"⚕️  {Name} يعالج المرضى في تخصص {Specialization}");
        }
        
        public void TreatPatient(string patientName)
        {
            Console.WriteLine($"🏥 {Name} يعالج {patientName}");
        }
    }
    
    // ❌ هذا سيرمي خطأ تجميع:
    // public class Cardiologist : Doctor { }  // لا يمكن!
    
    
    // ════════════════════════════════════════════════════════════
    // Program - التشغيل والاختبار
    // ════════════════════════════════════════════════════════════
    
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  مثال بسيط على الوراثة (Inheritance)");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            // ═════════════════════════════════
            // 1️⃣ الوراثة البسيطة
            // ═════════════════════════════════
            Console.WriteLine("1️⃣  الوراثة البسيطة:");
            Console.WriteLine("────────────────────────────────────\n");
            
            var student = new Student("أحمد محمد", 20, "ذكر",
                "2023001", 3.8, "الهندسة");
            
            Console.WriteLine("\nمعلومات الطالب:");
            student.Introduce();
            Console.WriteLine($"المعلومات المختصرة: {student.GetInfo()}");
            
            student.Work();
            student.StudyForExam();
            student.Sleep();
            
            // ═════════════════════════════════
            // 2️⃣ وراثة أخرى (الموظف)
            // ═════════════════════════════════
            Console.WriteLine("\n\n2️⃣  وراثة أخرى (الموظف):");
            Console.WriteLine("────────────────────────────────────\n");
            
            var employee = new Employee("فاطمة علي", 35, "أنثى",
                "محللة برمجيات", 5000, 1001);
            
            Console.WriteLine("\nمعلومات الموظفة:");
            employee.Introduce();
            employee.Work();
            employee.AttendMeeting();
            employee.SubmitReport("تقرير الأداء الشهري");
            
            // ═════════════════════════════════
            // 3️⃣ الوراثة المتعددة المستويات
            // ═════════════════════════════════
            Console.WriteLine("\n\n3️⃣  الوراثة المتعددة المستويات (Manager):");
            Console.WriteLine("────────────────────────────────────\n");
            
            var manager = new Manager("محمد حسن", 40, "ذكر",
                "مدير المشاريع", 8000, 2001, 5);
            
            Console.WriteLine("\nمعلومات المدير:");
            manager.Introduce();
            manager.Work();
            manager.AssignTask("أحمد", "تطوير واجهة المستخدم");
            manager.EvaluateEmployee("فاطمة", 8.5);
            
            // ═════════════════════════════════
            // 4️⃣ ثلاث مستويات وراثة
            // ═════════════════════════════════
            Console.WriteLine("\n\n4️⃣  ثلاث مستويات وراثة (GraduateStudent):");
            Console.WriteLine("────────────────────────────────────\n");
            
            var gradStudent = new GraduateStudent("نور الدين", 25, "ذكر",
                "2020015", 3.9, "الذكاء الاصطناعي",
                "التعلم العميق", "د. أحمد");
            
            Console.WriteLine("\nمعلومات طالب الدراسات العليا:");
            gradStudent.Introduce();
            gradStudent.Work();
            gradStudent.PublishPaper("شبكات عصبية متقدمة");
            gradStudent.PublishPaper("معالجة اللغة الطبيعية");
            gradStudent.AttendConference("مؤتمر الذكاء الاصطناعي 2024");
            
            // ═════════════════════════════════
            // 5️⃣ الفئة المختومة (Sealed)
            // ═════════════════════════════════
            Console.WriteLine("\n\n5️⃣  الفئة المختومة (Doctor):");
            Console.WriteLine("────────────────────────────────────\n");
            
            var doctor = new Doctor("سارة أحمد", 38, "أنثى",
                "LIC2020456", "أطفال");
            
            Console.WriteLine("\nمعلومات الطبيب:");
            doctor.Introduce();
            doctor.Work();
            doctor.TreatPatient("محمود");
            
            // ═════════════════════════════════
            // 6️⃣ Polymorphism - قائمة من الناس
            // ═════════════════════════════════
            Console.WriteLine("\n\n6️⃣  Polymorphism - معاملة موحدة:");
            Console.WriteLine("────────────────────────────────────\n");
            
            List<Person> people = new()
            {
                student,
                employee,
                manager,
                gradStudent,
                doctor
            };
            
            Console.WriteLine("معلومات الجميع:");
            foreach (var person in people)
            {
                Console.WriteLine($"  • {person.GetInfo()}");
            }
            
            Console.WriteLine("\nجميع يعملون:");
            foreach (var person in people)
            {
                person.Work();
            }
            
            // ═════════════════════════════════
            // الملخص
            // ═════════════════════════════════
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ ملخص الوراثة:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            1️⃣  الفئة الأب توفر الأساس:
                - الخصائص المشتركة (Name, Age)
                - الدوال المشتركة (Introduce, Work)
            
            2️⃣  الفئات الابنة ترث وتضيف:
                - جميع خصائص ودوال الأب
                - خصائص جديدة (UniversityId, Salary)
                - دوال جديدة (StudyForExam, AttendMeeting)
            
            3️⃣  Override يسمح بتعديل السلوك:
                - كل فئة تعدل Work() بطريقتها
                - base استدعاء الأب
            
            4️⃣  Constructor chain مهم:
                - Student يستدعي : base(name, age, gender)
                - Manager يستدعي base من Employee
            
            5️⃣  Sealed تمنع الوراثة:
                - Doctor هي sealed
                - لا يمكن أن ترث منها فئة أخرى
            
            6️⃣  Polymorphism يعمل تلقائياً:
                - قائمة Person تحتوي أنواع مختلفة
                - كل واحد يتصرف حسب نوعه
            """);
            
            // مقارنة مع بدون وراثة
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  📊 مقارنة:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            ❌ بدون وراثة:
               - تكرار الخصائص في كل فئة
               - 5 فئات × 3 خصائص = 15 خصية مكررة
               - صيانة صعبة جداً
            
            ✅ مع وراثة:
               - تعريف واحد للخصائص المشتركة
               - كل فئة تضيف فقط ما تحتاج
               - صيانة سهلة وآمنة
            """);
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}