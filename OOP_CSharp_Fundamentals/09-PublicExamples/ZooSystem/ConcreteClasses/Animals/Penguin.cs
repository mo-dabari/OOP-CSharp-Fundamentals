using Interfaces.Examples;
using PublicExamples.ZooSystem.AbstractClasses;

namespace PublicExamples.ZooSystem.ConcreteClasses.Animals
{
    public class Penguin : AbstractClasses.Bird, ISwimmable
    {
        public Penguin(string name, byte age, bool hasDanger, bool hasFeathers)
            : base(name, age, true, false) { }


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
            return 15; // K/m
        }
    }
}
