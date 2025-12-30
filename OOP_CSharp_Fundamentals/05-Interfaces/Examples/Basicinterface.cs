/*
 * BasicInterface.cs
 * ════════════════════════════════════════════════════════════
 * مثال بسيط لمفهوم الواجهات (Interfaces)
 *
 * يوضح:
 * - إنشاء واجهة بسيطة
 * - تطبيق واجهة واحدة
 * - الوراثة المتعددة من Interfaces
 * - Polymorphism مع Interfaces
 * - Interface Segregation Principle
 */

using System;
using System.Collections.Generic;

namespace Interfaces.Examples
{
    // ════════════════════════════════════════════════════════════
    // الواجهات الأساسية
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// واجهة حيوان - عقد بسيط
    /// </summary>
    public interface IAnimal
    {
        void MakeSound();
        string GetSpecies();
    }

    /// <summary>
    /// واجهة لأي شيء يستطيع الحركة
    /// </summary>
    public interface IMovable
    {
        void Move();
        int GetSpeed();
    }

    /// <summary>
    /// واجهة لأي شيء يستطيع الطيران
    /// </summary>
    public interface IFlying
    {
        void TakeOff();
        void Land();
        int GetAltitude();
    }

    /// <summary>
    /// واجهة للسباحة
    /// </summary>
    public interface ISwimmable
    {
        void Swim();
        int GetSwimmingSpeed();
    }


    // ════════════════════════════════════════════════════════════
    // الفئات التي تطبق الواجهات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// كلب - يطبق واجهة واحدة
    /// </summary>
    public class Dog : IAnimal, IMovable
    {
        public void MakeSound()
        {
            Console.WriteLine("🐕 الكلب: واف واف!");
        }

        public string GetSpecies()
        {
            return "كلب";
        }

        public void Move()
        {
            Console.WriteLine("🐕 الكلب يركض");
        }

        public int GetSpeed()
        {
            return 40;  // كم/س
        }
    }

    /// <summary>
    /// طائر - يطبق عدة واجهات
    /// </summary>
    public class Bird : IAnimal, IMovable, IFlying
    {
        private int altitude = 0;

        public void MakeSound()
        {
            Console.WriteLine("🐦 الطائر: تيوت تيوت!");
        }

        public string GetSpecies()
        {
            return "طائر";
        }

        public void Move()
        {
            Console.WriteLine("🐦 الطائر يطير");
        }

        public int GetSpeed()
        {
            return 50;
        }

        public void TakeOff()
        {
            altitude = 1000;
            Console.WriteLine("🐦 الطائر يرفع مستوى الارتفاع: 1000 متر");
        }

        public void Land()
        {
            altitude = 0;
            Console.WriteLine("🐦 الطائر يهبط الآن");
        }

        public int GetAltitude()
        {
            return altitude;
        }
    }

    /// <summary>
    /// سمكة - تطبق واجهات مختلفة
    /// </summary>
    public class Fish : IAnimal, IMovable, ISwimmable
    {
        public void MakeSound()
        {
            Console.WriteLine("🐠 السمكة: ...صمت...");
        }

        public string GetSpecies()
        {
            return "سمكة";
        }

        public void Move()
        {
            Console.WriteLine("🐠 السمكة تسبح");
        }

        public int GetSpeed()
        {
            return 30;
        }

        public void Swim()
        {
            Console.WriteLine("🐠 السمكة تسبح في الماء");
        }

        public int GetSwimmingSpeed()
        {
            return 25;
        }
    }

    /// <summary>
    /// بطة - تطبق 4 واجهات (حيوان + حركة + طيران + سباحة)
    /// </summary>
    public class Duck : IAnimal, IMovable, IFlying, ISwimmable
    {
        private int altitude = 0;

        public void MakeSound()
        {
            Console.WriteLine("🦆 البطة: واق واق!");
        }

        public string GetSpecies()
        {
            return "بطة";
        }

        public void Move()
        {
            Console.WriteLine("🦆 البطة تتحرك");
        }

