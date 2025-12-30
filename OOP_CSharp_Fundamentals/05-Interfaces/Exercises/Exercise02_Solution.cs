using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Interfaces.Exercises
{
    public interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount);
        string GetPaymentMethodName();
    }
    public interface IRefundable
    {
        bool RefundPayment(decimal amount);
    }

    public class CreditCard : IPaymentMethod , IRefundable
    {
        private string _cardNumber;
        private decimal _balance;

        public CreditCard(string number, decimal initialBalance)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(number, nameof(number));
            ArgumentOutOfRangeException.ThrowIfNegative(initialBalance, nameof(initialBalance));

            _cardNumber = number;
            _balance = initialBalance;
        }
        public bool ProcessPayment(decimal amount)
        {
            if(amount > _balance)
            {
                Console.WriteLine($"❌ رصيد غير كافي");
                return false;
            }

            _balance -= amount;
            Console.WriteLine($"💳 دفع بنجاح: {amount:C}");
            return true;
        }
        public string GetPaymentMethodName()
        {
            return "بطاقة ائتمان";
        }

        public bool RefundPayment(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"💳 تم استرجاع: {amount:C}");
            return true;
        }
    }

    public class PayPal : IPaymentMethod, IRefundable
    {
        private string _email;
        private decimal _balance;
        public PayPal(string email, decimal initialBalance)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            if(!email.Contains("@"))
                throw new Exception("Invalid Email :(");

            ArgumentOutOfRangeException.ThrowIfNegative(initialBalance, nameof(initialBalance));

            _email = email;
            _balance = initialBalance;
        }
        public bool ProcessPayment(decimal amount)
        {
            if (amount > _balance)
            {
                Console.WriteLine($"❌ رصيد غير كافي");
                return false;
            }

            _balance -= amount;
            Console.WriteLine($"🅿️  دفع PayPal بنجاح: {amount:C}");
            return true;
        }
        public string GetPaymentMethodName()
        {
            return "PayPal";
        }

        public bool RefundPayment(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"🅿️  استرجاع PayPal: {amount:C}");
            return true;
        }
    }

    public class GooglePay : IPaymentMethod, IRefundable
    {
        private decimal _balance;

        public GooglePay(decimal initialBalance)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(initialBalance, nameof(initialBalance));
            _balance = initialBalance;
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount > _balance)
            {
                Console.WriteLine($"❌ رصيد غير كافي");
                return false;
            }

            _balance -= amount;
            Console.WriteLine($"🔵 دفع Google Pay بنجاح: {amount:C}");
            return true;
        }
        public string GetPaymentMethodName()
        {
            return "Google Pay";
        }

        public bool RefundPayment(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"🔵 استرجاع Google Pay: {amount:C}");
            return true;
        }
    }

    public class Checkout
    {
        public bool ProcessOrder(decimal amount, IPaymentMethod paymentMethod)
        {
            Console.WriteLine($"\n🛒 معالجة الطلب بـ {paymentMethod.GetPaymentMethodName()}");
            return paymentMethod.ProcessPayment(amount);
        }
    }
}

