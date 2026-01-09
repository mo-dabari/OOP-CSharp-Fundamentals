using System;
using System.Collections.Generic;

namespace PublicExamples.ZooSystem.AbstractClasses
{
    public abstract class Animal
    {
        public string Name { get; }
        public byte Age { get; }
        public bool HasDanger { get; }

        public Animal(string name, byte age, bool hasDanger)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(age);
            Name = name;
            Age = age;
            HasDanger = hasDanger;
        }

        public abstract void MakeSound();
        public abstract void Eat();

        public virtual string DisplayInfo()
        {
            return $"Name Animal:{Name}\nAge:{Age}";
        }
    }
}
