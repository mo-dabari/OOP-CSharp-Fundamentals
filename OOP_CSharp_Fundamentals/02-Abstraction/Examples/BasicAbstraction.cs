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

namespace OOP_CSharp_Fundamentals
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
        public string Name { get; set; }
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
    
    
    // ============================================
    // Program - الاستخدام والتوضيح
    // ============================================
    
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  مثال بسيط على Abstraction (التجريد)");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            // ============================================
            // 1️⃣ Abstract Classes
            // ============================================
            Console.WriteLine("1️⃣  استخدام Abstract Classes:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            // ❌ لا يمكننا إنشاء instance مباشر
            // var animal = new Animal();  // ❌ خطأ!
            
            // ✅ لكن نستطيع إنشاء instances من الفئات الوارثة
            Dog dog = new() { Name = "ماكس", Age = 5 };
            Cat cat = new() { Name = "ميسي", Age = 3 };
            
            Console.WriteLine("إنشاء حيوانات:");
            Console.WriteLine(dog.Describe());
            Console.WriteLine(cat.Describe());
            Console.WriteLine();
            
            Console.WriteLine("الأكل:");
            dog.Eat("لحم");
            cat.Eat("سمك");
            Console.WriteLine();
            
            Console.WriteLine("الأصوات:");
            dog.MakeSound();
            cat.MakeSound();
            Console.WriteLine();
            
            Console.WriteLine("النوم:");
            dog.Sleep();
            cat.Sleep();
            Console.WriteLine();
            
            // ============================================
            // 2️⃣ Interfaces والوراثة المتعددة
            // ============================================
            Console.WriteLine("2️⃣  واجهة واحدة بتطبيقات متعددة:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            Parrot parrot = new("الكاكاو");
            Console.WriteLine($"معلومات: {parrot.Describe()}");
            Console.WriteLine("الأصوات:");
            parrot.MakeSound();
            Console.WriteLine();
            
            Console.WriteLine("القدرات:");
            parrot.TakeOff();
            parrot.Move();
            parrot.Land();
            Console.WriteLine();
            
            // ============================================
            // 3️⃣ Polymorphism في العمل
            // ============================================
            Console.WriteLine("3️⃣  Polymorphism - معاملة مختلف الأنواع بنفس الطريقة:");
            Console.WriteLine("────────────────────────────────────────\n");
            
            AnimalCareCenter center = new();
            center.AddAnimal(dog);
            center.AddAnimal(cat);
            center.AddAnimal(new Bird { Name = "تويتي", Age = 2 });
            
            center.PrintAllInfo();
            center.MakeSounds();
            center.FeedAllAnimals();
            center.SleepTime();
            Console.WriteLine();
            
            // ============================================
            // 4️⃣ نظام النقل - Interfaces العملية
            // ============================================
            Console.WriteLine("\n4️⃣  مثال: نظام النقل (Interfaces):");
            Console.WriteLine("────────────────────────────────────────\n");
            
            // قائمة من وسائل النقل المختلفة
            List<ITransport> vehicles = new()
            {
                new Bicycle(),
                new Car(),
                new Boat()
            };
            
            Console.WriteLine("قائمة المركبات:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"\n🚀 بدء رحلة مع {vehicle.GetType().Name}:");
                vehicle.Start();
                vehicle.Move();
                vehicle.Stop();
            }
            
            // ============================================
            // الفوائد الرئيسية
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✨ فوائد التجريد:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            1️⃣  البساطة:
                - كود واضح وسهل الفهم
                - واجهات سهلة الاستخدام
            
            2️⃣  المرونة:
                - إضافة أنواع جديدة سهلة جداً
                - بدون تغيير الكود القديم
            
            3️⃣  إعادة الاستخدام:
                - نفس الكود يعمل مع أنواع مختلفة
                - لا داعي لكتابة كود متكرر
            
            4️⃣  الصيانة:
                - تغييرات محلية فقط
                - أسهل في البحث عن الأخطاء
            
            5️⃣  الأمان:
                - التفاصيل المعقدة مخفية
                - المستخدم يرى فقط ما يحتاج
            """);
            
            // ============================================
            // الفرق بين Abstract و Interface
            // ============================================
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  📊 مقارنة Abstract Class vs Interface:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            ┌──────────────────────┬──────────────────┬──────────────────┐
            │      الميزة          │ Abstract Class   │    Interface     │
            ├──────────────────────┼──────────────────┼──────────────────┤
            │ الوراثة المتعددة     │       ❌         │        ✅        │
            │ State (بيانات)       │       ✅         │        ❌        │
            │ Constructor          │       ✅         │        ❌        │
            │ Access Modifiers     │       ✅         │    محدود         │
            │ الاستخدام           │  IS-A Relations  │  Contracts       │
            └──────────────────────┴──────────────────┴──────────────────┘
            """);
            
            // ============================================
            // متى استخدم أي منهما؟
            // ============================================
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  🎯 متى تستخدم أي منهما؟:");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
            
            Console.WriteLine("""
            استخدم Abstract Class عندما:
            ✅ توجد علاقة IS-A قوية
            ✅ تريد مشاركة state (بيانات)
            ✅ تريد methods بدون public
            ✅ تريد constructors
            
            مثال: Animal (كلب IS-A حيوان)
            
            استخدم Interface عندما:
            ✅ تريد العقد فقط
            ✅ تريد وراثة متعددة
            ✅ لا تشارك state
            ✅ تركز على القدرات (capabilities)
            
            مثال: IFlying (شيء يستطيع الطيران)
            """);
            
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهى المثال");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}