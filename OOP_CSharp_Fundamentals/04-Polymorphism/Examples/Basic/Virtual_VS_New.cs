namespace Polymorphism.Examples.Basic
{
    // ════════════════════════════════════════════════════════════
    // 4. EXAMPLE - الفرق بين Virtual و New
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// مثال على الفرق بين virtual و new
    /// </summary>
    public class Person
    {
        public virtual void Greet()
        {
            Console.WriteLine("🙋 مرحبا، أنا شخص عادي");
        }
    }

    public class Doctor : Person
    {
        // ✅ صحيح - override
        public override void Greet()
        {
            Console.WriteLine("🏥 مرحبا، أنا طبيب");
        }
    }

    public class Engineer : Person
    {
        // ❌ خطأ - new (shadowing)
        public new void Greet()
        {
            Console.WriteLine("⚙️  مرحبا، أنا مهندس");
        }
    }
}
