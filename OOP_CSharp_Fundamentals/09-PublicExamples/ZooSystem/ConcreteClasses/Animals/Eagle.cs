namespace PublicExamples.ZooSystem.ConcreteClasses.Animals
{
    public class Eagle : AbstractClasses.Bird, IFlyable
    {
        public Eagle(string name, byte age)
            : base(name, age, true, true) { }


        public override void MakeSound()
        {
            Console.WriteLine($"Sound :{Name}");
        }

        public override void Eat()
        {
            Console.WriteLine($"Eat :{Name}");
        }

        public void Fly()
        {
            Console.WriteLine($"Fly :{Name}");
        }
    }
}
