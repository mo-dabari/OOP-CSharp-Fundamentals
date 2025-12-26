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
}