// ✅ الطريقة الصحيحة - كود قابل للاختبار
namespace GoodExample
{
    // تعريف Abstractions
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

    // ✅ OrderService يعتمد على Abstractions
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IPaymentGateway _paymentGateway;

        public OrderService(
            IOrderRepository repository,
            IEmailService emailService,
            IPaymentGateway paymentGateway)
        {
            _repository = repository;
            _emailService = emailService;
            _paymentGateway = paymentGateway;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _repository.GetById(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            var paymentResult = _paymentGateway.Charge(order.TotalAmount, order.CreditCard);

            if (paymentResult.Success)
            {
                order.Status = "Paid";
                _repository.Update(order);

                _emailService.Send(
                    order.CustomerEmail,
                    "Order Confirmation",
                    $"Your order #{orderId} has been confirmed"
                );
            }
        }
    }

    // Real Implementations للـ Production
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public SqlOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Order GetById(int id)
        {
            Console.WriteLine($"Connecting to {_connectionString}");
            return new Order { Id = id, TotalAmount = 100, CustomerEmail = "test@example.com" };
        }

        public void Update(Order order)
        {
            Console.WriteLine($"Updating order {order.Id} in database");
        }
    }

    public class SmtpEmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {
            Console.WriteLine($"Sending real email to {to}");
        }
    }

    public class StripePaymentGateway : IPaymentGateway
    {
        public PaymentResult Charge(decimal amount, string creditCard)
        {
            Console.WriteLine($"Charging ${amount} via Stripe");
            return new PaymentResult { Success = true };
        }
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
}
