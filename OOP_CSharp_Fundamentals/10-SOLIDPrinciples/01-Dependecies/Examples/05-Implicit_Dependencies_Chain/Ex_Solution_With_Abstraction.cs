// ✅ الطريقة الصحيحة - كسر السلسلة بـ Abstractions
namespace GoodExample
{
    // تعريف Abstractions لكل طبقة
    public interface IOrderService
    {
        void CreateOrder(Order order);
    }

    public interface IInventoryService
    {
        bool CheckStock(int productId);
    }

    public interface IDatabase
    {
        int Query(string sql);
    }

    public interface IConnectionManager
    {
        string GetConnection();
    }

    // ✅ Implementations
    public class OrderController
    {
        private readonly IOrderService _orderService;

        // ✅ يعتمد على IOrderService فقط
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void PlaceOrder(Order order)
        {
            _orderService.CreateOrder(order);
        }
    }

    public class OrderService : IOrderService
    {
        private readonly IInventoryService _inventoryService;

        // ✅ يعتمد على IInventoryService فقط
        public OrderService(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void CreateOrder(Order order)
        {
            if (_inventoryService.CheckStock(order.ProductId))
            {
                Console.WriteLine("Order created");
            }
        }
    }

    public class InventoryService : IInventoryService
    {
        private readonly IDatabase _database;

        // ✅ يعتمد على IDatabase فقط
        public InventoryService(IDatabase database)
        {
            _database = database;
        }

        public bool CheckStock(int productId)
        {
            return _database.Query($"SELECT Stock FROM Products WHERE Id = {productId}") > 0;
        }
    }

    public class DatabaseService : IDatabase
    {
        private readonly IConnectionManager _connectionManager;

        // ✅ يعتمد على IConnectionManager فقط
        public DatabaseService(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Query(string sql)
        {
            var connection = _connectionManager.GetConnection();
            Console.WriteLine($"Executing: {sql}");
            return 10;
        }
    }

    public class ConnectionManager : IConnectionManager
    {
        private readonly string _connectionString;

        // ✅ Connection string من الخارج
        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnection()
        {
            Console.WriteLine($"Connecting to: {_connectionString}");
            return _connectionString;
        }
    }

    // ✅ Composition Root - المكان الوحيد للـ wiring
    public class Program
    {
        public static void Main()
        {
            // ✅ بناء السلسلة من الآخر للأول
            var connectionString = "Server=localhost;Database=Shop;";
            var connectionManager = new ConnectionManager(connectionString);
            var database = new DatabaseService(connectionManager);
            var inventoryService = new InventoryService(database);
            var orderService = new OrderService(inventoryService);
            var orderController = new OrderController(orderService);

            // الاستخدام
            var order = new Order { ProductId = 1 };
            orderController.PlaceOrder(order);
        }
    }

    // ✅ Unit Test - سهل جداً
    public class OrderControllerTests
    {
        public void PlaceOrder_ShouldCallOrderService()
        {
            // Arrange - Mock واحد فقط
            var mockOrderService = new Mock<IOrderService>();
            var controller = new OrderController(mockOrderService.Object);
            var order = new Order { ProductId = 1 };

            // Act
            controller.PlaceOrder(order);

            // Assert
            mockOrderService.Verify(s => s.CreateOrder(order), Times.Once);

            // ✅ مش محتاجين نعرف حاجة عن InventoryService أو Database
            // ✅ مش محتاجين Database شغال
            // ✅ الاختبار سريع جداً
        }
    }

    public class OrderServiceTests
    {
        public void CreateOrder_WhenStockAvailable_ShouldCreateOrder()
        {
            // Arrange
            var mockInventory = new Mock<IInventoryService>();
            mockInventory.Setup(i => i.CheckStock(It.IsAny<int>())).Returns(true);

            var service = new OrderService(mockInventory.Object);
            var order = new Order { ProductId = 1 };

            // Act
            service.CreateOrder(order);

            // Assert
            mockInventory.Verify(i => i.CheckStock(order.ProductId), Times.Once);

            // ✅ اختبرنا OrderService بدون ما نعتمد على Database
        }
    }
}
