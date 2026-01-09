namespace PublicExamples.ZooSystem.AbstractClasses
{
    public abstract class Mammal : Animal
    {
        public bool HasFur { get; }
        public bool HasGiveBirth { get; }

        public Mammal(string name, byte age, bool hasDanger, bool hasFur, bool hasGiveBirth)
            : base(name, age, hasDanger)
        {
            HasFur = hasFur;
            HasGiveBirth = hasGiveBirth;
        }
    }
}
