// ❌ الطريقة الخاطئة - كود غير قابل للاختبار
namespace Untestability.BadExample
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

    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string CustomerEmail { get; set; }
        public string CreditCard { get; set; }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
    }
}
