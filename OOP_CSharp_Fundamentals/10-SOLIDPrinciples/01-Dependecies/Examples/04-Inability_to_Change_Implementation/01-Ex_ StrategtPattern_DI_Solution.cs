// ✅ الطريقة الصحيحة - Strategy Pattern + DI
namespace InabilitytoChangeImplementation.GoodExample_Strategy
{
    // تعريف Abstraction للـ Payment
    public interface IPaymentStrategy
    {
        void ProcessPayment(decimal amount, PaymentDetails details);
        bool CanProcess(PaymentDetails details);
    }

    // تفاصيل الدفع
    public class PaymentDetails
    {
        public string CardNumber { get; set; }
        public string PayPalEmail { get; set; }
        public string WalletId { get; set; }
    }

    // ✅ Implementation لـ Visa
    public class VisaPaymentStrategy : IPaymentStrategy
    {
        public bool CanProcess(PaymentDetails details)
        {
            return !string.IsNullOrEmpty(details.CardNumber)
                   && details.CardNumber.StartsWith("4");
        }

        public void ProcessPayment(decimal amount, PaymentDetails details)
        {
            Console.WriteLine($"Processing {amount} via Visa");
            Console.WriteLine($"Card: {details.CardNumber}");
            // Visa API logic
        }
    }

    // ✅ Implementation لـ Mastercard
    public class MastercardPaymentStrategy : IPaymentStrategy
    {
        public bool CanProcess(PaymentDetails details)
        {
            return !string.IsNullOrEmpty(details.CardNumber)
                   && details.CardNumber.StartsWith("5");
        }

        public void ProcessPayment(decimal amount, PaymentDetails details)
        {
            Console.WriteLine($"Processing {amount} via Mastercard");
            Console.WriteLine($"Card: {details.CardNumber}");
            // Mastercard API logic
        }
    }

    // ✅ Implementation لـ PayPal
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        public bool CanProcess(PaymentDetails details)
        {
            return !string.IsNullOrEmpty(details.PayPalEmail);
        }

        public void ProcessPayment(decimal amount, PaymentDetails details)
        {
            Console.WriteLine($"Processing {amount} via PayPal");
            Console.WriteLine($"Email: {details.PayPalEmail}");
            // PayPal API logic
        }
    }

    // ✅ Payment Service يستخدم الاستراتيجيات
    public class PaymentService
    {
        private readonly IEnumerable<IPaymentStrategy> _strategies;

        // ✅ حقن كل الاستراتيجيات المتاحة
        public PaymentService(IEnumerable<IPaymentStrategy> strategies)
        {
            _strategies = strategies;
        }

        public void ProcessPayment(decimal amount, PaymentDetails details)
        {
            // اختيار الاستراتيجية المناسبة
            var strategy = _strategies.FirstOrDefault(s => s.CanProcess(details));

            if (strategy == null)
            {
                throw new Exception("No payment method available for the provided details");
            }

            strategy.ProcessPayment(amount, details);
        }
    }

    public class CheckoutService
    {
        private readonly PaymentService _paymentService;

        public CheckoutService(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void Checkout(Order order, PaymentDetails paymentDetails)
        {
            _paymentService.ProcessPayment(order.TotalAmount, paymentDetails);
        }
    }

    // ✅ Composition Root
    public class Program
    {
        public static void Main()
        {
            // تسجيل كل الاستراتيجيات
            var strategies = new List<IPaymentStrategy>
            {
                new VisaPaymentStrategy(),
                new MastercardPaymentStrategy(),
                new PayPalPaymentStrategy()
            };

            var paymentService = new PaymentService(strategies);
            var checkoutService = new CheckoutService(paymentService);

            var order = new Order { TotalAmount = 100 };

            // ✅ دفع بـ Visa
            checkoutService.Checkout(order, new PaymentDetails
            {
                CardNumber = "4111111111111111"
            });

            // ✅ دفع بـ PayPal
            checkoutService.Checkout(order, new PaymentDetails
            {
                PayPalEmail = "user@example.com"
            });

            // ✅ لو عايزين نضيف Apple Pay؟ --> نعمل Implementation جديد فقط!
            // مش محتاجين نعدل على PaymentService أو CheckoutService
        }
    }

    // ✅ إضافة استراتيجية جديدة بدون تعديل كود موجود
    public class ApplePayPaymentStrategy : IPaymentStrategy
    {
        public bool CanProcess(PaymentDetails details)
        {
            return !string.IsNullOrEmpty(details.WalletId);
        }

        public void ProcessPayment(decimal amount, PaymentDetails details)
        {
            Console.WriteLine($"Processing {amount} via Apple Pay");
            Console.WriteLine($"Wallet ID: {details.WalletId}");
            // Apple Pay API logic
        }
    }
}
