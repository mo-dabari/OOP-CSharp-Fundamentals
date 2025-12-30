/*
 * DependencyInjection.cs
 * ════════════════════════════════════════════════════════════
 * مثال متقدم: Dependency Injection مع Interfaces
 *
 * يوضح:
 * - كيفية استخدام Interfaces للـ DI
 * - فصل الواجهات عن التطبيق
 * - السهولة في تغيير التطبيق
 * - اختبار بسيط مع Mock Objects
 *
 * الفائدة الكبرى: تغيير التطبيق بدون تغيير الكود الرئيسي
 */

using System;
using System.Collections.Generic;

namespace Interfaces.Examples
{
    // ════════════════════════════════════════════════════════════
    // الواجهات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// واجهة تخزين البيانات
    /// </summary>
    public interface IUserRepository
    {
        User GetUserById(int id);
        void SaveUser(User user);
        void DeleteUser(int id);
        List<User> GetAllUsers();
    }

    /// <summary>
    /// واجهة إرسال البريد
    /// </summary>
    public interface IEmailSender
    {
        void SendEmail(string to, string subject, string body);
        bool IsConnected();
    }

    /// <summary>
    /// واجهة التسجيل
    /// </summary>
    public interface ILogger
    {
        void Log(string message);
        void LogError(string error);
    }

