using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.Examples
{
    public abstract class Animal
    {

        public string Name {get;}
        public byte Age {get;}

        public Animal(string name , byte age)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if(name.Length > 15)
                throw new InvalidOperationException("Must Be Name Animal Larger then 15 Characters");

            if(age < 1 || age > 20)
                throw new InvalidOperationException("Average the Age animal between 1 Day to 20 Years");

            Name = name;
            Age = age;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine($"{name} يصدر صوت");
        }

        public virtual void Move()
        {
            Console.WriteLine($"{name} يتحرك");
        }

        public virtual void Eat()
        {
            Console.WriteLine($"{name} يأكل");
        }

        public virtual string GetInfo()
        {
            return $"{name} ({age} سنة)";
        }
    }


    public class Dog : Animal
    {

        public Dog(string name , byte age) :base(name,age){} 

        public override void MakeSound()
        {
            Console.WriteLine("واف واف");
        }

        public override void Move()
        {
            Console.WriteLine("يركض");
        }

        public override void Eat()
        {
            Console.WriteLine($"🐕 {name} يأكل لحم");
        }

        public void Fetch()
        {
            Console.WriteLine($"🐕 {name} يجلب الكرة");
        }
    }


    public class Cat : Animal
    {
        public Cat(string name , byte age) :base(name,age){} 

        public override void MakeSound()
        {
            Console.WriteLine($"🐱 {name}: مياو!");
        }

        public override void Move()
        {
            Console.WriteLine($"🐱 {name} يمشي بخفة");
        }

        public override void Eat()
        {
            Console.WriteLine($"🐱 {name} يأكل سمك");        
        }
        public void Scratch()
        {
            Console.WriteLine($"🐱 {name} يخدش الأثاث");
        }
    }


    public class Bird : Animal
    {

        public Bird(string name , byte age) :base(name,age){} 
        public override void MakeSound()
        {
            Console.WriteLine($"🐦 {name}: تيوت تيوت!");
        }

        public override void Move()
        {
            Console.WriteLine($"🐦 {name} يطير في السماء");
        }
        public override void Eat()
        {
            Console.WriteLine($"🐦 {name} يأكل البذور");
        }
        public void BuildNest()
        {
            Console.WriteLine($"🐦 {name} يبني عش");
        }
    }


    public class AnimalSanctuary
    {
        private readonly List<Animal> _animals = new();
        public IReadOnlyList values;

        public AnimalSanctuary()
        {
            values = _animals;
        }
        public void AddAnimal(Animal animal)
        {
            ArgumentException.ThrowIfNullOrEmpty(animal);

            _animals.Add(animal);
            Console.WriteLine($"✅ تم إضافة {animal.GetInfo()}");
        }
        public void  MakeAllSounds()
        {
            Console.WriteLine("\n🔊 أصوات جميع الحيوانات:");
            foreach(Animal animal in values)
            {
                Console.WriteLine(animal.MakeSound());
            }
        }
        public void MoveAll()
        {
            Console.WriteLine("\n🏃 جميع الحيوانات تتحرك:");
            foreach(Animal animal in values)
            {
                Console.WriteLine(animal.Move());
            }
        }

        public void FeedAll()
        {
            Console.WriteLine("\n🍽️  إطعام جميع الحيوانات:");
            foreach (var animal in animals)
                animal.Eat();
        }
       public void PrintAnimalInfo()
        {
            Console.WriteLine("\n📋 معلومات الحيوانات:");
            foreach (var animal in animals)
                Console.WriteLine($"  • {animal.GetInfo()}");
        }
    }


}