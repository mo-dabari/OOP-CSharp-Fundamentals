/*
 * BasicAbstraction.cs
 * ============================================
 * مثال بسيط وسهل الفهم لمفهوم التجريد (Abstraction)
 * 
 * هذا الملف يوضح:
 * - الفرق بين Abstract Classes و Interfaces
 * - كيفية استخدام Abstract Methods
 * - Polymorphism العملي
 * - الفوائد الحقيقية للتجريد
 * 
 * التشبيه: نوع من السيارات - الواجهة واحدة (مقود، دواسات)
 * لكن التطبيق مختلف (بنزين، كهربائية، هجين)
 */

using System;
using System.Collections.Generic;

namespace Abstraction.Examples
{
    // ============================================
    // مثال 1: Abstract Class (الفئة المجردة)
    // ============================================
    
    /// <summary>
    /// فئة مجردة تمثل حيوان عام
    /// 
    /// لا يمكننا إنشاء instance مباشر:
    /// var animal = new Animal();  // ❌ خطأ!
    /// 
    /// لكن يمكننا استخدامها كـ base class
    /// var animal = new Dog();  // ✅ صحيح!
    /// </summary>
    public abstract class Animal
    {
        // خاصية عامة - كل الحيوانات لديها اسم
        public string? Name { get; }
        public int Age { get; set; }
        
        // 🔴 Abstract Method - بدون تطبيق
        // يجب على كل حيوان وارث أن يطبقها
        public abstract void MakeSound();
        
        // 🔵 Abstract Method آخر
        public abstract string Describe();
        
        // 🟢 Normal Method - لها تطبيق كامل
        public void Sleep()
        {
            Console.WriteLine($"😴 {Name} نائم...");
        }
        
        // Method عام يمكن override
        public virtual void Eat(string food)
        {
            Console.WriteLine($"🍽️  {Name} يأكل {food}");
        }
    }
    
