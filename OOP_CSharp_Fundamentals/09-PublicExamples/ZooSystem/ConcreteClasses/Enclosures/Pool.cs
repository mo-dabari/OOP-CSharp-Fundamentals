using PublicExamples.ZooSystem.AbstractClasses;

namespace PublicExamples.ZooSystem.ConcreteClasses.Enclosures
{
    public class Pool : WaterEnclosure
    {
        public Pool(string name, double width, double hight, bool isOpen, bool hasRoof, double waterDepth)
            : base(name, width, hight, isOpen, hasRoof, waterDepth)
        {

        }

        public override void Clean()
        {
            Console.WriteLine($"Clean {Name}");
        }
    }
}
