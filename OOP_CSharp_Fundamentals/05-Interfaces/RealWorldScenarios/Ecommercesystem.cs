/*
 * ECommerceSystem.cs
 * ════════════════════════════════════════════════════════════
 * حالة واقعية متقدمة: نظام تجارة إلكترونية متكامل
 *
 * السيناريو:
 * ────────
 * متجر أون لاين يحتاج:
 * - عدة طرق دفع مختلفة
 * - عدة طرق شحن مختلفة
 * - نظام إخطار متعدد
 * - نظام حسابات بنكية
 * - تقارير شاملة
 *
 * هذا يوضح كيف تحل Interfaces مشاكل معقدة حقيقية
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_CSharp_Fundamentals
{
    // ════════════════════════════════════════════════════════════
    // نماذج البيانات
    // ════════════════════════════════════════════════════════════

    public class Product
    {
        public int Id { get;}
        public string Name { get;}
        public decimal Price { get;}

        public Product(int id, string name, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id , nameof(id));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            Id = id;
            Name = name;
            Price = price;
        }
    }

    public class Order
    {
        public int OrderId { get;}
        private List<Product> _products;
        public IReadOnlyList<Product> Products;
        public decimal Total { get; set;}
        public DateTime OrderDate { get;}
        public OrderStatus Status { get; set;}

        public Order(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            OrderId = id;
            _products = new List<Product>();
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;

            Products = _products.AsReadOnly();
        }
    }

    public enum OrderStatus
    {
        Pending,
        PaymentProcessing,
        Shipped,
        Delivered,
        Cancelled
    }

    // ════════════════════════════════════════════════════════════
    // الواجهات
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// واجهة معالجة الدفع
    /// </summary>
    public interface IPaymentGateway
    {
        bool ProcessPayment(decimal amount, string accountInfo);
        bool VerifyPayment(string transactionId);
        void RefundPayment(string transactionId, decimal amount);
        string GetGatewayName();
    }

    /// <summary>
    /// واجهة الشحن
    /// </summary>
    public interface IShippingProvider
    {
        void ShipOrder(Order order, string address);
        string TrackOrder(string trackingNumber);
        decimal GetShippingCost(Order order);
        string GetProviderName();
    }

    /// <summary>
    /// واجهة الإخطار
    /// </summary>
    public interface INotificationChannel
    {
        void SendNotification(string recipient, string message);
        bool IsAvailable();
    }

    /// <summary>
    /// واجهة التقارير
    /// </summary>
    public interface IReportGenerator
    {
        void GenerateReport(List<Order> orders);
        string GetReportType();
    }


    // ════════════════════════════════════════════════════════════
    // تطبيقات الدفع
    // ════════════════════════════════════════════════════════════

    public class StripePaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount, string cardToken)
        {
            Console.WriteLine($"💳 Stripe: معالجة دفع {amount:C}");
            Console.WriteLine($"   التوكن: {cardToken}");
            return true;
        }

        public bool VerifyPayment(string transactionId)
        {
            Console.WriteLine($"✅ Stripe: التحقق من العملية {transactionId}");
            return true;
        }

        public void RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"🔄 Stripe: استرجاع {amount:C} من {transactionId}");
        }

        public string GetGatewayName() => "Stripe";
    }

    public class PayPalPaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount, string email)
        {
            Console.WriteLine($"🅿️  PayPal: معالجة دفع {amount:C}");
            Console.WriteLine($"   البريد: {email}");
            return true;
        }

        public bool VerifyPayment(string transactionId)
        {
            Console.WriteLine($"✅ PayPal: التحقق من {transactionId}");
            return true;
        }

        public void RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"🔄 PayPal: استرجاع {amount:C}");
        }

        public string GetGatewayName() => "PayPal";
    }

    public class ApplePaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount, string deviceId)
        {
            Console.WriteLine($"🍎 Apple Pay: معالجة دفع {amount:C}");
            return true;
        }

        public bool VerifyPayment(string transactionId)
        {
            Console.WriteLine($"✅ Apple Pay: التحقق من {transactionId}");
            return true;
        }

        public void RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"🔄 Apple Pay: استرجاع {amount:C}");
        }

        public string GetGatewayName() => "Apple Pay";
    }

    // ════════════════════════════════════════════════════════════
    // تطبيقات الشحن
    // ════════════════════════════════════════════════════════════

    public class FedexShippingProvider : IShippingProvider
    {
        public void ShipOrder(Order order, string address)
        {
            Console.WriteLine($"📦 FedEx: شحن الطلب #{order.OrderId}");
            Console.WriteLine($"   العنوان: {address}");
            order.Status = OrderStatus.Shipped;
        }

        public string TrackOrder(string trackingNumber)
        {
            return $"🔍 FedEx: الطلب في الطريق - {trackingNumber}";
        }

        public decimal GetShippingCost(Order order)
        {
            return order.Total * 0.05m;  // 5% من السعر
        }

        public string GetProviderName() => "FedEx";
    }

    public class DhlShippingProvider : IShippingProvider
    {
        public void ShipOrder(Order order, string address)
        {
            Console.WriteLine($"📦 DHL: شحن الطلب #{order.OrderId}");
            Console.WriteLine($"   العنوان: {address}");
            order.Status = OrderStatus.Shipped;
        }

        public string TrackOrder(string trackingNumber)
        {
            return $"🔍 DHL: الطلب في مسار التسليم - {trackingNumber}";
        }

        public decimal GetShippingCost(Order order)
        {
            return order.Total * 0.07m;  // 7% من السعر
        }

        public string GetProviderName() => "DHL";
    }

    public class LocalDeliveryProvider : IShippingProvider
    {
        public void ShipOrder(Order order, string address)
        {
            Console.WriteLine($"📦 توصيل محلي: شحن الطلب #{order.OrderId}");
            Console.WriteLine($"   العنوان: {address}");
            order.Status = OrderStatus.Shipped;
        }

        public string TrackOrder(string trackingNumber)
        {
            return $"🔍 التوصيل المحلي: سيصل غداً - {trackingNumber}";
        }

        public decimal GetShippingCost(Order order)
        {
            return 50;  // سعر ثابت
        }

        public string GetProviderName() => "التوصيل المحلي";
    }

    // ════════════════════════════════════════════════════════════
    // تطبيقات الإخطارات
    // ════════════════════════════════════════════════════════════

    public class EmailNotificationChannel : INotificationChannel
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"📧 بريد إلى {recipient}: {message}");
        }

        public bool IsAvailable() => true;
    }

    public class SmsNotificationChannel : INotificationChannel
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"📱 رسالة نصية إلى {recipient}: {message}");
        }

        public bool IsAvailable() => true;
    }

    public class PushNotificationChannel : INotificationChannel
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"🔔 إشعار فوري للمستخدم {recipient}: {message}");
        }

        public bool IsAvailable() => true;
    }


    // ════════════════════════════════════════════════════════════
    // تطبيقات التقارير
    // ════════════════════════════════════════════════════════════

    public class SalesReportGenerator : IReportGenerator
    {
        public void GenerateReport(List<Order> orders)
        {
            Console.WriteLine("\n📊 تقرير المبيعات:");
            Console.WriteLine("════════════════════════════════");
            decimal totalSales = orders.Sum(o => o.Total);
            Console.WriteLine($"إجمالي المبيعات: {totalSales:C}");
            Console.WriteLine($"عدد الطلبات: {orders.Count}");
            Console.WriteLine($"متوسط الطلب: {(orders.Count > 0 ? totalSales / orders.Count : 0):C}");
        }

        public string GetReportType() => "تقرير المبيعات";
    }

    public class CustomerReportGenerator : IReportGenerator
    {
        public void GenerateReport(List<Order> orders)
        {
            Console.WriteLine("\n👥 تقرير العملاء:");
            Console.WriteLine("════════════════════════════════");
            var orderedCount = orders.Count(o => o.Status == OrderStatus.Delivered);
            Console.WriteLine($"الطلبات المسلمة: {orderedCount}");
            Console.WriteLine($"الطلبات المعلقة: {orders.Count(o => o.Status == OrderStatus.Pending)}");
            Console.WriteLine($"الطلبات المرفوضة: {orders.Count(o => o.Status == OrderStatus.Cancelled)}");
        }

        public string GetReportType() => "تقرير العملاء";
    }

    public class DetailedReportGenerator : IReportGenerator
    {
        public void GenerateReport(List<Order> orders)
        {
            Console.WriteLine("\n📋 التقرير المفصل:");
            Console.WriteLine("════════════════════════════════");
            foreach (var order in orders)
            {
                Console.WriteLine($"الطلب #{order.OrderId}:");
                Console.WriteLine($"  المبلغ: {order.Total:C}");
                Console.WriteLine($"  الحالة: {order.Status}");
                Console.WriteLine($"  التاريخ: {order.OrderDate:yyyy-MM-dd}");
            }
        }

        public string GetReportType() => "التقرير المفصل";
    }


        // ════════════════════════════════════════════════════════════
    // نظام E-Commerce
    // ════════════════════════════════════════════════════════════

    public class ECommerceStore
    {
        private IPaymentGateway paymentGateway;
        private IShippingProvider shippingProvider;
        private List<INotificationChannel> notificationChannels;
        private List<Order> orders;
        private List<IReportGenerator> reportGenerators;

        public ECommerceStore(
            IPaymentGateway payment,
            IShippingProvider shipping)
        {
            paymentGateway = payment;
            shippingProvider = shipping;
            notificationChannels = new List<INotificationChannel>();
            orders = new List<Order>();
            reportGenerators = new List<IReportGenerator>();
        }

        public void AddNotificationChannel(INotificationChannel channel)
        {
            notificationChannels.Add(channel);
        }

        public void AddReportGenerator(IReportGenerator generator)
        {
            reportGenerators.Add(generator);
        }

        public void ProcessOrder(Order order, string paymentInfo, string shippingAddress, string customerContact)
        {
            Console.WriteLine($"\n🛒 معالجة الطلب #{order.OrderId}");
            Console.WriteLine("════════════════════════════════");

            // الدفع
            order.Status = OrderStatus.PaymentProcessing;
            Console.WriteLine($"💳 استخدام: {paymentGateway.GetGatewayName()}");
            if (paymentGateway.ProcessPayment(order.Total, paymentInfo))
            {
                // الشحن
                Console.WriteLine($"📦 استخدام: {shippingProvider.GetProviderName()}");
                decimal shippingCost = shippingProvider.GetShippingCost(order);
                order.Total += shippingCost;
                shippingProvider.ShipOrder(order, shippingAddress);

                // الإخطار
                Console.WriteLine("\n📬 إرسال الإخطارات:");
                foreach (var channel in notificationChannels)
                {
                    if (channel.IsAvailable())
                    {
                        channel.SendNotification(
                            customerContact,
                            $"تم تسليم طلبك #{order.OrderId} بنجاح");
                    }
                }

                orders.Add(order);
                Console.WriteLine("\n✅ تم معالجة الطلب بنجاح!");
            }
            else
            {
                order.Status = OrderStatus.Cancelled;
                Console.WriteLine("\n❌ فشل الدفع!");
            }
        }

        public void GenerateAllReports()
        {
            Console.WriteLine("\n\n📊 إنشاء التقارير:");
            Console.WriteLine("════════════════════════════════");
            foreach (var generator in reportGenerators)
            {
                generator.GenerateReport(orders);
            }
        }
    }
}
