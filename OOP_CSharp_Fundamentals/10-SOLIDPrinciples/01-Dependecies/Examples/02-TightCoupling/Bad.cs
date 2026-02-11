// ====================================
// Tight Coupling - الارتباط المحكم
// ====================================

// ❌ الطريقة الخاطئة - Tight Coupling
namespace TightCoupling.BadExample
{
    public class OrderService
    {
        // ❌ مرتبط مباشرة بـ SqlRepository
        private readonly SqlOrderRepository _repository;

        public OrderService()
        {
            _repository = new SqlOrderRepository(); // ❌ استخدام new
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

    public class SqlOrderRepository
    {
        public void Save(Order order)
        {
            // حفظ في SQL Database
            Console.WriteLine("Saving to SQL Database...");
        }

        public Order GetById(int id)
        {
            // استرجاع من SQL Database
            return new Order { Id = id };
        }
    }

    // ❌ المشاكل:
    // 1. لو عايز أغير من SQL لـ MongoDB؟ --> لازم أعدل OrderService
    // 2. مينفعش أعمل Unit Test --> محتاج SQL Database شغال
    // 3. مينفعش أستخدم OrderService في أكثر من context

    public class Order
    {
        public int Id { get; set; }
    }
}
