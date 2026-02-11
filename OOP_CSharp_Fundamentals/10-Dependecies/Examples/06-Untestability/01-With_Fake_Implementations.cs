namespace Untestability.Good.Fakes
{

    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IEmailService _email;
        private readonly IPaymentGateway _payment;

        public OrderService(
            IOrderRepository repo,
            IEmailService email,
            IPaymentGateway payment)
        {
            _repo = repo;
            _email = email;
            _payment = payment;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _repo.GetById(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var result = _payment.Charge(order.TotalAmount, order.CreditCard);
            if (!result.Success) return;

            order.Status = "Paid";
            _repo.Update(order);
            _email.Send(
                    order.CustomerEmail,
                    "Order Confirmation",
                    $"Your order #{orderId} has been confirmed"
                );
        }
    }

    // Abstractions
    public interface IOrderRepository
    {
        Order GetById(int id);
        void Update(Order order);
    }

    public interface IEmailService
    {
        void Send(string to, string subject, string body);
    }

    public interface IPaymentGateway
    {
        PaymentResult Charge(decimal amount, string creditCard);
    }


    // Fakes
    // ✅ Fake Implementations للاختبار
    public class FakeOrderRepository : IOrderRepository
    {
        public List<Order> Orders = new()
        {
            new Order { Id = 1, Status = "Pending", TotalAmount = 100 }
        };

        public Order GetById(int id) => Orders.FirstOrDefault(o => o.Id == id);

        public void Update(Order order)
        {
            var existing = Orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null)
            {
                existing.Status = order.Status;
            }
        }
    }

    public class FakeEmailService : IEmailService
    {
        public List<EmailMessage> EmailsSent { get; } = new List<EmailMessage>();

        public void Send(string to, string subject, string body)
        {
            EmailsSent.Add(new EmailMessage { To = to, Subject = subject, Body = body });
            Console.WriteLine($"[FAKE] Email sent to {to}");
        }
    }

    public class FakePaymentGateway : IPaymentGateway
    {
        public bool ShouldFail { get; set; }

        public PaymentResult Charge(decimal amount, string creditCard)
        {
            Console.WriteLine($"[FAKE] Charging ${amount}");
            return new PaymentResult { Success = !ShouldFail };
        }
    }

    // Models
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerEmail { get; set; }
        public string CreditCard { get; set; }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
    }


    // ✅ Unit Tests - سريعة وموثوقة
    public class OrderServiceTests
    {
        public void ProcessOrder_WhenOrderExists_ShouldProcessSuccessfully()
        {
            // Arrange - إنشاء Fake Implementations
            var fakeRepository = new FakeOrderRepository();
            var fakeEmailService = new FakeEmailService();
            var fakePaymentGateway = new FakePaymentGateway();

            var service = new OrderService(fakeRepository, fakeEmailService, fakePaymentGateway);

            // Act
            service.ProcessOrder(1);

            // Assert
            var order = fakeRepository.Orders.First(o => o.Id == 1);
            if (order.Status != "Paid")
            {
                throw new Exception("Order status should be Paid");
            }

            if (!fakeEmailService.EmailsSent.Any())
            {
                throw new Exception("Email should be sent");
            }

            Console.WriteLine("✅ Test Passed: Order processed successfully");
        }

        public void ProcessOrder_WhenOrderNotFound_ShouldThrowException()
        {
            // Arrange
            var fakeRepository = new FakeOrderRepository();
            var fakeEmailService = new FakeEmailService();
            var fakePaymentGateway = new FakePaymentGateway();

            var service = new OrderService(fakeRepository, fakeEmailService, fakePaymentGateway);

            // Act & Assert
            try
            {
                service.ProcessOrder(999); // order لا يوجد
                throw new Exception("Should have thrown exception");
            }
            catch (Exception ex)
            {
                if (ex.Message != "Order not found")
                {
                    throw;
                }
                Console.WriteLine("✅ Test Passed: Exception thrown for non-existent order");
            }
        }

        public void ProcessOrder_WhenPaymentFails_ShouldNotUpdateOrder()
        {
            // Arrange
            var fakeRepository = new FakeOrderRepository();
            var fakeEmailService = new FakeEmailService();
            var fakePaymentGateway = new FakePaymentGateway { ShouldFail = true };

            var service = new OrderService(fakeRepository, fakeEmailService, fakePaymentGateway);

            // Act
            service.ProcessOrder(1);

            // Assert
            var order = fakeRepository.Orders.First(o => o.Id == 1);
            if (order.Status == "Paid")
            {
                throw new Exception("Order status should not be Paid when payment fails");
            }

            if (fakeEmailService.EmailsSent.Any())
            {
                throw new Exception("Email should not be sent when payment fails");
            }

            Console.WriteLine("✅ Test Passed: Order not updated when payment fails");
        }
    }

    // ✅ تشغيل الاختبارات
    public class Program
    {
        public static void Main6()
        {
            var tests = new OrderServiceTests();

            Console.WriteLine("Running Unit Tests...\n");

            tests.ProcessOrder_WhenOrderExists_ShouldProcessSuccessfully();
            Console.WriteLine();

            tests.ProcessOrder_WhenOrderNotFound_ShouldThrowException();
            Console.WriteLine();

            tests.ProcessOrder_WhenPaymentFails_ShouldNotUpdateOrder();
            Console.WriteLine();

            Console.WriteLine("All tests completed!");
            Console.WriteLine("\n✅ المزايا:");
            Console.WriteLine("- الاختبارات سريعة جداً (بدون I/O)");
            Console.WriteLine("- مفيش اتصالات خارجية مطلوبة");
            Console.WriteLine("- نقدر نتحكم في السيناريوهات (Success/Failure)");
            Console.WriteLine("- اختبارات موثوقة ومستقرة");
        }
    }
}
