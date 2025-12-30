namespace Polymorphism.Examples.Basic
{
    // ════════════════════════════════════════════════════════════
    // 2. METHOD OVERRIDING (Runtime Polymorphism)
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الحيوان - الأب
    /// </summary>
    public abstract class Animal
    {
        protected string name;

        public Animal(string name)
        {
            this.name = name;
        }

        // virtual - يمكن override
        public virtual void MakeSound()
        {
            Console.WriteLine($"🔊 {name} يصدر صوت عام");
        }

        // virtual - يمكن override
        public virtual void Move()
        {
            Console.WriteLine($"🚶 {name} يتحرك");
        }

        // عادي - لا يمكن override
        public void Sleep()
        {
            Console.WriteLine($"😴 {name} نائم");
        }
    }

    /// <summary>
    /// الكلب - يرث من Animal
    /// </summary>
    public class Dog : Animal
    {
        public Dog(string name) : base(name) { }

        // override المذكورة في الأب
        public override void MakeSound()
        {
            Console.WriteLine($"🐕 {name}: واف واف!");
        }

        public override void Move()
        {
            Console.WriteLine($"🐕 {name} يركض");
        }
    }

    /// <summary>
    /// القط - يرث من Animal
    /// </summary>
    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"🐱 {name}: مياو!");
        }

        public override void Move()
        {
            Console.WriteLine($"🐱 {name} يمشي بخفة");
        }
    }

    /// <summary>
    /// البطة - يرث من Animal
    /// </summary>
    public class Duck : Animal
    {
        public Duck(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"🦆 {name}: واق واق!");
        }

        public override void Move()
        {
            Console.WriteLine($"🦆 {name} يسبح");
        }
    }
}
