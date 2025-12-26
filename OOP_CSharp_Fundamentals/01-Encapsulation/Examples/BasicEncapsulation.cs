/*
 * BasicEncapsulation.cs
 * ============================================
 * شرح الكبسولة بأبسط صورة
 * 
 * المفاهيم:
 * - private vs public
 * - getters و setters (Properties)
 * - التحقق من الصحة (Validation)
 * 
 * الهدف: فهم الفرق بين الكود الخاطئ والصحيح
 */

using System;

namespace Encapsulation.Examples
{
    // ============================================
    // ❌ الطريقة الخاطئة - لا تفعل هذا!
    // ============================================
    public class PersonBad
    {
        // 🚨 المشكلة: البيانات معرضة للتعديل من أي مكان
        public string Name;
        public int Age;
        public decimal Salary;
        
        public PersonBad(string name, int age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
    }
    
    // ============================================
    // ✅ الطريقة الصحيحة - افعل هذا!
    // ============================================
    public class PersonGood
    {
        // 🔒 الخطوة 1: اجعل البيانات خاصة (Private)
        private string name;
        private int age;
        private decimal salary;
        
        // Constructor
        public PersonGood(string name, int age, decimal salary)
        {
            // استخدم Properties للتحقق عند الإنشاء
            Name = name;
            Age = age;
            Salary = salary;
        }
        
        // 🔑 الخطوة 2: وفر Properties مع Validation
        
        /// <summary>
        /// الحصول على أو تعيين الاسم
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                // التحقق من الصحة
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("❌ خطأ: الاسم لا يمكن أن يكون فارغاً");
                    return;
                }
                
                if (value.Length < 2)
                {
                    Console.WriteLine("❌ خطأ: الاسم يجب أن يكون أطول من حرف واحد");
                    return;
                }
                
