namespace PublicExamples.ZooSystem.AbstractClasses
{
    public abstract class Bird : Animal
    {
        public bool HasFeathers { get; }
        public Bird(string name, byte age, bool hasDanger, bool hasFeathers)
            : base(name, age, hasDanger)
        {
            HasFeathers = hasFeathers;
        }
    }
}
