/*
 * PaymentSystem.cs
 * ============================================
 * مثال واقعي: نظام معالجة الدفع الاحترافي
 * 
 * هذا الملف يوضح:
 * - استخدام Interfaces كعقود
 * - الوراثة المتعددة من Interfaces
 * - Dependency Injection (حقن الاعتماديات)
 * - معالجة حالات الدفع المختلفة
 * - نمط Strategy مع Abstraction
 * 
 * التشبيه: البنك يقبل دفع من طرق مختلفة
 * لكن عملية الدفع الأساسية واحدة
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Abstraction.Examples
{
    // ============================================
    // الواجهات الأساسية (Interfaces)
    // ============================================
    
    /// <summary>
    /// واجهة معالج الدفع
    /// جميع طرق الدفع يجب أن تطبقها
    /// </summary>
    public interface IPaymentProcessor
    {
        /// <summary>
        /// معالجة عملية الدفع
        /// </summary>
        bool ProcessPayment(decimal amount, string description);
        
        /// <summary>
        /// استرجاع المبلغ
        /// </summary>
        bool RefundPayment(string transactionId, decimal amount);
        
        /// <summary>
        /// التحقق من أن الطريقة متاحة
        /// </summary>
        bool IsAvailable();
        
        /// <summary>
        /// الحصول على اسم الطريقة
        /// </summary>
        string GetPaymentMethodName();
    }
    
    /// <summary>
    /// واجهة للتحقق من الحسابات
    /// </summary>
    public interface IAccountValidator
    {
        bool ValidateAccount();
        string GetAccountInfo();
    }
    
    /// <summary>
    /// واجهة لتسجيل المعاملات
    /// </summary>
    public interface ITransactionLogger
    {
        void LogTransaction(string transactionId, string details);
        void LogError(string error);
    }
    
    /// <summary>
    /// واجهة للإخطارات
    /// </summary>
    public interface INotificationService
    {
        void SendConfirmation(string recipientId, string message);
        void SendAlert(string recipientId, string alert);
    }
    
    
    // ============================================
    // نظام التسجيل (Logger)
    // ============================================
    
    public class ConsoleTransactionLogger : ITransactionLogger
    {
        public void LogTransaction(string transactionId, string details)
        {
            Console.WriteLine($"📝 [LOG] معاملة #{transactionId}: {details}");
        }
        
        public void LogError(string error)
        {
            Console.WriteLine($"⚠️  [ERROR] {error}");
        }
    }
    
    public class FileTransactionLogger : ITransactionLogger
    {
        private List<string> logs = new();
        
        public void LogTransaction(string transactionId, string details)
        {
            logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] معاملة #{transactionId}: {details}");
        }
        
        public void LogError(string error)
        {
            logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] خطأ: {error}");
        }
        
        public void PrintAllLogs()
        {
            Console.WriteLine("\n📋 جميع السجلات:");
            foreach (var log in logs)
                Console.WriteLine($"  {log}");
        }
    }
    
    
    // ============================================
    // نظام الإخطارات
    // ============================================
    
    public class EmailNotificationService : INotificationService
    {
        public void SendConfirmation(string recipientId, string message)
        {
            Console.WriteLine($"📧 تم إرسال تأكيد بريد إلى {recipientId}");
            Console.WriteLine($"   الرسالة: {message}");
        }
        
        public void SendAlert(string recipientId, string alert)
        {
            Console.WriteLine($"📧 تنبيه بريد إلى {recipientId}: {alert}");
        }
    }
    
    public class SMSNotificationService : INotificationService
    {
        public void SendConfirmation(string recipientId, string message)
        {
            Console.WriteLine($"📱 تم إرسال تأكيد SMS إلى {recipientId}");
        }
        
        public void SendAlert(string recipientId, string alert)
        {
            Console.WriteLine($"📱 تنبيه SMS إلى {recipientId}: {alert}");
        }
    }
    
    
    // ============================================
    // طرق الدفع المختلفة
    // ============================================
    
    /// <summary>
    /// الدفع ببطاقة الائتمان
    /// </summary>
    public class CreditCardProcessor : IPaymentProcessor, IAccountValidator
    {
        private string cardNumber;
        private string cardholderName;
        private decimal balance;
        private ITransactionLogger logger;
        private INotificationService notificationService;
        
        public CreditCardProcessor(string cardNumber, string name, decimal limit,
            ITransactionLogger logger, INotificationService notificationService)
        {
            cardNumber = cardNumber;
            cardholderName = name;
            balance = limit;
            this.logger = logger;
            this.notificationService = notificationService;
        }
        
        public bool ProcessPayment(decimal amount, string description)
        {
            // التحقق من الحساب
            if (!ValidateAccount())
            {
                logger.LogError("بطاقة غير صالحة");
                return false;
            }
            
            // التحقق من الرصيد
            if (amount > balance)
            {
                logger.LogError($"رصيد غير كافي. المتاح: {balance}, المطلوب: {amount}");
                return false;
            }
            
            // معالجة الدفع
            balance -= amount;
            string transactionId = GenerateTransactionId();
            logger.LogTransaction(transactionId, 
                $"دفع ببطاقة ائتمان: {amount:C}");
            
            notificationService.SendConfirmation(cardholderName,
                $"تم الدفع بنجاح: {description} - المبلغ: {amount:C}");
            
            return true;
        }
        
        public bool RefundPayment(string transactionId, decimal amount)
        {
            balance += amount;
            logger.LogTransaction(transactionId,
                $"استرجاع مبلغ: {amount:C}");
            
            notificationService.SendConfirmation(cardholderName,
                $"تم استرجاع المبلغ: {amount:C}");
            
            return true;
        }
        
        public bool IsAvailable()
        {
            return !string.IsNullOrEmpty(cardNumber) && balance > 0;
        }
        
        public string GetPaymentMethodName()
        {
            return "بطاقة ائتمان";
        }
        
        public bool ValidateAccount()
        {
            // تحقق بسيط (في الواقع أكثر تعقيداً)
            return !string.IsNullOrEmpty(cardNumber) && cardNumber.Length >= 13;
        }
        
        public string GetAccountInfo()
        {
            return $"بطاقة: {cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*')} - " +
                   $"المتاح: {balance:C}";
        }
        
        private string GenerateTransactionId()
        {
            return "CC_" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
    
    /// <summary>
    /// الدفع عبر PayPal
    /// </summary>
    public class PayPalProcessor : IPaymentProcessor, IAccountValidator
    {
        private string email;
        private string password;
        private decimal balance;
        private ITransactionLogger logger;
        private INotificationService notificationService;
        
        public PayPalProcessor(string email, decimal balance,
            ITransactionLogger logger, INotificationService notificationService)
        {
            this.email = email;
            this.password = "secured";
            this.balance = balance;
            this.logger = logger;
            this.notificationService = notificationService;
        }
        
        public bool ProcessPayment(decimal amount, string description)
        {
            if (!ValidateAccount())
            {
                logger.LogError("حساب PayPal غير صحيح");
                return false;
            }
            
            if (amount > balance)
            {
                logger.LogError($"الرصيد غير كافي في حساب PayPal");
                return false;
            }
            
            // محاكاة اتصال PayPal
            Console.WriteLine("🌐 جاري الاتصال بـ PayPal...");
            System.Threading.Thread.Sleep(500);
            
            balance -= amount;
            string transactionId = GenerateTransactionId();
            logger.LogTransaction(transactionId,
                $"دفع عبر PayPal: {amount:C}");
            
            notificationService.SendConfirmation(email,
                $"تم الدفع من حسابك: {description} - المبلغ: {amount:C}");
            
            return true;
        }
        
        public bool RefundPayment(string transactionId, decimal amount)
        {
            balance += amount;
            logger.LogTransaction(transactionId,
                $"استرجاع من PayPal: {amount:C}");
            return true;
        }
        
        public bool IsAvailable()
        {
            return !string.IsNullOrEmpty(email);
        }
        
        public string GetPaymentMethodName()
        {
            return "PayPal";
        }
        
        public bool ValidateAccount()
        {
            return email.Contains("@") && email.Contains(".");
        }
        
        public string GetAccountInfo()
        {
            return $"PayPal: {email} - المتاح: {balance:C}";
        }
        
        private string GenerateTransactionId()
        {
            return "PP_" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
    
    /// <summary>
    /// الدفع عبر المحفظة الرقمية
    /// </summary>
    public class DigitalWalletProcessor : IPaymentProcessor, IAccountValidator
    {
        private string phoneNumber;
        private decimal balance;
        private ITransactionLogger logger;
        private INotificationService notificationService;
        
        public DigitalWalletProcessor(string phoneNumber, decimal balance,
            ITransactionLogger logger, INotificationService notificationService)
        {
            this.phoneNumber = phoneNumber;
            this.balance = balance;
            this.logger = logger;
            this.notificationService = notificationService;
        }
        
        public bool ProcessPayment(decimal amount, string description)
        {
            if (!ValidateAccount())
            {
                logger.LogError("رقم الهاتف غير صحيح");
                return false;
            }
            
            if (amount > balance)
            {
                logger.LogError("رصيد المحفظة غير كافي");
                return false;
            }
            
            balance -= amount;
            string transactionId = GenerateTransactionId();
            logger.LogTransaction(transactionId,
                $"دفع من محفظة رقمية: {amount:C}");
            
            notificationService.SendConfirmation(phoneNumber,
                $"تم الدفع: {description} - المبلغ: {amount:C}");
            
            return true;
        }
        
        public bool RefundPayment(string transactionId, decimal amount)
        {
            balance += amount;
            logger.LogTransaction(transactionId,
                $"استرجاع من المحفظة: {amount:C}");
            return true;
        }
        
        public bool IsAvailable()
        {
            return !string.IsNullOrEmpty(phoneNumber);
        }
        
        public string GetPaymentMethodName()
        {
            return "محفظة رقمية";
        }
        
        public bool ValidateAccount()
        {
            return phoneNumber.Length >= 10;
        }
        
        public string GetAccountInfo()
        {
            return $"هاتف: {phoneNumber} - المتاح: {balance:C}";
        }
        
        private string GenerateTransactionId()
        {
            return "DW_" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
    
    
    // ============================================
    // نظام معالجة الطلبات (Order Processor)
    // ============================================
    
    /// <summary>
    /// نظام معالجة الطلبات
    /// يوضح كيفية استخدام Dependency Injection
    /// </summary>
    public class OrderProcessor
    {
        private IPaymentProcessor paymentProcessor;
        private INotificationService notificationService;
        private ITransactionLogger logger;
        
        // Dependency Injection - حقن الاعتماديات
        public OrderProcessor(IPaymentProcessor processor,
            INotificationService notification,
            ITransactionLogger transactionLogger)
        {
            paymentProcessor = processor;
            notificationService = notification;
            logger = transactionLogger;
        }
        
        public bool ProcessOrder(decimal amount, string description)
        {
            Console.WriteLine($"\n🛒 معالجة الطلب:");
            Console.WriteLine($"   المبلغ: {amount:C}");
            Console.WriteLine($"   الوصف: {description}");
            Console.WriteLine($"   الطريقة: {paymentProcessor.GetPaymentMethodName()}");
            
            if (!paymentProcessor.IsAvailable())
            {
                logger.LogError("طريقة الدفع غير متاحة");
                return false;
            }
            
            bool success = paymentProcessor.ProcessPayment(amount, description);
            
            if (success)
            {
                Console.WriteLine("✅ تم معالجة الطلب بنجاح!");
            }
            else
            {
                Console.WriteLine("❌ فشل معالجة الطلب");
            }
            
            return success;
        }
    }
    
}