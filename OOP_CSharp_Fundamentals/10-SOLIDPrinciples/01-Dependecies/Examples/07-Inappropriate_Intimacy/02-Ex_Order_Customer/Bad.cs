// ====================================
// Inappropriate Intimacy - الألفة غير المناسبة
// ====================================

// ❌ الطريقة الخاطئة - كلاس يعرف تفاصيل داخلية عن كلاس آخر
namespace InappropriateIntimacy.OrderCustomer.BadExample
{
    // مثال: Order و Customer
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }

        public void ApplyDiscount()
        {
            // ❌ Order يعرف التفاصيل الداخلية لـ Customer
            if (Customer.IsVIP)
            {
                // ❌ Order يعدّل في حالة Customer مباشرة!
                Customer.LoyaltyPoints += 100;
                TotalAmount = TotalAmount * 0.9m; // 10% discount
            }

            // ❌ Order يتحقق من شروط داخلية لـ Customer
            if (Customer.OrderHistory.Count > 10)
            {
                TotalAmount = TotalAmount * 0.95m; // 5% discount
            }

            // ❌ Order يصل لـ private-like data
            if (Customer.CreditScore > 700)
            {
                // يسمح بالشراء الآجل
                Customer.CreditLimit += 1000;
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVIP { get; set; }
        public int LoyaltyPoints { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public int CreditScore { get; set; }
        public decimal CreditLimit { get; set; }
    }

    // ❌ المشاكل:
    // 1. Order يعرف أكثر من اللازم عن Customer
    // 2. انتهاك لـ Encapsulation - Order يعدّل حالة Customer مباشرة
    // 3. Tight Coupling - لو Customer اتغير، Order لازم يتغير
    // 4. Responsibility مش واضحة - مين المسؤول عن Loyalty Points؟
}
