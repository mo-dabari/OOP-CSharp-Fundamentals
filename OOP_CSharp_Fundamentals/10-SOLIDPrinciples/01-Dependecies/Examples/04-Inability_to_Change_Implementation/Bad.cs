// ====================================
// Inability to Change Implementation
// عدم القدرة على تغيير التنفيذ
// ====================================

// ❌ الطريقة الخاطئة - Implementation محدد ومش قابل للتغيير
namespace InabilitytoChangeImplementation.BadExample
{
    // مثال: نظام دفع في متجر إلكتروني
    public class PaymentService
    {
        // ❌ مرتبط مباشرة بـ Visa Payment
        public void ProcessPayment(decimal amount, string cardNumber)
        {
            Console.WriteLine($"Processing payment of {amount} using Visa");

            // Visa-specific logic
            if (cardNumber.StartsWith("4"))
            {
                Console.WriteLine("Valid Visa card");
                // معالجة الدفع عبر Visa API
                ChargeVisaCard(amount, cardNumber);
            }
            else
            {
                throw new Exception("Invalid Visa card");
            }
        }

        private void ChargeVisaCard(decimal amount, string cardNumber)
        {
            // Visa API Call
            Console.WriteLine("Charging Visa card...");
        }
    }

    public class CheckoutService
    {
        private readonly PaymentService _paymentService;

        public CheckoutService()
        {
            _paymentService = new PaymentService();
        }

        public void Checkout(Order order, string cardNumber)
        {
            _paymentService.ProcessPayment(order.TotalAmount, cardNumber);
        }
    }

    // ❌ المشاكل:
    // 1. لو العميل عايز يدفع بـ Mastercard؟ --> لازم نعدل PaymentService
    // 2. لو عايزين نضيف PayPal؟ --> لازم نعدل PaymentService تاني
    // 3. كل تعديل ممكن يكسر كود شغال
    // 4. انتهاك مبدأ Open/Closed Principle
}
