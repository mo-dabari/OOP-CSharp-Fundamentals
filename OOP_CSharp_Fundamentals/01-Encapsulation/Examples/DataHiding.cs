/*
 * DataHiding.cs
 * ============================================
 * شرح إخفاء البيانات (Data Hiding) بتفاصيل عميقة
 * 
 * هذا الملف يوضح:
 * - الفرق بين Access Modifiers
 * - الحالات الحقيقية لإخفاء البيانات
 * - أمثلة عملية من التطبيقات الفعلية
 * - Best Practices في الكود الاحترافي
 * 
 * الهدف: فهم عميق لـ Data Hiding والأمان في البيانات
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_CSharp_Fundamentals
{
    // ============================================
    // مثال 1: عدم استخدام Data Hiding (❌ خطأ)
    // ============================================
    
    /// <summary>
    /// مثال خاطئ: بيانات معرضة للتعديل العشوائي
    /// 
    /// ⚠️ لا تفعل هذا أبداً في الكود الحقيقي!
    /// </summary>
    public class EmployeeBad
    {
        // 🚨 جميع البيانات عامة (Public)
        public string Name;
        public decimal Salary;
        public int YearsOfService;
        
        // بدون أي حماية أو تحقق!
        public void GiveRaise(decimal amount)
        {
            Salary += amount;  // لا يوجد تحقق!
        }
    }
    
    // استخدام خاطئ:
    // var employee = new EmployeeBad { Name = "أحمد", Salary = 5000 };
    // employee.Salary = -10000;  // ❌ راتب سالب! (خطر!)
    // employee.YearsOfService = -5;  // ❌ سنوات خدمة سالبة! (مستحيل!)
    
    
    // ============================================
    // مثال 2: استخدام Data Hiding الصحيح
    // ============================================
    
    /// <summary>
    /// مثال صحيح: بيانات محمية مع Validation
    /// 
    /// ✅ هذا هو النمط الصحيح للكود الاحترافي
    /// </summary>
    public class EmployeeGood
    {
        // 🔒 جميع البيانات خاصة (Private)
        private string name;
        private decimal salary;
        private int yearsOfService;
        private readonly DateTime hireDate;
        private readonly List<decimal> salaryHistory;
        
        // Properties مع Validation
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("الاسم لا يمكن أن يكون فارغاً");
                name = value;
            }
        }
        
        public decimal Salary
        {
            get { return salary; }
            private set  // private setter - لا يمكن الوصول من الخارج
            {
                if (value < 0)
                    throw new ArgumentException("الراتب لا يمكن أن يكون سالباً");
                salary = value;
            }
        }
        
        public int YearsOfService
        {
            get { return yearsOfService; }
        }
        
        // Read-only Properties
        public DateTime HireDate => hireDate;
        public decimal AverageSalary => salaryHistory.Any() ? salaryHistory.Average() : 0;
        public decimal HighestSalary => salaryHistory.Any() ? salaryHistory.Max() : 0;
        
        // Constructor
        public EmployeeGood(string name, decimal initialSalary)
        {
            Name = name;  // سيتم التحقق هنا
            Salary = initialSalary;
            hireDate = DateTime.Now;
            yearsOfService = 0;
            salaryHistory = new List<decimal> { initialSalary };
        }
        
        // دالة محمية: إعطاء زيادة راتب
        public void GiveRaise(decimal raiseAmount)
        {
            // تحقق قوي جداً
            if (raiseAmount <= 0)
                throw new ArgumentException("الزيادة يجب أن تكون موجبة");
            
            // قد نريد حد أقصى للزيادة (مثلاً 30% زيادة فقط)
            decimal maximumRaise = Salary * 0.30m;
            if (raiseAmount > maximumRaise)
            {
                Console.WriteLine($"⚠️  تحذير: الزيادة المطلوبة ({raiseAmount}) أكبر من 30%");
                raiseAmount = maximumRaise;
            }
            
            decimal newSalary = Salary + raiseAmount;
            Salary = newSalary;
            salaryHistory.Add(newSalary);
            
            Console.WriteLine($"✅ تم إعطاء زيادة: +{raiseAmount:C} → الراتب الجديد: {Salary:C}");
        }
        
        // دالة محمية: تحديث سنوات الخدمة (مثلاً عند نهاية السنة)
        public void IncrementServiceYear()
        {
            yearsOfService++;
            Console.WriteLine($"📅 تم تحديث سنوات الخدمة إلى: {yearsOfService}");
        }
        
        // دالة للحصول على تقرير الراتب
        public string GetSalaryReport()
        {
            return $@"
📊 تقرير الراتب للموظف: {name}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
الراتب الحالي:        {Salary:C}
أعلى راتب:           {HighestSalary:C}
متوسط الراتب:        {AverageSalary:C}
تاريخ التعيين:        {HireDate:yyyy-MM-dd}
سنوات الخدمة:        {YearsOfService} سنة
عدد تعديلات الراتب:   {salaryHistory.Count - 1}
";
        }
        
        public override string ToString()
            => $"{name} - الراتب: {Salary:C} - الخدمة: {YearsOfService} سنة";
    }

        // ============================================
    // مثال 3: Student Grade System (نظام تقديرات الطلاب)
    // تطبيق حقيقي لـ Data Hiding
    // ============================================
    
    /// <summary>
    /// نظام تقديرات آمن مع إخفاء كامل للبيانات
    /// </summary>
    public class Student
    {
        // البيانات الخاصة
        private string studentId;
        private string fullName;
        private readonly List<Grade> grades;
        private const int MaxGrades = 100;  // حد أقصى للتقديرات
        
        // Properties محمية
        public string StudentId => studentId;  // read-only
        public string FullName => fullName;    // read-only
        
        // عدد التقديرات الحالي
        public int GradeCount => grades.Count;
        
        // معدل الطالب (GPA) - محسوب تلقائياً
        public decimal GPA
        {
            get
            {
                if (grades.Count == 0)
                    return 0;
                return Math.Round(grades.Average(g => g.Score), 2);
            }
        }
        
        // التقدير الكتابي (A, B, C, ...)
        public string LetterGrade
        {
            get
            {
                decimal gpa = GPA;
                return gpa switch
                {
                    >= 90 => "A (ممتاز)",
                    >= 80 => "B (جيد جداً)",
                    >= 70 => "C (جيد)",
                    >= 60 => "D (مقبول)",
                    >= 0 => "F (راسب)",
                    _ => "Invalid"
                };
            }
        }
        
        // Constructor
        public Student(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("رقم الطالب لا يمكن أن يكون فارغاً");
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("اسم الطالب لا يمكن أن يكون فارغاً");
            
            studentId = id;
            fullName = name;
            grades = new List<Grade>();
        }
        
        // إضافة تقدير
        public void AddGrade(string subject, decimal score)
        {
            // التحقق من الحد الأقصى
            if (grades.Count >= MaxGrades)
                throw new InvalidOperationException($"لا يمكن إضافة أكثر من {MaxGrades} تقدير");
            
            // التحقق من صحة التقدير
            if (score < 0 || score > 100)
                throw new ArgumentException("التقدير يجب أن يكون بين 0 و 100");
            
            grades.Add(new Grade { Subject = subject, Score = score, Date = DateTime.Now });
            Console.WriteLine($"✅ تم إضافة تقدير: {subject} = {score}");
        }
        
        // الحصول على تقديرات مادة معينة
        public List<Grade> GetGradesBySubject(string subject)
        {
            return grades.Where(g => g.Subject == subject).ToList();
        }
        
        // طباعة السجل الأكاديمي
        public void PrintTranscript()
        {
            Console.WriteLine($@"
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📚 السجل الأكاديمي للطالب
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
الاسم:          {FullName}
رقم الطالب:     {StudentId}
المعدل (GPA):   {GPA:F2}
التقدير:        {LetterGrade}
عدد التقديرات: {GradeCount}

📋 التقديرات:
");
            
            var subjectGroups = grades.GroupBy(g => g.Subject);
            foreach (var group in subjectGroups)
            {
                var avgScore = group.Average(g => g.Score);
                Console.WriteLine($"  {group.Key}: {avgScore:F1}/100");
            }
            
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
        }
        
        public override string ToString()
            => $"{FullName} (ID: {StudentId}) - GPA: {GPA:F2}";
    }
    
    // فئة مساعدة للتقديرات
    public class Grade
    {
        public string Subject { get; set; }
        public decimal Score { get; set; }
        public DateTime Date { get; set; }
    }
    
    
    // ============================================
    // مثال 4: Secret Manager - إدارة البيانات السرية
    // أمان على أعلى مستوى
    // ============================================
    
    /// <summary>
    /// مدير البيانات السرية - بدون إمكانية الوصول المباشر
    /// </summary>
    public class SecretManager
    {
        private string password;  // مخزنة بطريقة آمنة (في الواقع يجب تشفيرها)
        private readonly List<string> accessLog;
        
        public SecretManager(string initialPassword)
        {
            if (initialPassword.Length < 8)
                throw new ArgumentException("كلمة المرور يجب أن تكون 8 أحرف على الأقل");
            
            password = initialPassword;
            accessLog = new List<string>();
        }
        
        // تحقق من كلمة المرور
        public bool VerifyPassword(string attemptedPassword)
        {
            bool isCorrect = password == attemptedPassword;
            accessLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - محاولة وصول: {(isCorrect ? "✅ نجحت" : "❌ فشلت")}");
            return isCorrect;
        }
        
        // تغيير كلمة المرور (يتطلب التحقق)
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!VerifyPassword(oldPassword))
            {
                Console.WriteLine("❌ كلمة المرور القديمة خاطئة");
                return false;
            }
            
            if (newPassword.Length < 8)
            {
                Console.WriteLine("❌ كلمة المرور الجديدة قصيرة جداً");
                return false;
            }
            
            password = newPassword;
            accessLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - تم تغيير كلمة المرور");
            Console.WriteLine("✅ تم تغيير كلمة المرور");
            return true;
        }
        
        // عرض السجل (بدون كلمة المرور!)
        public void PrintAccessLog()
        {
            Console.WriteLine("\n🔐 سجل الوصول:");
            foreach (var entry in accessLog)
                Console.WriteLine($"   {entry}");
        }
    }
    
    
    // ============================================
    // Program - الاستخدام والمقارنة
    // ============================================
    
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  شرح إخفاء البيانات (Data Hiding) بتفاصيل عملية");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            // ============================================
            // 1️⃣ المقارنة بين الطريقتين
            // ============================================
            Console.WriteLine("1️⃣  مقارنة بين الطريقة الخاطئة والصحيحة:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            // الطريقة الخاطئة
            Console.WriteLine("❌ الطريقة الخاطئة (EmployeeBad):");
            var badEmployee = new EmployeeBad { Name = "أحمد", Salary = 5000 };
            Console.WriteLine($"   الاسم: {badEmployee.Name}, الراتب: {badEmployee.Salary}");
            
            // يمكننا تعديل البيانات بطريقة خاطئة!
            badEmployee.Salary = -10000;
            Console.WriteLine($"❌ بعد التعديل: الراتب = {badEmployee.Salary} (سالب! 🚨)");
            
            // الطريقة الصحيحة
            Console.WriteLine("\n✅ الطريقة الصحيحة (EmployeeGood):");
            var goodEmployee = new EmployeeGood("علي", 5000);
            Console.WriteLine($"   {goodEmployee}");
            
            // محاولة تعيين راتب سالب - سيفشل!
            try
            {
                goodEmployee.Salary = -10000;  // ❌ محاولة خاطئة
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ محاولة تعيين راتب سالب: {ex.Message}");
            }
            
            // إعطاء زيادة راتب بطريقة آمنة
            goodEmployee.GiveRaise(500);
            goodEmployee.GiveRaise(2000);  // ستحد من 30%
            Console.WriteLine(goodEmployee.GetSalaryReport());
            
            // ============================================
            // 2️⃣ نظام تقديرات الطلاب
            // ============================================
            Console.WriteLine("\n2️⃣  نظام تقديرات الطلاب:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            var student = new Student("20230001", "فاطمة محمود");
            
            // إضافة تقديرات
            student.AddGrade("رياضيات", 95);
            student.AddGrade("إنجليزي", 88);
            student.AddGrade("العلوم", 92);
            student.AddGrade("رياضيات", 90);
            student.AddGrade("إنجليزي", 85);
            
            // محاولة إضافة تقدير خاطئ
            try
            {
                student.AddGrade("تاريخ", 150);  // 150 > 100
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
            }
            
            // عرض السجل
            student.PrintTranscript();
            
            // ============================================
            // 3️⃣ مدير البيانات السرية
            // ============================================
            Console.WriteLine("\n3️⃣  مدير البيانات السرية:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            var secretManager = new SecretManager("MySecret123");
            
            // محاولة الوصول بكلمة مرور خاطئة
            Console.WriteLine("محاولة الوصول بكلمة مرور خاطئة:");
            secretManager.VerifyPassword("WrongPassword");
            
            // محاولة بكلمة صحيحة
            Console.WriteLine("محاولة الوصول بكلمة مرور صحيحة:");
            secretManager.VerifyPassword("MySecret123");
            
            // تغيير كلمة المرور
            Console.WriteLine("\nتغيير كلمة المرور:");
            secretManager.ChangePassword("MySecret123", "NewSecret456");
            secretManager.ChangePassword("MySecret123", "Wrong");  // محاولة خاطئة
            
            // عرض السجل
            secretManager.PrintAccessLog();
            
            // ============================================
            // الخلاصة
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  📊 ملخص فوائد Data Hiding:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            ✅ الأمان:
               - البيانات محمية من التعديل العشوائي
               - تحقق من الصحة عند التعديل
               
            ✅ الموثوقية:
               - لا يمكن وضع بيانات خاطئة
               - الكائن دائماً في حالة صحيحة
               
            ✅ المرونة:
               - يمكن تغيير التطبيق الداخلي
               - بدون تأثير على المستخدم
               
            ✅ سهولة الصيانة:
               - تغييرات الكود محدودة المكان
               - أسهل في البحث عن الأخطاء
               
            ✅ التحكم:
               - تحكم كامل على كيفية استخدام البيانات
               - يمكن إضافة عمليات خاصة (logging, validation, ...)
            """);
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}