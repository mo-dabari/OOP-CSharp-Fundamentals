// ====================================
// 1️⃣ Constructor Injection - الطريقة المفضلة
// ====================================
namespace ConstructorInjection
{
    public interface ILogger
    {
        void Log(string message);
    }

    public interface IEmailService
    {
        void SendEmail(string to, string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;

        public EmailService(ILogger logger)
        {
            _logger = logger;
        }

        public void SendEmail(string to, string message)
        {
            _logger.Log($"Sending email to {to}");
            Console.WriteLine($"Email sent to {to}: {message}");
        }
    }

    // ✅ Constructor Injection - المفضل
    public class UserService
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        // ✅ كل الـ Dependencies واضحة في الـ Constructor
        public UserService(ILogger logger, IEmailService emailService)
        {
            // ✅ التحقق من أن الـ Dependencies مش null
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public void RegisterUser(string email)
        {
            _logger.Log($"Registering user: {email}");
            _emailService.SendEmail(email, "Welcome to our service!");
            _logger.Log("User registered successfully");
        }
    }

    // ✅ مزايا Constructor Injection:
    public class Advantages
    {
        public static void Demo()
        {
            Console.WriteLine("=== Constructor Injection Advantages ===\n");

            // 1. Dependencies واضحة من البداية
            var logger = new ConsoleLogger();
            var emailService = new EmailService(logger);
            var userService = new UserService(logger, emailService);
            // ✅ أي حد يشوف الكود يعرف الـ dependencies المطلوبة

            // 2. الـ Object دائماً في حالة صحيحة
            // ✅ مينفعش ننشئ UserService بدون dependencies
            // var invalid = new UserService(); // ❌ Compile Error

            // 3. Immutability - الـ Dependencies مش هتتغير
            // ✅ لو استخدمنا readonly، مفيش حد يقدر يغير الـ dependencies

            // 4. سهولة الاختبار
            var mockLogger = new MockLogger();
            var mockEmailService = new MockEmailService();
            var testService = new UserService(mockLogger, mockEmailService);
            testService.RegisterUser("test@example.com");

            Console.WriteLine("\n✅ Constructor Injection هو الطريقة القياسية\n");
        }
    }

    // Mock classes للاختبار
    public class MockLogger : ILogger
    {
        public List<string> Messages { get; } = new List<string>();
        public void Log(string message) => Messages.Add(message);
    }

    public class MockEmailService : IEmailService
    {
        public List<string> SentEmails { get; } = new List<string>();
        public void SendEmail(string to, string message) => SentEmails.Add(to);
    }
}
