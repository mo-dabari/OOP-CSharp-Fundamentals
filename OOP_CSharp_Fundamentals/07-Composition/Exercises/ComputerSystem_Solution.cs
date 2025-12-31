namespace Composition.Exercises
{
    public class Processor
    {
        public string Model { get; set; }

        public Processor(string model) => Model = model;

        public void Execute() => Console.WriteLine($"⚙️  معالج {Model} يعمل");
    }

    public class RAM
    {
        public int Capacity { get; set; }  // GB

        public RAM(int capacity) => Capacity = capacity;

        public void LoadData() => Console.WriteLine($"💾 تحميل من {Capacity}GB RAM");
    }

    public class Computer
    {
        private Processor processor;  // Strong Composition - ينتهي مع Computer
        private RAM ram;              // Strong Composition - ينتهي مع Computer

        public Computer(string procModel, int ramSize)
        {
            processor = new Processor(procModel);
            ram = new RAM(ramSize);
        }

        public void Boot()
        {
            Console.WriteLine("🔌 بدء الحاسب...");
            processor.Execute();
            ram.LoadData();
            Console.WriteLine("✅ الحاسب جاهز\n");
        }
    }
}