    /// <summary>
    /// كلب - يرث من Animal
    /// يجب تطبيق جميع الـ abstract methods
    /// </summary>
    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine($"🐕 {Name}: واف واف! 🐶");
        }
        
        public override string Describe()
        {
            return $"كلب باسم {Name} عمره {Age} سنة";
        }
        
        // يمكن أيضاً override الـ normal methods
        public override void Eat(string food)
        {
            Console.WriteLine($"🐕 {Name} يأكل بسرعة: {food}");
        }
    }
    
    /// <summary>
    /// قطة - وارثة أخرى من Animal
    /// </summary>
    public class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine($"🐱 {Name}: مياو مياو! 😸");
        }
        
        public override string Describe()
        {
            return $"قطة باسم {Name} عمرها {Age} سنة";
        }
        
        public override void Eat(string food)
        {
            Console.WriteLine($"🐱 {Name} تأكل بأناقة: {food}");
        }
    }
    
    /// <summary>
    /// طائر - وارثة أخرى
    /// </summary>
    public class Bird : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine($"🦅 {Name}: تيوت تيوت! 🐦");
        }
        
        public override string Describe()
        {
            return $"طائر باسم {Name} عمره {Age} سنة";
        }
    }
    
    
    // ============================================
    // مثال 2: Interface (الواجهة)
    // ============================================
    
    /// <summary>
    /// واجهة - عقد فقط
    /// تحدد الخدمات التي يجب أن تقدمها الفئة المنفذة
    /// </summary>
    public interface IAnimal
    {
        // خصائص
        string Name { get; set; }
        
        // methods
        void MakeSound();
        string Describe();
        void Sleep();
    }
    
    /// <summary>
    /// واجهة أخرى - تقدرة الطيران
    /// لاحظ: يمكن implement عدة interfaces!
    /// </summary>
    public interface IFlying
    {
        void TakeOff();
        void Land();
        int GetFlightSpeed();
    }
    
    /// <summary>
    /// طائر يطبق Interfaces بدلاً من Abstract Class
    /// </summary>
    public class Parrot : IAnimal, IFlying
    {
        public string Name { get; set; }
        
        public Parrot(string name)
        {
            Name = name;
        }
        
        // تطبيق IAnimal
        public void MakeSound()
        {
            Console.WriteLine($"🦜 {Name}: هلا! مرحبا! 🦜");
        }
        
        public string Describe()
        {
            return $"ببغاء ذكي باسم {Name}";
        }
        
        public void Sleep()
        {
            Console.WriteLine($"😴 {Name} (الببغاء) نائم");
        }
        
        // تطبيق IFlying
        public void TakeOff()
        {
            Console.WriteLine($"🦜 {Name} يطير: بووووم! 🚀");
        }
        
        public void Land()
        {
            Console.WriteLine($"🦜 {Name} يهبط بخفة");
        }
        
        public int GetFlightSpeed()
        {
            return 80;  // كم/ساعة
        }
    }
    
    
    // ============================================
    // مثال 3: مقارنة - نظام نقل
    // ============================================
    
    /// <summary>
    /// واجهة لأي وسيلة نقل
    /// </summary>
    public interface ITransport
    {
        void Start();
        void Stop();
        void Move();
    }
    
    /// <summary>
    /// دراجة - تطبيق بسيط
    /// </summary>
    public class Bicycle : ITransport
    {
        public void Start()
        {
            Console.WriteLine("🚴 الدراجة جاهزة!");
        }
        
        public void Move()
        {
            Console.WriteLine("🚴 أركب الدراجة بقوتي!");
        }
        
        public void Stop()
        {
            Console.WriteLine("🛑 توقفت الدراجة");
        }
    }
    
    /// <summary>
    /// سيارة - تطبيق أكثر تعقيداً
    /// </summary>
    public class Car : ITransport
    {
        public void Start()
        {
            Console.WriteLine("🚗 صوت المحرك: برووووم!");
            Console.WriteLine("✅ المحرك بدأ بالعمل");
        }
        
        public void Move()
        {
            Console.WriteLine("🚗 السيارة تتحرك بسرعة!");
        }
        
        public void Stop()
        {
            Console.WriteLine("🛑 السيارة توقفت");
        }
    }
    
    /// <summary>
    /// قارب - تطبيق مختلف تماماً
    /// </summary>
    public class Boat : ITransport
    {
        public void Start()
        {
            Console.WriteLine("⛵ تم تشغيل المحرك البحري");
        }
        
        public void Move()
        {
            Console.WriteLine("⛵ القارب ينطلق على الماء!");
        }
        
        public void Stop()
        {
            Console.WriteLine("⛵ القارب توقف في الميناء");
        }
    }
    
    
    // ============================================
    // مثال 4: Polymorphism العملي
    // ============================================
    
    /// <summary>
    /// فئة توضح قوة Polymorphism
    /// نستطيع التعامل مع جميع الحيوانات بنفس الطريقة
    /// بدون معرفة نوعها الحقيقي!
    /// </summary>
    public class AnimalCareCenter
    {
        private List<Animal> animals = new();
        
        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
            Console.WriteLine($"✅ تمت إضافة: {animal.Describe()}");
        }
        
        public void FeedAllAnimals()
        {
            Console.WriteLine("\n🍽️  وقت الأكل!");
            foreach (var animal in animals)
            {
                // نفس الكود، لكن كل حيوان يأكل بطريقته الخاصة!
                animal.Eat("طعام صحي");
            }
        }
        
        public void MakeSounds()
        {
            Console.WriteLine("\n🔊 أصوات الحيوانات:");
            foreach (var animal in animals)
            {
                // نفس الدالة، لكن صوت مختلف لكل حيوان!
                animal.MakeSound();
            }
        }
        
        public void SleepTime()
        {
            Console.WriteLine("\n😴 وقت النوم!");
            foreach (var animal in animals)
            {
                animal.Sleep();  // جميع الحيوانات تنام بنفس الطريقة
            }
        }
        
        public void PrintAllInfo()
        {
            Console.WriteLine("\n📋 معلومات الحيوانات:");
            foreach (var animal in animals)
            {
                Console.WriteLine($"  • {animal.Describe()}");
            }
        }
    }

}