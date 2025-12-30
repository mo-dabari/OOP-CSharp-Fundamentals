using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Interfaces.Exercises
{
    public interface INotifiable
    {
        void Send(string message);
    }
    public interface ISchedulable
    {
        void ScheduleNotification(string message, DateTime time);
    }

    public class EmailNotification : INotifiable, ISchedulable
    {
        private string _email;

        public EmailNotification(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            if (!email.Contains("@"))
                throw new Exception("Invalid Email :(");

            _email = email;
        }

        public void Send(string message)
        {
            Console.WriteLine($"📧 بريد إلى {_email}: {message}");
        }

        public void ScheduleNotification(string message, DateTime time)
        {
            Console.WriteLine($"📧 جدولة بريد في {time}: {message}");
        }
    }

    public class SMSNotification : INotifiable, ISchedulable
    {
        private string _phone;
        public SMSNotification(string phone)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(_phone, nameof(_phone));
            if (!phone.All(c => char.IsDigit(c) || phone.Length != 11))
                throw new Exception("Invalid Phone :(");

            _phone = phone;
        }
        public void Send(string message)
        {
            Console.WriteLine($"📱 رسالة نصية إلى {_phone}: {message}");
        }

        public void ScheduleNotification(string message, DateTime time)
        {
            Console.WriteLine($"📱 جدولة رسالة نصية في {time}");
        }
    }

    public class PushNotification : INotifiable
    {
        private string _userID;

        public PushNotification(string userId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userId, nameof(userId));

            _userID = userId;
        }

        public void Send(string message)
        {
            Console.WriteLine($"🔔 إشعار للمستخدم {_userID}: {message}");
        }
    }

    public class NotificationService
    {
        private List<INotifiable> notifications = new();

        public void RegisterNotification(INotifiable notification)
        {
            ArgumentNullException.ThrowIfNull(notification);
            notifications.Add(notification);
            Console.WriteLine($"✅ تم تسجيل قناة إشعار");
        }

        public void SendToAll(string message)
        {
            Console.WriteLine($"\n📢 إرسال الرسالة: {message}");
            foreach (var notification in notifications)
            {
                notification.Send(message);
            }
        }
    }
}

