using Interfaces.Examples;
using PublicExamples.ZooSystem.AbstractClasses;
using PublicExamples.ZooSystem.Interfaces;

namespace PublicExamples.ZooSystem.ConcreteClasses.Animals
{
    public class Elephant : Mammal, IWalkable, ISwimmable
    {
        public Elephant(string name, byte age, bool hasDanger)
            : base(name, age, false, false, true) { }


        public override void MakeSound()
        {
            Console.WriteLine($"Sound :{Name}");
        }

        public override void Eat()
        {
            Console.WriteLine($"Eat :{Name}");
        }

        public void Swim()
        {
            Console.WriteLine($"Swim :{Name}");
        }

        public int GetSwimmingSpeed()
        {
            return 7; // K/m
        }

        public void walk()
        {
            Console.WriteLine($"Walk :{Name}");
        }
    }
}
