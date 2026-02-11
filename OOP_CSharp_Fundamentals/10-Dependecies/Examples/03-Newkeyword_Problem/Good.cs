using System;
using System.Collections.Generic;

namespace NewkeywordProblem.GoodExample
{
    // Domain Model
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
    }

    // ✅ Abstractions
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

    // ✅ Implementations
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public SqlOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(Order order)
        {
            Console.WriteLine($"Saving order {order.Id} to {_connectionString}");
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
            Console.WriteLine($"Sending confirmation email for order {order.Id} via {_smtpServer}");
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

    // ✅ OrderService - يعتمد على Abstractions فقط
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public OrderService(
            IOrderRepository repository,
            IEmailService emailService,
            ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void CreateOrder(Order order)
        {
            try
            {
                _repository.Save(order);
                _emailService.SendOrderConfirmation(order);
                _logger.Log($"Order {order.Id} created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create order: {ex.Message}");
                throw;
            }
        }
    }

    // ✅ Composition Root - Manual DI
    public class Program
    {
        public static void Main2()
        {
            // إنشاء الـ dependencies
            var connectionString = "Server=localhost;Database=Orders;";
            var smtpServer = "smtp.gmail.com";

            IOrderRepository repository = new SqlOrderRepository(connectionString);
            IEmailService emailService = new EmailService(smtpServer);
            ILogger logger = new ConsoleLogger();

            // حقن الـ dependencies
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
}

// ✅ Unit Tests - استخدام مكتبة Moq الحقيقية
// Install-Package Moq
/*
using Moq;
using Xunit;

namespace NewkeywordProblem.GoodExample.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_ShouldSaveAndSendEmail()
        {
            // Arrange
            var mockRepository = new Mock<IOrderRepository>();
            var mockEmailService = new Mock<IEmailService>();
            var mockLogger = new Mock<ILogger>();

            var service = new OrderService(
                mockRepository.Object,
                mockEmailService.Object,
                mockLogger.Object
            );

            var order = new Order { Id = 1, ProductName = "Test", TotalAmount = 100 };

            // Act
            service.CreateOrder(order);

            // Assert
            mockRepository.Verify(r => r.Save(order), Times.Once);
            mockEmailService.Verify(e => e.SendOrderConfirmation(order), Times.Once);
            mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_WhenRepositoryFails_ShouldLogError()
        {
            // Arrange
            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(r => r.Save(It.IsAny<Order>()))
                         .Throws(new Exception("Database error"));

            var mockEmailService = new Mock<IEmailService>();
            var mockLogger = new Mock<ILogger>();

            var service = new OrderService(
                mockRepository.Object,
                mockEmailService.Object,
                mockLogger.Object
            );

            var order = new Order { Id = 1 };

            // Act & Assert
            Assert.Throws<Exception>(() => service.CreateOrder(order));
            mockLogger.Verify(l => l.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}
*/

// ✅ استخدام DI Container
// Install-Package Microsoft.Extensions.DependencyInjection
/*
using Microsoft.Extensions.DependencyInjection;

namespace NewkeywordProblem.GoodExample
{
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

            // ✅ تسجيل OrderService نفسه
            services.AddScoped<OrderService>();

            var serviceProvider = services.BuildServiceProvider();

            // الـ Container يحقن الـ dependencies تلقائياً
            var orderService = serviceProvider.GetRequiredService<OrderService>();

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
*/