                name = value;
                Console.WriteLine($"✅ تم تعيين الاسم: {value}");
            }
        }
        
        /// <summary>
        /// الحصول على أو تعيين العمر
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                // التحقق من الصحة
                if (value < 0)
                {
                    Console.WriteLine("❌ خطأ: العمر لا يمكن أن يكون سالباً");
                    return;
                }
                
                if (value > 150)
                {
                    Console.WriteLine("❌ خطأ: العمر غير منطقي (أكثر من 150)");
                    return;
                }
                
                age = value;
                Console.WriteLine($"✅ تم تعيين العمر: {value}");
            }
        }
        
        /// <summary>
        /// الحصول على أو تعيين الراتب
        /// </summary>
        public decimal Salary
        {
            get
            {
                return salary;
            }
            set
            {
                // التحقق من الصحة
                if (value < 0)
                {
                    Console.WriteLine("❌ خطأ: الراتب لا يمكن أن يكون سالباً");
                    return;
                }
                
                salary = value;
                Console.WriteLine($"✅ تم تعيين الراتب: {value:C}");
            }
        }
        
        // دالة مساعدة لعرض البيانات
        public void DisplayInfo()
        {
            Console.WriteLine($"\n📋 معلومات الموظف:");
            Console.WriteLine($"   الاسم: {name}");
            Console.WriteLine($"   العمر: {age} سنة");
            Console.WriteLine($"   الراتب: {salary:C}");
        }
    }
    
    // ============================================
    // الاستخدام والاختبار
    // ============================================
    class Program
    {
        static void Main()
        {
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine("  شرح الكبسولة (Encapsulation) بـ C#");
            Console.WriteLine("═══════════════════════════════════════════════════\n");
            
            // ============================================
            // ❌ مثال 1: الطريقة الخاطئة
            // ============================================
            Console.WriteLine("❌ المثال الأول - الطريقة الخاطئة:");
            Console.WriteLine("────────────────────────────────────");
            
            var badPerson = new PersonBad("أحمد", 30, 5000);
            Console.WriteLine($"الاسم: {badPerson.Name}");
            Console.WriteLine($"العمر: {badPerson.Age}");
            Console.WriteLine($"الراتب: {badPerson.Salary}\n");
            
            // المشكلة: يمكننا تغيير القيم لأي شيء بدون تحقق!
            Console.WriteLine("🚨 المشكلة: يمكننا القيام بأشياء خاطئة:");
            badPerson.Age = -50;  // ❌ عمر سالب!
            Console.WriteLine($"   تم تعيين العمر إلى: {badPerson.Age} (خطأ!)");
            
            badPerson.Salary = -1000;  // ❌ راتب سالب!
            Console.WriteLine($"   تم تعيين الراتب إلى: {badPerson.Salary} (خطأ!)\n");
            
            // ============================================
            // ✅ مثال 2: الطريقة الصحيحة
            // ============================================
            Console.WriteLine("\n✅ المثال الثاني - الطريقة الصحيحة:");
            Console.WriteLine("────────────────────────────────────");
            
            var goodPerson = new PersonGood("محمد", 28, 4500);
            goodPerson.DisplayInfo();
            
            // محاولة تعيين قيم صحيحة
            Console.WriteLine("\n👤 محاولة تعيين قيم صحيحة:");
            goodPerson.Name = "علي";
            goodPerson.Age = 32;
            goodPerson.Salary = 6000;
            
            goodPerson.DisplayInfo();
            
            // محاولة تعيين قيم خاطئة
            Console.WriteLine("\n❌ محاولة تعيين قيم خاطئة:");
            goodPerson.Name = "";              // سيتم الرفض
            goodPerson.Age = -5;               // سيتم الرفض
            goodPerson.Salary = -1000;         // سيتم الرفض
            goodPerson.Age = 200;              // سيتم الرفض
            
            // ============================================
            // مقارنة الطريقتين
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════");
            Console.WriteLine("  📊 مقارنة الطريقتين:");
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("الطريقة الخاطئة (PersonBad):");
            Console.WriteLine("  ❌ لا توجد حماية للبيانات");
            Console.WriteLine("  ❌ لا يوجد تحقق من الصحة");
            Console.WriteLine("  ❌ البيانات معرضة للأخطاء");
            Console.WriteLine("  ❌ صعوبة الصيانة والتطوير");
            Console.WriteLine();
            Console.WriteLine("الطريقة الصحيحة (PersonGood):");
            Console.WriteLine("  ✅ بيانات محمية (Private)");
            Console.WriteLine("  ✅ تحقق من الصحة عند التعديل");
            Console.WriteLine("  ✅ بيانات موثوقة وآمنة");
            Console.WriteLine("  ✅ سهلة الصيانة والتطوير");
            Console.WriteLine();
            
            // ============================================
            // النقاط الرئيسية
            // ============================================
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine("  💡 النقاط الرئيسية:");
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("1. استخدم 'private' لإخفاء البيانات");
            Console.WriteLine("   ❌ public string Name;");
            Console.WriteLine("   ✅ private string name;");
            Console.WriteLine();
            Console.WriteLine("2. وفر 'Properties' للوصول الآمن");
            Console.WriteLine("   public string Name");
            Console.WriteLine("   {");
            Console.WriteLine("       get { return name; }");
            Console.WriteLine("       set { /* تحقق من الصحة */ }");
            Console.WriteLine("   }");
            Console.WriteLine();
            Console.WriteLine("3. أضف 'Validation' في الـ Setters");
            Console.WriteLine("   if (value > 0 && value < 150)");
            Console.WriteLine("       age = value;");
            Console.WriteLine();
            
            // ============================================
            // اختبار نهائي
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════");
            Console.WriteLine("  🎯 اختبار نهائي:");
            Console.WriteLine("═══════════════════════════════════════════════════\n");
            
            Console.WriteLine("إنشاء موظف جديد بقيم صحيحة:");
            var employee = new PersonGood("فاطمة", 26, 5500);
            
            Console.WriteLine("\nمحاولة تحديث البيانات:");
            employee.Name = "نور";
            employee.Age = 27;
            employee.Salary = 6200;
            
            Console.WriteLine("\nمحاولة تعيين قيم خاطئة:");
            employee.Name = "";         // سيتم رفضه
            employee.Age = 170;         // سيتم رفضه
            employee.Salary = -100;     // سيتم رفضه
            
            employee.DisplayInfo();
            
            Console.WriteLine("\n═══════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════\n");
        }
    }
}