// ====================================
// مثال واقعي: ASP.NET Core
// ====================================
public class RealWorldExample
{
    public static void ShowAspNetCorePattern()
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("مثال واقعي: ASP.NET Core Pattern");
        Console.WriteLine(new string('=', 80) + "\n");

        Console.WriteLine("في ASP.NET Core:");
        Console.WriteLine(@"
public void ConfigureServices(IServiceCollection services)
{
    // ✅ Singleton - نفس الـ Instance في كل الـ Application
    services.AddSingleton<ILogger, Logger>();
    services.AddSingleton<IConfiguration, Configuration>();
    services.AddSingleton<IMemoryCache, MemoryCache>();

    // ✅ Scoped - Instance جديد لكل HTTP Request
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<DbContext, ApplicationDbContext>();

    // ✅ Transient - Instance جديد كل مرة
    services.AddTransient<IEmailService, EmailService>();
    services.AddTransient<INotificationService, NotificationService>();
}
");

        Console.WriteLine("كل HTTP Request = Scope جديد");
        Console.WriteLine("- Singleton: نفس الـ Logger لكل الـ Requests");
        Console.WriteLine("- Scoped: DbContext جديد لكل Request (مهم جداً!)");
        Console.WriteLine("- Transient: EmailService جديد كل ما ننادي عليه");

        Console.WriteLine("\n" + new string('=', 80) + "\n");
    }
}
