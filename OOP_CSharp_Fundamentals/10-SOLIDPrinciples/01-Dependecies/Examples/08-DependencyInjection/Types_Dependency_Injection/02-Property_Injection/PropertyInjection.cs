// ====================================
// 2️⃣ Property Injection - استخدام نادر
// ====================================
namespace PropertyInjection
{
    public interface ILogger
    {
        void Log(string message);
    }

    public interface ICache
    {
        void Set(string key, object value);
        object Get(string key);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    public class MemoryCache : ICache
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        public void Set(string key, object value) => _cache[key] = value;
        public object Get(string key) => _cache.ContainsKey(key) ? _cache[key] : null;
    }

    // ⚠️ Property Injection - للـ Optional Dependencies فقط
    public class ProductService
    {
        private readonly ILogger _logger;

        // ✅ Required Dependency في Constructor
        public ProductService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // ⚠️ Optional Dependency كـ Property
        public ICache Cache { get; set; }

        public Product GetProduct(int id)
        {
            _logger.Log($"Getting product {id}");

            // ⚠️ لازم نعمل Null Check
            if (Cache != null)
            {
                var cached = Cache.Get($"product_{id}");
                if (cached != null)
                {
                    _logger.Log("Retrieved from cache");
                    return (Product)cached;
                }
            }

            // جلب من Database
            var product = new Product { Id = id, Name = "Product " + id };

            // ⚠️ لازم نعمل Null Check تاني
            if (Cache != null)
            {
                Cache.Set($"product_{id}", product);
            }

            return product;
        }
    }

    // ❌ مشاكل Property Injection:
    public class Problems
    {
        public static void Demo()
        {
            Console.WriteLine("\n=== Property Injection Problems ===\n");

            var logger = new ConsoleLogger();
            var service = new ProductService(logger);

            // ❌ المشكلة 1: الـ Object ممكن يكون في حالة غير كاملة
            var product1 = service.GetProduct(1); // Cache = null
            Console.WriteLine("⚠️ Service working without cache");

            // ✅ بعدين نضيف الـ Cache
            service.Cache = new MemoryCache();
            var product2 = service.GetProduct(2); // Cache موجود
            Console.WriteLine("✅ Service now using cache");

            // ❌ المشكلة 2: الـ Dependency ممكن تتغير
            service.Cache = null; // ⚠️ حد غيّر الـ Cache!

            // ❌ المشكلة 3: لازم Null Check في كل مكان

            Console.WriteLine("\n⚠️ Property Injection للـ Optional Dependencies فقط\n");
        }
    }

    // ✅ متى نستخدم Property Injection:
    // 1. الـ Dependency اختياري فعلاً
    // 2. الـ Framework يفرض علينا (مثل ASP.NET WebForms)
    // 3. Backward compatibility
}
