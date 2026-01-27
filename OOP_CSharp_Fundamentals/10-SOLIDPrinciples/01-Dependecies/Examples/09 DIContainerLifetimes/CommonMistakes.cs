// ====================================
// ⚠️ Common Mistakes
// ====================================
public class CommonMistakes
{
    public static void ShowMistakes()
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("⚠️ أخطاء شائعة في Lifetimes");
        Console.WriteLine(new string('=', 80) + "\n");

        Console.WriteLine("❌ خطأ 1: Injecting Scoped Service في Singleton");
        Console.WriteLine(@"
// ❌ خطأ!
services.AddSingleton<SingletonService>();
services.AddScoped<ScopedService>();

class SingletonService
{
    // ❌ Singleton يحتفظ بـ Scoped Service!
    // هيستخدم نفس الـ Instance طول الوقت
    public SingletonService(ScopedService scoped) { }
}

✅ الحل: Singleton يستخدم Singleton أو Transient فقط
");

        Console.WriteLine("\n❌ خطأ 2: DbContext كـ Singleton");
        Console.WriteLine(@"
// ❌ كارثة!
services.AddSingleton<DbContext>();

المشكلة:
- DbContext مش Thread-Safe
- هيحصل Memory Leaks
- هتحصل Concurrency Issues

✅ الحل: DbContext دائماً Scoped
services.AddScoped<DbContext>();
");

        Console.WriteLine("\n❌ خطأ 3: Transient Services ثقيلة");
        Console.WriteLine(@"
// ❌ مش فعّال
services.AddTransient<HeavyService>(); // Constructor فيه I/O operations

المشكلة:
- Performance Issues
- زيادة في Memory

✅ الحل: استخدم Scoped أو Singleton حسب الحاجة
");

        Console.WriteLine(new string('=', 80) + "\n");
    }
}