        public int GetSpeed()
        {
            return 35;
        }

        public void TakeOff()
        {
            altitude = 500;
            Console.WriteLine("🦆 البطة ترفع إلى 500 متر");
        }

        public void Land()
        {
            altitude = 0;
            Console.WriteLine("🦆 البطة تهبط على الماء");
        }

        public int GetAltitude()
        {
            return altitude;
        }

        public void Swim()
        {
            Console.WriteLine("🦆 البطة تسبح بمهارة");
        }

        public int GetSwimmingSpeed()
        {
            return 20;
        }
    }


    // ════════════════════════════════════════════════════════════
    // فئات غير حيوانية لتوضيح استخدام Interfaces
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// سيارة - تطبق واجهات الحركة
    /// (لا تطبق IAnimal لأنها ليست حيواناً)
    /// </summary>
    public class Car : IMovable
    {
        public void Move()
        {
            Console.WriteLine("🚗 السيارة تسير على الطريق");
        }

        public int GetSpeed()
        {
            return 200;
        }
    }

    /// <summary>
    /// طائرة - تطبق واجهات الطيران والحركة
    /// </summary>
    public class Airplane : IMovable, IFlying
    {
        private int altitude = 0;

        public void Move()
        {
            Console.WriteLine("✈️  الطائرة تتحرك");
        }

        public int GetSpeed()
        {
            return 900;
        }

        public void TakeOff()
        {
            altitude = 10000;
            Console.WriteLine("✈️  الطائرة تحلق إلى 10,000 متر");
        }

        public void Land()
        {
            altitude = 0;
            Console.WriteLine("✈️  الطائرة تهبط");
        }

        public int GetAltitude()
        {
            return altitude;
        }
    }


    // ════════════════════════════════════════════════════════════
    // مدير الحيوانات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// يدير حيوانات بناءً على الواجهات
    /// </summary>
    public class Zoo
    {
        private List<IAnimal> animals = new();

        public void AddAnimal(IAnimal animal)
        {
            animals.Add(animal);
            Console.WriteLine($"✅ تم إضافة حيوان جديد");
        }

        public void MakeAllSounds()
        {
            Console.WriteLine("\n🔊 أصوات جميع الحيوانات:");
            foreach (var animal in animals)
            {
                animal.MakeSound();
            }
        }

        public void ShowAllSpecies()
        {
            Console.WriteLine("\n📋 أنواع الحيوانات:");
            foreach (var animal in animals)
            {
                Console.WriteLine($"  • {animal.GetSpecies()}");
            }
        }
    }

    /// <summary>
    /// يدير الأشياء التي تتحرك
    /// </summary>
    public class TrafficController
    {
        private List<IMovable> movables = new();

        public void AddMovable(IMovable movable)
        {
            movables.Add(movable);
        }

        public void MoveAll()
        {
            Console.WriteLine("\n🚀 جميع الأشياء تتحرك:");
            foreach (var movable in movables)
            {
                movable.Move();
            }
        }

        public void ShowSpeeds()
        {
            Console.WriteLine("\n⚡ السرعات:");
            foreach (var movable in movables)
            {
                Console.WriteLine($"  • السرعة: {movable.GetSpeed()} كم/س");
            }
        }
    }

    /// <summary>
    /// يدير الأشياء التي تطير
    /// </summary>
    public class AirTrafficControl
    {
        private List<IFlying> flyers = new();

        public void AddFlyer(IFlying flyer)
        {
            flyers.Add(flyer);
        }

        public void TakeOffAll()
        {
            Console.WriteLine("\n✈️  جميع الطائرات تحلق:");
            foreach (var flyer in flyers)
            {
                flyer.TakeOff();
                Console.WriteLine($"   الارتفاع الحالي: {flyer.GetAltitude()} متر");
            }
        }

        public void LandAll()
        {
            Console.WriteLine("\n🛬 جميع الطائرات تهبط:");
            foreach (var flyer in flyers)
            {
                flyer.Land();
            }
        }
    }
}
