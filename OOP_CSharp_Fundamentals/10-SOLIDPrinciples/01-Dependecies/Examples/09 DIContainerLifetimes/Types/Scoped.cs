// ====================================
// 2️⃣ Scoped - Instance واحد لكل Request/Scope
// ====================================
public class ScopedUserRepository : IUserRepository
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    private readonly ILogger _logger;

    public ScopedUserRepository(ILogger logger)
    {
        _logger = logger;
        Console.WriteLine($"[SCOPED] UserRepository created: {InstanceId}");
    }

    public void Save(string user)
    {
        _logger.Log($"UserRepository {InstanceId} saving user: {user}");
        Console.WriteLine($"[{InstanceId}] User saved: {user}");
    }
}

// ✅ متى نستخدم Scoped:
// - DbContext (Entity Framework)
// - Unit of Work
// - Repositories (في نفس الـ Request)
// - أي شيء محتاج يحافظ على State داخل Request واحد
