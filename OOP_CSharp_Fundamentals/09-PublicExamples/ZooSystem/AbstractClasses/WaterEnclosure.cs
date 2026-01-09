namespace PublicExamples.ZooSystem.AbstractClasses
{
    public abstract class WaterEnclosure : Enclosure
    {
        public double WaterDepth { get; }
        public bool IsFiltered { get; private set; }
        public WaterEnclosure(string name, double width, double hight, bool isOpen, bool hasRoof, double waterDepth)
        : base(name, width, hight, isOpen, hasRoof)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(waterDepth, nameof(waterDepth));
            WaterDepth = waterDepth;
            IsFiltered = true;
        }
    }
}
