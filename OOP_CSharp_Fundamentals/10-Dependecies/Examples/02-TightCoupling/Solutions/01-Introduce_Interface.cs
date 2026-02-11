// ====================================
// ✅ الحل 1: Introduce Interface
// ====================================
namespace GoodExample_IntroduceInterface
{
    // إنشاء Abstraction
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int id);
    }

    // Implementation للـ SQL
    public class SqlOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine("Saving to SQL Database...");
        }

        public Order GetById(int id)
        {
            return new Order { Id = id };
        }
    }

    // Implementation للـ MongoDB
    public class MongoOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine("Saving to MongoDB...");
        }

        public Order GetById(int id)
        {
            return new Order { Id = id };
        }
    }

    // ✅ OrderService يعتمد على Abstraction مش Implementation
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        // ✅ Constructor Injection
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void CreateOrder(Order order)
        {
            _repository.Save(order);
        }

        public Order GetOrder(int id)
        {
            return _repository.GetById(id);
        }
    }

    // ✅ الاستخدام:
    // var sqlService = new OrderService(new SqlOrderRepository());
    // var mongoService = new OrderService(new MongoOrderRepository());

    // ✅ في الـ Unit Test:
    // var mockRepo = new Mock<IOrderRepository>();
    // var service = new OrderService(mockRepo.Object);

    public class Order
    {
        public int Id { get; set; }
    }
}
