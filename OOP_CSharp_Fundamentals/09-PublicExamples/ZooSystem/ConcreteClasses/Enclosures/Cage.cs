using PublicExamples.ZooSystem.AbstractClasses;

namespace PublicExamples.ZooSystem.ConcreteClasses.Enclosures
{
    public class Cage : Enclosure
    {
        public Cage(string name, double width, double hight, bool isOpen, bool hasRoof)
            : base(name, width, hight, isOpen, hasRoof) { }

        public override void Clean()
        {
            Console.WriteLine($"Clean {Name}");
        }
    }
}
