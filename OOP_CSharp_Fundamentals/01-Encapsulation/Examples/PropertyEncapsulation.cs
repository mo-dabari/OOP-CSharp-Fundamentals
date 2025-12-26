/*
 * PropertyEncapsulation.cs
 * ============================================
 * شرح Properties الحديثة في C#
 * 
 * هذا الملف يوضح:
 * - Auto-implemented Properties (الأبسط)
 * - Properties مع Validation
 * - Computed Properties (خصائص محسوبة)
 * - Init-only Properties (C# 9+)
 * - Expression-bodied Properties
 * - Property Change Notifications
 * 
 * الفائدة: فهم الطرق المختلفة لاستخدام Properties
 * في الكود الحقيقي الاحترافي
 */

using System;
using System.Collections.Generic;

namespace Encapsulation.Examples
{
    // ============================================
    // مثال 1: Auto-implemented Properties
    // الطريقة الأبسط والأسرع
    // ============================================
    
    /// <summary>
    /// شخص بسيط باستخدام Auto Properties
    /// 
    /// هذا هو الاستخدام الشائع جداً عندما:
    /// - لا تحتاج validation
    /// - البيانات بسيطة
    /// - الأداء مهمة أقل من الوضوح
    /// </summary>
    public class PersonSimple
    {
        // أبسط طريقة: Auto Properties
        public string Name { get; }
        public int Age { get; set; }
        public string? Email { get; }
        
        public override string ToString()
            => $"الاسم: {Name}, العمر: {Age}, البريد: {Email}";
    }
    
    
    // ============================================
    // مثال 2: Properties مع Validation (الأكثر شيوعاً)
    // للبيانات الحساسة والمهمة
    // ============================================
    
    /// <summary>
    /// شخص مع تحقق من صحة البيانات
    /// 
    /// يوضح:
    /// - التحقق في الـ Setter
    /// - رسائل خطأ واضحة
    /// - الحفاظ على سلامة البيانات
    /// </summary>
    public class PersonValidated
    {
        private string name;
        private int age;
        private string email;
        
        // ✅ Property مع Validation للاسم
        public string Name
        {
            get { return name; }
            set
            {
                // التحقق من عدم كونه فارغاً
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("❌ الاسم لا يمكن أن يكون فارغاً");
                }
                
                // التحقق من الطول
                if (value.Length < 2)
                {
                    throw new ArgumentException("❌ الاسم يجب أن يكون أطول من حرف واحد");
                }
                
                // إذا كان كل شيء صحيح
                name = value;
                Console.WriteLine($"✅ تم تعيين الاسم: {value}");
            }
        }
        
        // ✅ Property مع Validation للعمر
        public int Age
        {
            get { return age; }
            set
            {
                // التحقق من أن يكون موجباً
                if (value < 0)
                {
                    throw new ArgumentException("❌ العمر لا يمكن أن يكون سالباً");
                }
                
                // التحقق من أن يكون منطقياً
                if (value > 150)
                {
                    throw new ArgumentException("❌ العمر أكبر من 150 غير منطقي");
                }
                
                age = value;
                Console.WriteLine($"✅ تم تعيين العمر: {value}");
            }
        }
        
