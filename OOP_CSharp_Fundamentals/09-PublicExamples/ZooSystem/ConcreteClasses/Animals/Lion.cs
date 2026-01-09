using PublicExamples.ZooSystem.AbstractClasses;
using PublicExamples.ZooSystem.Interfaces;

namespace PublicExamples.ZooSystem.ConcreteClasses.Animals
{
    public class Lion : Mammal, IWalkable
    {
        public Lion(string name, byte age, bool hasDanger)
            : base(name, age, true, true, true) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Sound :{Name}");
        }
        public override void Eat()
        {
            Console.WriteLine($"Eat :{Name}");
        }
        public void walk()
        {
            Console.WriteLine($"Walk :{Name}");
        }
    }
}
