// ✅ الطريقة الصحيحة - Proper Encapsulation
namespace InappropriateIntimacy.OrderCustomer.GoodExample
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; private set; }
        public decimal FinalAmount => TotalAmount - DiscountAmount;

        // ✅ Order يطلب Discount فقط، مش يحسبه بنفسه
        public void ApplyDiscount(decimal discountAmount)
        {
            if (discountAmount < 0 || discountAmount > TotalAmount)
            {
                throw new ArgumentException("Invalid discount amount");
            }

            DiscountAmount = discountAmount;
        }
    }

    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        private bool _isVIP;
        private int _loyaltyPoints;
        private int _creditScore;
        private decimal _creditLimit;
        private readonly List<int> _orderHistory;

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
            _orderHistory = new List<int>();
        }

        // ✅ Customer يتحكم في حالته الخاصة
        public bool IsVIP() => _isVIP;

        public int GetLoyaltyPoints() => _loyaltyPoints;

        // ✅ Customer مسؤول عن إضافة Loyalty Points
        public void AddLoyaltyPoints(int points)
        {
            if (points <= 0)
            {
                throw new ArgumentException("Points must be positive");
            }

            _loyaltyPoints += points;
        }

        // ✅ Customer يحسب الخصم الخاص به
        public decimal CalculateDiscountForOrder(decimal orderAmount)
        {
            decimal discountPercentage = 0m;

            if (_isVIP)
            {
                discountPercentage += 0.10m; // 10% for VIP
            }

            if (_orderHistory.Count > 10)
            {
                discountPercentage += 0.05m; // 5% for loyal customers
            }

            return orderAmount * discountPercentage;
        }

        // ✅ Customer يتحقق من Credit Eligibility
        public bool IsEligibleForCredit()
        {
            return _creditScore > 700;
        }

        public void IncreaseCreditLimit(decimal amount)
        {
            if (!IsEligibleForCredit())
            {
                throw new InvalidOperationException("Customer is not eligible for credit increase");
            }

            _creditLimit += amount;
        }

        public void AddOrderToHistory(int orderId)
        {
            _orderHistory.Add(orderId);
        }
    }

    // ✅ Service للتنسيق بين Order و Customer
    public class OrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public void ProcessOrder(Order order)
        {
            // جلب Customer
            var customer = _customerRepository.GetById(order.CustomerId);

            // ✅ Customer يحسب الخصم الخاص به
            var discount = customer.CalculateDiscountForOrder(order.TotalAmount);

            // ✅ Order يستقبل الخصم
            order.ApplyDiscount(discount);

            // حفظ Order
            _orderRepository.Save(order);

            // ✅ Customer يضيف النقاط بنفسه
            if (customer.IsVIP())
            {
                customer.AddLoyaltyPoints(100);
            }

            // ✅ Customer يضيف Order للتاريخ
            customer.AddOrderToHistory(order.Id);

            // حفظ التغييرات
            _customerRepository.Update(customer);
        }
    }

    public interface ICustomerRepository
    {
        Customer GetById(int id);
        void Update(Customer customer);
    }

    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
