// ✅ مثال آخر: نظام Notification
namespace GoodExample_Notification
{
    // الـ Abstraction
    public interface INotificationChannel
    {
        void Send(string message, string recipient);
    }

    // Implementations مختلفة
    public class EmailNotification : INotificationChannel
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Sending Email to {recipient}: {message}");
        }
    }

    public class SmsNotification : INotificationChannel
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Sending SMS to {recipient}: {message}");
        }
    }

    public class PushNotification : INotificationChannel
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Sending Push Notification to {recipient}: {message}");
        }
    }

    // Service يستخدم الـ channels
    public class NotificationService
    {
        private readonly IEnumerable<INotificationChannel> _channels;

        public NotificationService(IEnumerable<INotificationChannel> channels)
        {
            _channels = channels;
        }

        // إرسال عبر كل الـ channels
        public void NotifyAll(string message, string recipient)
        {
            foreach (var channel in _channels)
            {
                channel.Send(message, recipient);
            }
        }

        // إرسال عبر channel محدد
        public void Notify<TChannel>(string message, string recipient)
            where TChannel : INotificationChannel
        {
            var channel = _channels.OfType<TChannel>().FirstOrDefault();
            channel?.Send(message, recipient);
        }
    }

    // ✅ الاستخدام
    public class Program
    {
        public static void Main()
        {
            var channels = new List<INotificationChannel>
            {
                new EmailNotification(),
                new SmsNotification(),
                new PushNotification()
            };

            var notificationService = new NotificationService(channels);

            // إرسال عبر كل القنوات
            notificationService.NotifyAll("Your order is ready!", "user@example.com");

            // إرسال عبر Email فقط
            notificationService.Notify<EmailNotification>(
                "Password reset link",
                "user@example.com"
            );

            // ✅ لو عايزين نضيف WhatsApp؟
            // نعمل WhatsAppNotification : INotificationChannel
            // ونسجلها في الـ DI Container
            // بدون تعديل على NotificationService!
        }
    }
}

public class Order
{
    public decimal TotalAmount { get; set; }
}
