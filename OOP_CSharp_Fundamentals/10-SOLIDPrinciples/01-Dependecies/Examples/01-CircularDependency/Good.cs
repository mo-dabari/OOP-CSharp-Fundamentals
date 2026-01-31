// ✅ الطريقة الصحيحة - كسر الـ Circular Dependency
namespace CircularDependency.GoodExample
{
    // الحل: استخدام Events أو Interfaces لكسر الدائرة

    // إنشاء Interface للـ Discount Calculation
    public interface IDiscountCalculator
    {
        decimal CalculateDiscount(Order order);
    }

    public class OrderService : IDiscountCalculator
    {
        private readonly PaymentService _paymentService;

        public OrderService(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void ProcessOrder(Order order)
        {
            _paymentService.ProcessPayment(order.TotalAmount, order);
        }

        public decimal CalculateDiscount(Order order)
        {
            return order.TotalAmount * 0.1m;
        }
    }

    public class PaymentService
    {
        private readonly NotificationService _notificationService;

        public PaymentService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void ProcessPayment(decimal amount, Order order)
        {
            // معالجة الدفع
            _notificationService.SendPaymentNotification(amount, order);
        }
    }

    public class NotificationService
    {
        // ✅ بدل ما نعتمد على OrderService، نعتمد على Interface
        private readonly IDiscountCalculator _discountCalculator;

        public NotificationService(IDiscountCalculator discountCalculator)
        {
            _discountCalculator = discountCalculator;
        }

        public void SendPaymentNotification(decimal amount, Order order)
        {
            var discount = _discountCalculator.CalculateDiscount(order);
            // إرسال الإشعار مع الخصم
        }
    }

    // ✅ الآن نقدر ننشئ الـ Objects بدون مشاكل!
    // var orderService = new OrderService(paymentService);
    // var paymentService = new PaymentService(notificationService);
    // var notificationService = new NotificationService(orderService); // orderService implements IDiscountCalculator
    public class Order
    {
        public decimal TotalAmount { get; set; }
    }
}

