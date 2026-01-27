// ====================================
// Chaining of Implicit Dependencies
// سلسلة الاعتمادات الضمنية
// ====================================

// ❌ الطريقة الخاطئة - سلسلة طويلة من Dependencies
namespace BadExample
{
    // A => B => C => D => E
    // المشكلة: A يعتمد على E بشكل ضمني!

    public class OrderController
    {
        private readonly OrderService _orderService;

        public OrderController()
        {
            _orderService = new OrderService(); // A => B
        }

        public void PlaceOrder(Order order)
        {
            _orderService.CreateOrder(order);
        }
    }

    public class OrderService
    {
        private readonly InventoryService _inventoryService;

        public OrderService()
        {
            _inventoryService = new InventoryService(); // B => C
        }

        public void CreateOrder(Order order)
        {
            if (_inventoryService.CheckStock(order.ProductId))
            {
                Console.WriteLine("Order created");
            }
        }
    }

    public class InventoryService
    {
        private readonly DatabaseService _databaseService;

        public InventoryService()
        {
            _databaseService = new DatabaseService(); // C => D
        }

        public bool CheckStock(int productId)
        {
            return _databaseService.Query($"SELECT Stock FROM Products WHERE Id = {productId}") > 0;
        }
    }

    public class DatabaseService
    {
        private readonly ConnectionManager _connectionManager;

        public DatabaseService()
        {
            _connectionManager = new ConnectionManager(); // D => E
        }

        public int Query(string sql)
        {
            var connection = _connectionManager.GetConnection();
            Console.WriteLine($"Executing: {sql}");
            return 10; // dummy result
        }
    }

    public class ConnectionManager
    {
        // ❌ هنا في مشكلة: Connection string hardcoded
        private readonly string _connectionString = "Server=localhost;Database=Shop;";

        public string GetConnection()
        {
            Console.WriteLine($"Connecting to: {_connectionString}");
            return _connectionString;
        }
    }

    // ❌ المشاكل الكارثية:
    // 1. OrderController مش عارف حاجة عن ConnectionManager
    //    لكن لو ConnectionManager فيه مشكلة، OrderController هيكراش!
    // 2. مينفعش نعمل Unit Test لـ OrderController
    //    لازم يكون عندنا Database شغال فعلياً
    // 3. لو حصل Exception في ConnectionManager
    //    الـ Stack Trace هيكون طويل جداً وصعب تتبعه
    // 4. كل كلاس في السلسلة لازم يكون شغال عشان الأول يشتغل
}
