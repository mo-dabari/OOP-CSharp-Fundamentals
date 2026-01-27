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

// ✅ Fake Implementations للاختبار
public class FakeOrderRepository : IOrderRepository
{
    public List<Order> Orders { get; } = new List<Order>
        {
            new Order { Id = 1, TotalAmount = 100, CustomerEmail = "test@example.com", Status = "Pending" }
        };

    public Order GetById(int id)
    {
        return Orders.FirstOrDefault(o => o.Id == id);
    }

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

public class EmailMessage
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

// ✅ تشغيل الاختبارات
public class Program
{
    public static void Main()
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
