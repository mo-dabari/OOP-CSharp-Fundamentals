public interface INotification
    {
        bool Send(string recipient, string message);
        string GetChannelName();
        bool ValidateRecipient(string recipient);
    }
    
    public class EmailNotification : INotification
    {
        public bool Send(string recipient, string message)
        {
            if (!ValidateRecipient(recipient))
            {
                Console.WriteLine("❌ بريد إلكتروني غير صحيح");
                return false;
            }
            Console.WriteLine($"📧 تم إرسال بريد إلى: {recipient}");
            Console.WriteLine($"   الرسالة: {message}");
            return true;
        }
        
        public string GetChannelName() => "البريد الإلكتروني";
        
        public bool ValidateRecipient(string recipient)
        {
            return recipient.Contains("@") && recipient.Contains(".");
        }
    }
    
    public class SMSNotification : INotification
    {
        public bool Send(string recipient, string message)
        {
            if (!ValidateRecipient(recipient))
            {
                Console.WriteLine("❌ رقم هاتف غير صحيح");
                return false;
            }
            Console.WriteLine($"📱 تم إرسال SMS إلى: {recipient}");
            Console.WriteLine($"   الرسالة: {message.Substring(0, Math.Min(20, message.Length))}...");
            return true;
        }
        
        public string GetChannelName() => "الرسائل النصية";
        
        public bool ValidateRecipient(string recipient)
        {
            return recipient.Length >= 10 && recipient.All(char.IsDigit);
        }
    }
    
    public class PushNotification : INotification
    {
        public bool Send(string recipient, string message)
        {
            if (!ValidateRecipient(recipient))
            {
                Console.WriteLine("❌ معرف مستخدم غير صحيح");
                return false;
            }
            Console.WriteLine($"🔔 تم إرسال إشعار فوري للمستخدم: {recipient}");
            Console.WriteLine($"   الإشعار: {message}");
            return true;
        }
        
        public string GetChannelName() => "الإشعارات الفورية";
        
        public bool ValidateRecipient(string recipient)
        {
            return !string.IsNullOrEmpty(recipient) && recipient.Length >= 3;
        }
    }
    
    public class NotificationService
    {
        private List<INotification> channels = new();
        private List<string> notificationHistory = new();
        
        public void AddChannel(INotification channel)
        {
            channels.Add(channel);
        }
        
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"\n📢 إرسال إشعار:");
            Console.WriteLine($"   المستقبل: {recipient}");
            Console.WriteLine($"   الرسالة: {message}");
            Console.WriteLine("────────────────────────────────");
            
            bool sentSuccessfully = false;
            foreach (var channel in channels)
            {
                if (channel.Send(recipient, message))
                {
                    sentSuccessfully = true;
                    notificationHistory.Add(
                        $"[{DateTime.Now:HH:mm:ss}] {channel.GetChannelName()}: {recipient}");
                }
            }
            
            if (!sentSuccessfully)
                Console.WriteLine("⚠️  فشل إرسال الإشعار عبر جميع القنوات");
        }
        
        public void PrintHistory()
        {
            Console.WriteLine("\n📋 سجل الإشعارات:");
            foreach (var record in notificationHistory)
                Console.WriteLine($"  {record}");
        }
    }