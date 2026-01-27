// ====================================
// ✅ الحل 2: Reverse Dependency
// ====================================
namespace GoodExample_ReverseDependency
{
    // في Clean Architecture:
    // بدل ما UI يعتمد على Data Layer مباشرة
    // UI و Data كلهم يعتمدوا على Abstraction

    // Domain Layer (Core) - مفيهوش أي dependencies
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int id);
    }

    // Application Layer - يعتمد على Domain فقط
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void CreateOrder(Order order)
        {
            _repository.Save(order);
        }
    }

    // Infrastructure Layer - يعتمد على Domain ويطبق الـ Interface
    public class SqlOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            // SQL Implementation
        }

        public Order GetById(int id)
        {
            return new Order { Id = id };
        }
    }

    // Presentation Layer - يعتمد على Application
    public class OrderController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void CreateOrder(Order order)
        {
            _orderService.CreateOrder(order);
        }
    }

    // ✅ الاتجاه:
    // Presentation --> Application --> Domain <-- Infrastructure
    // Infrastructure يعتمد على Domain (عكس الاتجاه التقليدي)
}
