// ✅ مثال آخر: تقليل السلسلة بـ Facade Pattern
using ImplicitDependenciesChain.GoodExample;

namespace ImplicitDependenciesChain.GoodExample_Facade
{
    // بدل ما يكون عندنا سلسلة طويلة
    // نعمل Facade يخفي التعقيد

    public interface IOrderFacade
    {
        void PlaceOrder(Order order);
    }

    // الـ Facade يتعامل مع كل التعقيدات الداخلية
    public class OrderFacade : IOrderFacade
    {
        private readonly IInventoryService _inventoryService;
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;
        private readonly INotificationService _notificationService;

        public OrderFacade(
            IInventoryService inventoryService,
            IPaymentService paymentService,
            IShippingService shippingService,
            INotificationService notificationService)
        {
            _inventoryService = inventoryService;
            _paymentService = paymentService;
            _shippingService = shippingService;
            _notificationService = notificationService;
        }

        public void PlaceOrder(Order order)
        {
            // التحقق من المخزون
            if (!_inventoryService.CheckStock(order.ProductId))
            {
                throw new Exception("Out of stock");
            }

            // معالجة الدفع
            _paymentService.ProcessPayment(order.TotalAmount);

            // ترتيب الشحن
            _shippingService.ArrangeShipment(order);

            // إرسال إشعار
            _notificationService.SendOrderConfirmation(order);

            Console.WriteLine("Order placed successfully");
        }
    }

    // ✅ الـ Controller بسيط جداً
    public class OrderController
    {
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public void PlaceOrder(Order order)
        {
            _orderFacade.PlaceOrder(order);
        }
    }

    // Dummy interfaces
    public interface IPaymentService
    {
        void ProcessPayment(decimal amount);
    }

    public interface IShippingService
    {
        void ArrangeShipment(Order order);
    }

    public interface INotificationService
    {
        void SendOrderConfirmation(Order order);
    }
}

public class Order
{
    public int ProductId { get; set; }
    public decimal TotalAmount { get; set; }
}
