// ====================================
// Untestability - صعوبة الاختبار
// ====================================

// ❌ الطريقة الخاطئة - كود غير قابل للاختبار
namespace BadExample
{
    public class OrderService
    {
        public void ProcessOrder(int orderId)
        {
            // ❌ مرتبط مباشرة بـ Concrete Classes
            var repository = new SqlOrderRepository();
            var emailService = new SmtpEmailService();
            var paymentGateway = new StripePaymentGateway();

            // جلب الطلب
            var order = repository.GetById(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // معالجة الدفع
            var paymentResult = paymentGateway.Charge(order.TotalAmount, order.CreditCard);

            if (paymentResult.Success)
            {
                order.Status = "Paid";
                repository.Update(order);

                // إرسال إيميل
                emailService.Send(
                    order.CustomerEmail,
                    "Order Confirmation",
                    $"Your order #{orderId} has been confirmed"
                );
            }
        }
    }

    public class SqlOrderRepository
    {
        public Order GetById(int id)
        {
            // ❌ يحتاج اتصال حقيقي بـ Database
            Console.WriteLine("Connecting to SQL Server...");
            Console.WriteLine("SELECT * FROM Orders WHERE Id = " + id);
            return new Order { Id = id, TotalAmount = 100, CustomerEmail = "test@example.com" };
        }

        public void Update(Order order)
        {
            Console.WriteLine("UPDATE Orders SET Status = 'Paid' WHERE Id = " + order.Id);
        }
    }

    public class SmtpEmailService
    {
        public void Send(string to, string subject, string body)
        {
            // ❌ يحتاج SMTP Server شغال
            Console.WriteLine($"Connecting to SMTP Server smtp.gmail.com:587...");
            Console.WriteLine($"Sending email to {to}");
        }
    }

    public class StripePaymentGateway
    {
        public PaymentResult Charge(decimal amount, string creditCard)
        {
            // ❌ يحتاج اتصال حقيقي بـ Stripe API
            Console.WriteLine($"Connecting to Stripe API...");
            Console.WriteLine($"Charging ${amount} to card {creditCard}");
            return new PaymentResult { Success = true };
        }
    }

    // ❌ محاولة عمل Unit Test - فاشلة!
    public class OrderServiceTests
    {
        public void ProcessOrder_ShouldUpdateOrderStatus()
        {
            // ❌ المشاكل:
            // 1. محتاجين SQL Server شغال
            // 2. محتاجين SMTP Server شغال
            // 3. محتاجين Stripe API شغال (أو Test Mode)
            // 4. الاختبار بطيء جداً (I/O operations)
            // 5. لو أي service فشل، الاختبار هيفشل
            // 6. ممكن نخصم فلوس حقيقية من بطاقة!

            var service = new OrderService();

            // هيفشل لأننا مش متصلين بـ Database حقيقي
            // service.ProcessOrder(1);

            Console.WriteLine("Cannot test this code without real dependencies!");
        }
    }
}
