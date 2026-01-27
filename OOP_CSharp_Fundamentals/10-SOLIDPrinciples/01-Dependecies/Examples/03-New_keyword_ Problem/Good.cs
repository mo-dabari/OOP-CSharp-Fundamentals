// ✅ الطريقة الصحيحة - Dependency Injection
namespace GoodExample
{
    // تعريف Abstractions
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public interface IEmailService
    {
        void SendOrderConfirmation(Order order);
    }

    public interface ILogger
    {
        void Log(string message);
        void LogError(string message);
    }

    // Implementations
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public SqlOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(Order order)
        {
            Console.WriteLine($"Saving to {_connectionString}");
        }
    }

    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;

        public EmailService(string smtpServer)
        {
            _smtpServer = smtpServer;
        }

        public void SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"Sending email via {_smtpServer}");
        }
    }

    public class ConsoleLogger : ILogger
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

    // ✅ OrderService يستقبل كل الـ dependencies من الخارج
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        // ✅ Constructor Injection - كل الـ dependencies واضحة
        public OrderService(
            IOrderRepository repository,
            IEmailService emailService,
            ILogger logger)
        {
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
        }

        public void CreateOrder(Order order)
        {
            try
            {
                // ✅ مفيش new في Business Logic
                _repository.Save(order);
                _emailService.SendOrderConfirmation(order);
                _logger.Log("Order created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

    // ✅ Composition Root - المكان الوحيد اللي فيه new
    public class Program
    {
        public static void Main()
        {
            // ✅ إنشاء الـ dependencies هنا فقط
            var connectionString = "Server=localhost;Database=Orders;";
            var smtpServer = "smtp.gmail.com";

            var repository = new SqlOrderRepository(connectionString);
            var emailService = new EmailService(smtpServer);
            var logger = new ConsoleLogger();

            // ✅ Inject الـ dependencies
            var orderService = new OrderService(repository, emailService, logger);

            // استخدام الـ service
            var order = new Order
            {
                Id = 1,
                ProductName = "Laptop",
                TotalAmount = 1000
            };

            orderService.CreateOrder(order);
        }
    }

    // ✅ Unit Test - سهل جداً
    public class OrderServiceTests
    {
        public void CreateOrder_ShouldSaveAndSendEmail()
        {
            // Arrange - إنشاء Mocks
            var mockRepository = new Mock<IOrderRepository>();
            var mockEmailService = new Mock<IEmailService>();
            var mockLogger = new Mock<ILogger>();

            var service = new OrderService(
                mockRepository.Object,
                mockEmailService.Object,
                mockLogger.Object
            );

            var order = new Order { Id = 1 };

            // Act
            service.CreateOrder(order);

            // Assert
            mockRepository.Verify(r => r.Save(order), Times.Once);
            mockEmailService.Verify(e => e.SendOrderConfirmation(order), Times.Once);
            mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
        }
    }

    // ✅ أو باستخدام DI Container (مثل Microsoft.Extensions.DependencyInjection)
    public class ProgramWithDIContainer
    {
        public static void Main()
        {
            // إنشاء DI Container
            var services = new ServiceCollection();

            // تسجيل الـ dependencies
            services.AddScoped<IOrderRepository>(sp =>
                new SqlOrderRepository("Server=localhost;Database=Orders;"));
            services.AddScoped<IEmailService>(sp =>
                new EmailService("smtp.gmail.com"));
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddScoped<OrderService>();

            var serviceProvider = services.BuildServiceProvider();

            // ✅ الـ Container هيحقن الـ dependencies تلقائياً
            var orderService = serviceProvider.GetService<OrderService>();

            var order = new Order
            {
                Id = 1,
                ProductName = "Laptop",
                TotalAmount = 1000
            };

            orderService.CreateOrder(order);
        }
    }
}

// Dummy Mock class للتوضيح
public class Mock<T> where T : class
{
    public T Object { get; }
    public void Verify(Action<T> expression, Times times) { }
}

public class Times
{
    public static Times Once => new Times();
}

public class It
{
    public static T IsAny<T>() => default(T);
}

public class ServiceCollection : List<object>
{
    public void AddScoped<TInterface, TImplementation>() where TImplementation : TInterface { }
    public void AddScoped<TInterface>(Func<object, TInterface> factory) { }
    public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface { }
    public object BuildServiceProvider() => null;
}

public class Order
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal TotalAmount { get; set; }
}
