// ====================================
// استخدام new في المكان الخطأ
// ====================================

// ❌ الطريقة الخاطئة - استخدام new في Business Logic
namespace BadExample
{
    public class OrderService
    {
        public void CreateOrder(Order order)
        {
            // ❌ إنشاء dependencies داخل الـ method
            var repository = new SqlOrderRepository();
            var emailService = new EmailService();
            var loggerService = new LoggerService();

            try
            {
                // حفظ الطلب
                repository.Save(order);

                // إرسال إيميل
                emailService.SendOrderConfirmation(order);

                // تسجيل العملية
                loggerService.Log("Order created successfully");
            }
            catch (Exception ex)
            {
                loggerService.LogError(ex.Message);
                throw;
            }
        }
    }

    public class SqlOrderRepository
    {
        // ❌ Connection string هنا hardcoded
        private readonly string _connectionString = "Server=localhost;Database=Orders;";

        public void Save(Order order)
        {
            Console.WriteLine($"Saving to {_connectionString}");
        }
    }

    public class EmailService
    {
        // ❌ SMTP settings هنا hardcoded
        private readonly string _smtpServer = "smtp.gmail.com";

        public void SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"Sending email via {_smtpServer}");
        }
    }

    public class LoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"LOG: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
    }

    // ❌ المشاكل:
    // 1. مينفعش نعمل Unit Test --> محتاجين SQL و SMTP شغالين
    // 2. مينفعش نغير Implementation --> لازم نعدل OrderService
    // 3. كل مرة ننادي CreateOrder هننشئ Objects جديدة --> Performance
    // 4. Settings hardcoded --> مش flexible
    // 5. OrderService بيعرف تفاصيل كل الـ dependencies --> Inappropriate Intimacy
}
