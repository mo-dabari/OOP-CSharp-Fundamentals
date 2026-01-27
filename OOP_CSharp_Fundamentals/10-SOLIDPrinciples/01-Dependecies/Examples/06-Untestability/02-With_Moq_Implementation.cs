// ✅ مثال متقدم: استخدام Moq Library
namespace GoodExample_WithMoq
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

            var service = new GoodExample.OrderService(
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

    // Dummy Mock class
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
