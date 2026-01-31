// ====================================
// Circular Dependency - مشكلة الاعتماد الدائري
// ====================================

// ❌ الطريقة الخاطئة - Circular Dependency
// المشكلة: A يعتمد على B، B يعتمد على C، C يعتمد على A
namespace CircularDependency.BadExample
{
    public class OrderService
    {
        private readonly PaymentService _paymentService;

        public OrderService(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void ProcessOrder(Order order)
        {
            // معالجة الطلب
            _paymentService.ProcessPayment(order.TotalAmount);
        }

        public decimal CalculateDiscount(Order order)
        {
            // حساب الخصم
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

        public void ProcessPayment(decimal amount)
        {
            // معالجة الدفع
            _notificationService.SendPaymentNotification(amount);
        }
    }

    public class NotificationService
    {
        private readonly OrderService _orderService; // ❌ رجعنا لـ OrderService تاني!

        public NotificationService(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void SendPaymentNotification(decimal amount)
        {
            // إرسال إشعار
            // ولو احتجنا نحسب الخصم؟
            var discount = _orderService.CalculateDiscount(new Order { TotalAmount = amount });
        }
    }

    // ❌ المشكلة: مينفعش نعمل Instance من أي كلاس!
    // var orderService = new OrderService(paymentService); // محتاج paymentService
    // var paymentService = new PaymentService(notificationService); // محتاج notificationService
    // var notificationService = new NotificationService(orderService); // محتاج orderService
    // --> البيضة ولا الفرخة؟
}