    /// <summary>
    /// واجهة التشفير
    /// </summary>
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }


    // ════════════════════════════════════════════════════════════
    // نموذج البيانات
    // ════════════════════════════════════════════════════════════

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, الاسم: {Name}, البريد: {Email}";
        }
    }


    // ════════════════════════════════════════════════════════════
    // التطبيقات (Implementations)
    // ════════════════════════════════════════════════════════════

    // --- تطبيقات Repository ---

    /// <summary>
    /// تخزين في الذاكرة (للاختبار)
    /// </summary>
    public class InMemoryUserRepository : IUserRepository
    {
        private List<User> users = new();

        public User GetUserById(int id)
        {
            Console.WriteLine($"💾 البحث في الذاكرة عن المستخدم {id}");
            return users.Find(u => u.Id == id);
        }

        public void SaveUser(User user)
        {
            Console.WriteLine($"💾 حفظ المستخدم في الذاكرة: {user.Name}");
            var existing = users.Find(u => u.Id == user.Id);
            if (existing != null)
                users.Remove(existing);
            users.Add(user);
        }

        public void DeleteUser(int id)
        {
            Console.WriteLine($"💾 حذف المستخدم {id} من الذاكرة");
            users.RemoveAll(u => u.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }
    }

    /// <summary>
    /// تخزين في قاعدة بيانات SQL
    /// </summary>
    public class SqlUserRepository : IUserRepository
    {
        public User GetUserById(int id)
        {
            Console.WriteLine($"🔍 البحث في SQL عن المستخدم {id}");
            // محاكاة الاستعلام
            return new User { Id = id, Name = "أحمد", Email = "ahmed@test.com" };
        }

        public void SaveUser(User user)
        {
            Console.WriteLine($"💾 حفظ في قاعدة SQL: {user.Name}");
        }

        public void DeleteUser(int id)
        {
            Console.WriteLine($"🗑️  حذف من SQL: {id}");
        }

        public List<User> GetAllUsers()
        {
            Console.WriteLine("📂 جلب جميع المستخدمين من SQL");
            return new List<User>();
        }
    }

    /// <summary>
    /// تخزين في MongoDB
    /// </summary>
    public class MongoUserRepository : IUserRepository
    {
        public User GetUserById(int id)
        {
            Console.WriteLine($"🍃 البحث في MongoDB عن {id}");
            return new User { Id = id, Name = "فاطمة", Email = "fatima@test.com" };
        }

        public void SaveUser(User user)
        {
            Console.WriteLine($"🍃 حفظ في MongoDB: {user.Name}");
        }

        public void DeleteUser(int id)
        {
            Console.WriteLine($"🍃 حذف من MongoDB: {id}");
        }

        public List<User> GetAllUsers()
        {
            Console.WriteLine("📂 جلب من MongoDB");
            return new List<User>();
        }
    }

    // --- تطبيقات Email Sender ---

    /// <summary>
    /// إرسال بريد حقيقي
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"📧 إرسال بريد SMTP");
            Console.WriteLine($"   إلى: {to}");
            Console.WriteLine($"   الموضوع: {subject}");
        }

        public bool IsConnected()
        {
            Console.WriteLine("🌐 التحقق من اتصال SMTP");
            return true;
        }
    }

    /// <summary>
    /// إرسال بريد وهمي (للاختبار)
    /// </summary>
    public class MockEmailSender : IEmailSender
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"🧪 [TEST] إرسال بريد وهمي إلى {to}");
        }

        public bool IsConnected()
        {
            Console.WriteLine("🧪 [TEST] اتصال وهمي = true");
            return true;
        }
    }

    // --- تطبيقات Logger ---

    /// <summary>
    /// تسجيل في Console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"📝 [LOG] {message}");
        }

        public void LogError(string error)
        {
            Console.WriteLine($"❌ [ERROR] {error}");
        }
    }

    /// <summary>
    /// تسجيل في ملف
    /// </summary>
    public class FileLogger : ILogger
    {
        private List<string> logs = new();

        public void Log(string message)
        {
            logs.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            Console.WriteLine($"📄 [FILE] {message}");
        }

        public void LogError(string error)
        {
            logs.Add($"[{DateTime.Now:HH:mm:ss}] ERROR: {error}");
            Console.WriteLine($"📄 [FILE] ERROR: {error}");
        }

        public void PrintLogs()
        {
            Console.WriteLine("\n📋 السجلات:");
            foreach (var log in logs)
                Console.WriteLine($"  {log}");
        }
    }

    // --- تطبيقات Encryption ---

    /// <summary>
    /// تشفير بسيط
    /// </summary>
    public class SimpleEncryption : IEncryptionService
    {
        public string Encrypt(string plainText)
        {
            Console.WriteLine($"🔐 تشفير بسيط: {plainText}");
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        public string Decrypt(string encryptedText)
        {
            Console.WriteLine($"🔓 فك تشفير بسيط");
            return System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(encryptedText));
        }
    }

    /// <summary>
    /// تشفير متقدم
    /// </summary>
    public class AdvancedEncryption : IEncryptionService
    {
        public string Encrypt(string plainText)
        {
            Console.WriteLine($"🔐 تشفير متقدم (AES): {plainText}");
            return "🔒ENCRYPTED_DATA_STRONG";
        }

        public string Decrypt(string encryptedText)
        {
            Console.WriteLine($"🔓 فك تشفير متقدم (AES)");
            return "DECRYPTED_DATA";
        }
    }


    // ════════════════════════════════════════════════════════════
    // الخدمة الرئيسية (UserService) - تستقبل Dependencies
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// خدمة المستخدمين - مثال على Dependency Injection
    ///
    /// لاحظ: لا تنشئ الـ dependencies نفسها
    ///       بل تستقبلها من الخارج (Constructor Injection)
    /// </summary>
    public class UserService
    {
        // جميع الاعتماديات
        private readonly IUserRepository userRepository;
        private readonly IEmailSender emailSender;
        private readonly ILogger logger;
        private readonly IEncryptionService encryption;

        // Constructor Injection
        public UserService(
            IUserRepository repository,
            IEmailSender emailSender,
            ILogger logger,
            IEncryptionService encryption)
        {
            userRepository = repository;
            this.emailSender = emailSender;
            this.logger = logger;
            this.encryption = encryption;
        }

        public void RegisterUser(User user)
        {
            logger.Log($"تسجيل مستخدم جديد: {user.Name}");

            // تشفير كلمة المرور
            user.Password = encryption.Encrypt(user.Password);

            // حفظ المستخدم
            userRepository.SaveUser(user);

            // إرسال بريد ترحيب
            if (emailSender.IsConnected())
            {
                emailSender.SendEmail(
                    user.Email,
                    "مرحباً بك",
                    "شكراً على التسجيل");
                logger.Log($"تم إرسال بريد ترحيب لـ {user.Email}");
            }
            else
            {
                logger.LogError("لا يمكن إرسال البريد");
            }
        }

        public User GetUser(int id)
        {
            logger.Log($"جلب المستخدم {id}");
            return userRepository.GetUserById(id);
        }

        public void DeleteUser(int id)
        {
            logger.Log($"حذف المستخدم {id}");
            userRepository.DeleteUser(id);
        }

        public void PrintAllUsers()
        {
            var users = userRepository.GetAllUsers();
            Console.WriteLine($"\n📋 جميع المستخدمين ({users.Count}):");
            foreach (var user in users)
                Console.WriteLine($"  • {user}");
        }
    }
}
