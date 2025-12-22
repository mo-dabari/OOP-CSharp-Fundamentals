public interface IDataStore
    {
        void Save(string key, string value);
        string Load(string key);
        void Delete(string key);
        bool Exists(string key);
        string GetStorageName();
    }
    
    public class MemoryStore : IDataStore
    {
        private Dictionary<string, string> data = new();
        
        public void Save(string key, string value)
        {
            data[key] = value;
            Console.WriteLine($"💾 تم حفظ '{key}' في الذاكرة");
        }
        
        public string Load(string key)
        {
            if (data.ContainsKey(key))
            {
                Console.WriteLine($"📂 تم تحميل '{key}' من الذاكرة");
                return data[key];
            }
            return null;
        }
        
        public void Delete(string key)
        {
            data.Remove(key);
            Console.WriteLine($"🗑️  تم حذف '{key}' من الذاكرة");
        }
        
        public bool Exists(string key) => data.ContainsKey(key);
        public string GetStorageName() => "الذاكرة";
    }
    
    public class FileStore : IDataStore
    {
        private Dictionary<string, string> fileSimulation = new();
        
        public void Save(string key, string value)
        {
            fileSimulation[key] = value;
            Console.WriteLine($"💾 تم حفظ '{key}' في ملف");
        }
        
        public string Load(string key)
        {
            if (fileSimulation.ContainsKey(key))
            {
                Console.WriteLine($"📂 تم تحميل '{key}' من ملف");
                return fileSimulation[key];
            }
            return null;
        }
        
        public void Delete(string key)
        {
            fileSimulation.Remove(key);
            Console.WriteLine($"🗑️  تم حذف '{key}' من الملف");
        }
        
        public bool Exists(string key) => fileSimulation.ContainsKey(key);
        public string GetStorageName() => "الملفات";
    }
    
    public class CloudStore : IDataStore
    {
        private Dictionary<string, string> cloudData = new();
        
        public void Save(string key, string value)
        {
            // محاكاة تأخير الاتصال
            System.Threading.Thread.Sleep(100);
            cloudData[key] = value;
            Console.WriteLine($"☁️  تم حفظ '{key}' على السحابة");
        }
        
        public string Load(string key)
        {
            System.Threading.Thread.Sleep(100);
            if (cloudData.ContainsKey(key))
            {
                Console.WriteLine($"☁️  تم تحميل '{key}' من السحابة");
                return cloudData[key];
            }
            return null;
        }
        
        public void Delete(string key)
        {
            cloudData.Remove(key);
            Console.WriteLine($"☁️  تم حذف '{key}' من السحابة");
        }
        
        public bool Exists(string key) => cloudData.ContainsKey(key);
        public string GetStorageName() => "التخزين السحابي";
    }
    
    public class DataManager
    {
        private IDataStore primaryStore;
        private IDataStore backupStore;
        
        public DataManager(IDataStore primary, IDataStore backup)
        {
            primaryStore = primary;
            backupStore = backup;
        }
        
        public void SafeSave(string key, string value)
        {
            Console.WriteLine($"\n💾 حفظ آمن لـ '{key}':");
            primaryStore.Save(key, value);
            backupStore.Save(key, value);
            Console.WriteLine("✅ تم حفظ نسخة احتياطية");
        }
        
        public string SafeLoad(string key)
        {
            Console.WriteLine($"\n📂 تحميل '{key}':");
            string value = primaryStore.Load(key);
            if (value != null)
                return value;
            
            Console.WriteLine("⚠️  الخزان الأساسي فارغ، جاري البحث في النسخة الاحتياطية...");
            return backupStore.Load(key);
        }
        
        public void PrintStatus()
        {
            Console.WriteLine($"\n📊 الخزان الأساسي: {primaryStore.GetStorageName()}");
            Console.WriteLine($"   النسخة الاحتياطية: {backupStore.GetStorageName()}");
        }
    





    // ═══════════════════════════════════════════════════════════
    // Program - تشغيل جميع التمارين
    // ═══════════════════════════════════════════════════════════
    
    class ExercisesProgram
    {
        static void RunExercises()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            تمارين عملية على Abstraction               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
            
            // ═════════════════════════════════════════
            // التمرين 1: نظام النقل
            // ═════════════════════════════════════════
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 1: نظام وسائل النقل");
            Console.WriteLine(new string('═', 60) + "\n");
            
            var vehicleManager = new VehicleManager();
            vehicleManager.AddVehicle(new Car());
            vehicleManager.AddVehicle(new Motorcycle());
            vehicleManager.AddVehicle(new Bus());
            
            vehicleManager.StartAllVehicles();
            vehicleManager.PrintReport();
            vehicleManager.StopAllVehicles();
            
            // ═════════════════════════════════════════
            // التمرين 2: نظام الإشعارات
            // ═════════════════════════════════════════
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 2: نظام الإشعارات");
            Console.WriteLine(new string('═', 60) + "\n");
            
            var notificationService = new NotificationService();
            notificationService.AddChannel(new EmailNotification());
            notificationService.AddChannel(new SMSNotification());
            notificationService.AddChannel(new PushNotification());
            
            notificationService.SendNotification("user@email.com", "مرحباً بك!");
            notificationService.SendNotification("966501234567", "رسالة نصية");
            notificationService.SendNotification("user123", "إشعار فوري");
            notificationService.PrintHistory();
            
            // ═════════════════════════════════════════
            // التمرين 3: نظام التخزين
            // ═════════════════════════════════════════
            Console.WriteLine("\n" + new string('═', 60));
            Console.WriteLine("  التمرين 3: نظام التخزين");
            Console.WriteLine(new string('═', 60) + "\n");
            
            var dataManager = new DataManager(
                new MemoryStore(),
                new CloudStore()
            );
            
            dataManager.PrintStatus();
            dataManager.SafeSave("user_1", "أحمد محمد");
            dataManager.SafeSave("user_2", "فاطمة علي");
            dataManager.SafeLoad("user_1");
            
            Console.WriteLine("\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  ✅ انتهت جميع التمارين");
            Console.WriteLine("═══════════════════════════════════════════════════════════\n");
        }
    }
}