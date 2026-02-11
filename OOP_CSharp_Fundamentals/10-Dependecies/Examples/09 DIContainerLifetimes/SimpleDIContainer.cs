using DIContainerLifetimes.Types.Scoped;
using DIContainerLifetimes.Types.Singleton;

namespace DIContainerLifetimes
{
    // ====================================
    // Simple DI Container
    // ====================================
    public class SimpleDIContainer
    {
        private readonly Dictionary<Type, Func<object>> _transientServices = new();
        private readonly Dictionary<Type, Func<object>> _scopedFactories = new();
        private readonly Dictionary<Type, object> _singletonInstances = new();

        // Register Singleton
        public void RegisterSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface, new()
        {
            _singletonInstances[typeof(TInterface)] = new TImplementation();
        }

        // Register Scoped
        public void RegisterScoped<TInterface>(Func<object> factory)
        {
            _scopedFactories[typeof(TInterface)] = factory;
        }

        // Register Transient
        public void RegisterTransient<TInterface>(Func<object> factory)
        {
            _transientServices[typeof(TInterface)] = factory;
        }

        // Create Scope
        public Scope CreateScope()
        {
            return new Scope(_transientServices, _scopedFactories, _singletonInstances);
        }
    }

    // Nested Scope class
    public class Scope : IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _transientServices;
        private readonly Dictionary<Type, Func<object>> _scopedFactories;
        private readonly Dictionary<Type, object> _singletonInstances;
        private readonly Dictionary<Type, object> _scopedInstances = new();

        public Scope(
            Dictionary<Type, Func<object>> transientServices,
            Dictionary<Type, Func<object>> scopedFactories,
            Dictionary<Type, object> singletonInstances)
        {
            _transientServices = transientServices;
            _scopedFactories = scopedFactories;
            _singletonInstances = singletonInstances;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);

            // Check Singleton first
            if (_singletonInstances.ContainsKey(type))
            {
                return (T)_singletonInstances[type];
            }

            // Check Scoped
            if (_scopedFactories.ContainsKey(type))
            {
                if (!_scopedInstances.ContainsKey(type))
                {
                    _scopedInstances[type] = _scopedFactories[type]();
                }
                return (T)_scopedInstances[type];
            }

            // Check Transient
            if (_transientServices.ContainsKey(type))
            {
                return (T)_transientServices[type]();
            }

            throw new Exception($"Service {type.Name} not registered");
        }

        public void Dispose()
        {
            _scopedInstances.Clear();
            Console.WriteLine("[SCOPE] Disposed");
        }
    }

    public class TransientEmailService : IEmailService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
        private readonly ILogger _logger;

        public TransientEmailService(ILogger logger)
        {
            _logger = logger;
            Console.WriteLine($"[TRANSIENT] EmailService created: {InstanceId}");
        }

        public void Send(string email, string message)
        {
            _logger.Log($"EmailService {InstanceId} sending to: {email}");
            Console.WriteLine($"[{InstanceId}] Email sent to {email}: {message}");
        }
    }
    // ====================================
    // Demo Program
    // ====================================
    public class Program
    {
        public static void Demo()
        {
            Console.WriteLine(new string('=', 80));
            Console.WriteLine("DI Container Lifetimes Demo");
            Console.WriteLine(new string('=', 80));

            // إنشاء Container وتسجيل Services
            var container = new SimpleDIContainer();

            // Singleton - نفس الـ Instance دائماً
            container.RegisterSingleton<ILogger, SingletonLogger>();

            // Scoped - Instance واحد لكل Scope
            ILogger loggerForScoped = null;
            container.RegisterScoped<IUserRepository>(
                () => new ScopedUserRepository(loggerForScoped = container.CreateScope().Resolve<ILogger>())
            );

            // Transient - Instance جديد كل مرة
            container.RegisterTransient<IEmailService>(
                () => new TransientEmailService(container.CreateScope().Resolve<ILogger>())
            );

            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine("Request 1 - First Scope");
            Console.WriteLine(new string('-', 80));

            using (var scope1 = container.CreateScope())
            {
                var logger1 = scope1.Resolve<ILogger>();
                var repo1 = scope1.Resolve<IUserRepository>();
                var email1 = scope1.Resolve<IEmailService>();

                Console.WriteLine("\nFirst call within scope 1:");
                Console.WriteLine($"Logger:     {logger1.InstanceId}");
                Console.WriteLine($"Repository: {repo1.InstanceId}");
                Console.WriteLine($"Email:      {email1.InstanceId}");

                // نفس الـ Scope - نطلب تاني
                var logger1b = scope1.Resolve<ILogger>();
                var repo1b = scope1.Resolve<IUserRepository>();
                var email1b = scope1.Resolve<IEmailService>();

                Console.WriteLine("\nSecond call within scope 1:");
                Console.WriteLine($"Logger:     {logger1b.InstanceId}");
                Console.WriteLine($"Repository: {repo1b.InstanceId}");
                Console.WriteLine($"Email:      {email1b.InstanceId}");

                Console.WriteLine("\n📊 ملاحظات Scope 1:");
                Console.WriteLine($"✅ Logger: {(logger1.InstanceId == logger1b.InstanceId ? "نفس الـ Instance (Singleton)" : "مختلف")}");
                Console.WriteLine($"✅ Repository: {(repo1.InstanceId == repo1b.InstanceId ? "نفس الـ Instance (Scoped)" : "مختلف")}");
                Console.WriteLine($"❌ Email: {(email1.InstanceId == email1b.InstanceId ? "نفس الـ Instance" : "Instance جديد (Transient)")}");
            }

            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine("Request 2 - Second Scope");
            Console.WriteLine(new string('-', 80));

            using (var scope2 = container.CreateScope())
            {
                var logger2 = scope2.Resolve<ILogger>();
                var repo2 = scope2.Resolve<IUserRepository>();
                var email2 = scope2.Resolve<IEmailService>();

                Console.WriteLine("\nFirst call within scope 2:");
                Console.WriteLine($"Logger:     {logger2.InstanceId}");
                Console.WriteLine($"Repository: {repo2.InstanceId}");
                Console.WriteLine($"Email:      {email2.InstanceId}");
            }

            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("الخلاصة:");
            Console.WriteLine(new string('=', 80));
            Console.WriteLine("🏆 Singleton:  نفس الـ Instance في كل الـ Scopes");
            Console.WriteLine("📦 Scoped:     Instance جديد لكل Scope، نفس الـ Instance داخل نفس الـ Scope");
            Console.WriteLine("🔄 Transient:  Instance جديد مع كل Resolve");
            Console.WriteLine(new string('=', 80) + "\n");
        }
    }
    public interface IEmailService
    {
        Guid InstanceId { get; }
        void Send(string email, string message);
    }

    public interface IUserRepository
    {
        Guid InstanceId { get; }
        public void Save(string user);
    }

    public interface ILogger
    {
        Guid InstanceId { get; }
        void Log(string message);
    }
}
