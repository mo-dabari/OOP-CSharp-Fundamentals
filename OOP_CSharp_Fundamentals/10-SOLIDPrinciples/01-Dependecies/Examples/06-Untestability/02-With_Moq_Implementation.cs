using System;

// ✅ مثال متقدم: استخدام Moq Library

namespace Untestability.Good.Moq
{
    // في الواقع، نستخدم Mocking Framework مثل Moq
    // بدل ما نكتب Fake Implementations يدوياً

    public class OrderServiceTests
    {
        public void ProcessOrder_UsingMoq_ShouldWork()
        {
            // Arrange - استخدام Mock بدل Fake
            var mockRepository = new Mock<IOrderRepository>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPaymentGateway = new Mock<IPaymentGateway>();

            // Setup the mock behavior
            var order = new Order { Id = 1, TotalAmount = 100, CustomerEmail = "test@example.com" };
            mockRepository.Setup(r => r.GetById(1)).Returns(order);
            mockPaymentGateway.Setup(p => p.Charge(100, It.IsAny<string>()))
                .Returns(new PaymentResult { Success = true });

            var service = new OrderService(
                mockRepository.Object,
                mockEmailService.Object,
                mockPaymentGateway.Object
            );

            // Act
            service.ProcessOrder(1);

            // Assert - Verify الـ interactions
            mockRepository.Verify(r => r.Update(It.Is<Order>(o => o.Status == "Paid")), Times.Once);
            mockEmailService.Verify(e => e.Send(
                "test@example.com",
                "Order Confirmation",
                It.IsAny<string>()
            ), Times.Once);

            Console.WriteLine("✅ Test with Moq passed!");
        }
    }

    // نفس Abstractions
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
            var result = _payment.Charge(order.TotalAmount, order.CreditCard);

            if (!result.Success) return;
            _email.Send(
                    order.CustomerEmail,
                    "Order Confirmation",
                    $"Your order #{orderId} has been confirmed"
                );
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerEmail { get; set; }
        public string CreditCard { get; set; }
        public string Status { get; set; }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
    }


    // ❌ Dummy Mock class
    public class Mock<T> where T : class
    {
        public T Object { get; }
        public Mock<T> Setup(Func<T, object> expression) => this;
        public Mock<T> Returns(object value) => this;
        public void Verify(Action<T> expression, Times times) { }
    }

    public class Times
    {
        public static Times Once => new Times();
    }
    public class It
    {
        public static T IsAny<T>() => default(T);
        public static T Is<T>(Func<T, bool> predicate) => default(T);
    }
}