        // ✅ Property مع Validation للبريد الإلكتروني
        public string Email
        {
            get { return email; }
            set
            {
                // التحقق من وجود @
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("❌ البريد يجب أن يحتوي على @");
                }
                
                // التحقق من وجود نقطة بعد @
                if (!value.Contains("."))
                {
                    throw new ArgumentException("❌ البريد غير صحيح");
                }
                
                email = value;
                Console.WriteLine($"✅ تم تعيين البريد: {value}");
            }
        }
        
        // Constructor
        public PersonValidated(string name, int age, string email)
        {
            Name = name;      // سيتم التحقق هنا
            Age = age;        // سيتم التحقق هنا
            Email = email;    // سيتم التحقق هنا
        }
    }
    
    
    // ============================================
    // مثال 3: Computed Properties (خصائص محسوبة)
    // قيمة محسوبة من بيانات أخرى
    // ============================================
    
    /// <summary>
    /// شخص مع خاصية محسوبة (العمر من تاريخ الميلاد)
    /// 
    /// الفائدة:
    /// - لا نخزن العمر مباشرة
    /// - العمر يُحسب تلقائياً
    /// - لا حاجة للتحديث اليدوي
    /// </summary>
    public class PersonComputed
    {
        private string name;
        private DateTime birthDate;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        
        // 🎯 Computed Property - تُحسب تلقائياً
        public int Age
        {
            get
            {
                // حساب العمر من تاريخ الميلاد
                int age = DateTime.Now.Year - birthDate.Year;
                
                // التحقق إذا كان عيد الميلاد قد مرّ أم لا هذه السنة
                if (birthDate.Date > DateTime.Now.AddYears(-age))
                    age--;
                
                return age;
            }
        }
        
        // Constructor
        public PersonComputed(string name, DateTime birthDate)
        {
            name = name;
            birthDate = birthDate;
        }
        
        public override string ToString()
            => $"{name} - تاريخ الميلاد: {birthDate:yyyy-MM-dd} - العمر: {Age}";
    }
    
    
    // ============================================
    // مثال 4: Init-only Properties (C# 9+)
    // خصائص لا يمكن تغييرها بعد الإنشاء
    // ============================================
    
    /// <summary>
    /// شخص بـ Immutable Properties
    /// 
    /// يمكن تعيين القيم فقط عند الإنشاء
    /// بعدها لا يمكن تغييرها
    /// 
    /// الفائدة:
    /// - بيانات آمنة وثابتة
    /// - منع التعديل غير المقصود
    /// - أفضل للـ Thread Safety
    /// </summary>
    public class PersonImmutable
    {
        // يمكن تعيينها فقط عند الإنشاء
        public string Name { get; init; }
        public int Age { get; init; }
        public string Email { get; init; }
        
        // Constructor
        public PersonImmutable(string name, int age, string email)
        {
            Name = name;
            Age = age;
            Email = email;
        }
        
        public override string ToString()
            => $"{Name} ({Age} سنة) - {Email}";
    }
    
    
    // ============================================
    // مثال 5: Expression-bodied Properties
    // طريقة قصيرة وأنيقة باستخدام =>
    // ============================================
    
    /// <summary>
    /// استخدام Expression-bodied Properties
    /// للخصائص البسيطة
    /// </summary>
    public class PersonExpressionBodied
    {
        private string firstName;
        private string lastName;
        private decimal salary;
        
        // Property بسيط باستخدام =>
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }
        
        // Computed Property باستخدام =>
        public string FullName => $"{firstName} {lastName}";
        
        // Property مع Validation باستخدام =>
        public decimal Salary
        {
            get => salary;
            set => salary = value >= 0 ? value : throw new ArgumentException("الراتب لا يمكن أن يكون سالباً");
        }
        
        public override string ToString() => $"{FullName} - الراتب: {Salary:C}";
    }
    
    
    // ============================================
    // مثال 6: Property Change Notification
    // إخطار عند تغيير الخاصية (للـ Binding)
    // ============================================
    
    /// <summary>
    /// شخص مع تنبيهات عند تغيير الخصائص
    /// 
    /// مفيد جداً في:
    /// - WPF Applications
    /// - Data Binding
    /// - Event-driven Applications
    /// </summary>
    public class PersonWithNotification
    {
        private string name;
        private int age;
        
        // Event للإخطار عند التغيير
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
        
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)  // فقط إذا تغيرت
                {
                    name = value;
                    // إرسال إخطار
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        
        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }
        
        // دالة مساعدة لإرسال الإخطار
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public override string ToString() => $"{name} - {age} سنة";
    }
    
    // Event Args مخصصة
    public class PropertyChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }
        
        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
    
    
    // ============================================
    // مثال 7: Property مع Backing Field Private
    // تخزين خاص مع واجهة عامة
    // ============================================
    
    /// <summary>
    /// استخدام Backing Field مع Property
    /// الطريقة الكلاسيكية والآمنة جداً
    /// </summary>
    public class BankAccount
    {
        // Backing Field - مخفي تماماً
        private decimal balance;
        private readonly List<string> transactionHistory = new();
        
        public string AccountNumber { get; }
        public string OwnerName { get; set; }
        
        // ✅ Controlled Access إلى الرصيد
        public decimal Balance
        {
            get { return balance; }
            private set  // فقط الفئة نفسها يمكنها التعديل
            {
                if (value >= 0)
                {
                    balance = value;
                }
            }
        }
        
        public BankAccount(string accountNumber, string ownerName, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                transactionHistory.Add($"إيداع: +{amount:C}");
                Console.WriteLine($"✅ تم الإيداع: {amount:C}");
            }
        }
        
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                transactionHistory.Add($"سحب: -{amount:C}");
                Console.WriteLine($"✅ تم السحب: {amount:C}");
                return true;
            }
            return false;
        }
        
        public void PrintStatement()
        {
            Console.WriteLine($"\n📊 كشف حساب {OwnerName}");
            Console.WriteLine($"رقم الحساب: {AccountNumber}");
            Console.WriteLine($"الرصيد الحالي: {Balance:C}");
            Console.WriteLine($"\n📋 السجل:");
            foreach (var trans in transactionHistory)
                Console.WriteLine($"   {trans}");
        }
    }
    
    
    // ============================================
    // Program - الاستخدام والاختبار
    // ============================================
    
}