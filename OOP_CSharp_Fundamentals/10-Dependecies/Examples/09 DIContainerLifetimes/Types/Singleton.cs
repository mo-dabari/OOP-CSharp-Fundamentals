namespace DIContainerLifetimes.Types.Singleton
{
    // ====================================
    // 1️⃣ Singleton - نفس الـ Instance للكل
    // ====================================
    public class SingletonLogger : ILogger
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        public SingletonLogger()
        {
            Console.WriteLine($"[SINGLETON] Logger created: {InstanceId}");
        }

        public void Log(string message)
        {
            Console.WriteLine($"[{InstanceId}] LOG: {message}");
        }
    }

    // ✅ متى نستخدم Singleton:
    // - Logger
    // - Configuration
    // - Cache
    // - Connection Pool Manager
    // - أي شيء stateless ومشترك

    // ⚠️ تحذيرات Singleton:
    // - لازم يكون Thread-Safe
    // - تجنب Mutable State
    // - ممكن يسبب Memory Leaks لو مش منتبه
}
