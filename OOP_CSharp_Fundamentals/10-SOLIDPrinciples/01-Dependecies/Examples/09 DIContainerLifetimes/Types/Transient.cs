namespace DIContainerLifetimes.Types.Transient
{
    // ====================================
    // 3️⃣ Transient - Instance جديد مع كل استخدام
    // ====================================
    public class TransientEmailService : IEmailService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
        private readonly ILogger _logger;

        public TransientEmailService(ILogger logger)
        {
            _logger = logger;
            Console.WriteLine($"[TRANSIENT] EmailService created: {InstanceId}");
        }

        public void Send(string email, string message)
        {
            _logger.Log($"EmailService {InstanceId} sending to: {email}");
            Console.WriteLine($"[{InstanceId}] Email sent to {email}: {message}");
        }
    }

    // ✅ متى نستخدم Transient:
    // - Services خفيفة وStateless
    // - Services اللي كل Request محتاج نسخة منفصلة
    // - لما مفيش State محتاج يتشارك

    // ⚠️ تحذيرات Transient:
    // - ممكن يسبب Performance Issues لو الـ Constructor ثقيل
    // - زيادة في Memory Usage

}
