# 📌 Leaky Abstraction

**Category:** Architecture Smell
**Level:** Intermediate

---

## 🧠 What is it?

ال Leaky Abstraction هو عيب معماري يحدث عندما تكشف الطبقة العليا (Abstraction) تفاصيل التنفيذ (Implementation) الخاصة بالطبقة السفلى.

**الفكرة الجوهرية:**

- الـ Interface يجب أن يكون **مستقل** عن التكنولوجيا المستخدمة
- المستخدم لا يجب أن **يعرف** كيف تم التنفيذ
- التسريب يحدث عندما **تتسرب** تفاصيل Implementation للخارج

**مثال واقعي:**
عندما تستخدم مصعد، أنت تضغط زر الطابق ولا تحتاج معرفة نوع المحرك أو نظام الكابلات.
لو اضطررت لمعرفة نوع المحرك لتشغيله → Leaky Abstraction.

---

## ❓ Why does it exist?

ظهرت المشكلة مع تعقيد الأنظمة:

- **المشكلة الأصلية**: ربط الكود بتكنولوجيا معينة (SQL Server, MongoDB, etc.)
- **النتيجة**: استحالة تغيير التكنولوجيا بدون تكسير كل شيء
- **الحل**: إخفاء التفاصيل خلف Abstraction

**العواقب لو تجاهلناه:**

- ال **Tight Coupling**: كل طبقة تعتمد على تفاصيل الطبقة السفلى
- ال **Impossibility to Change**: لا يمكن تغيير Database أو Technology
- ال **Testing Nightmare**: صعوبة عمل Mocks و Unit Tests
- ال **Violation of Dependency Inversion**: الاعتماد على Concrete بدلاً من Abstraction

---

## 🧱 Where does it belong?

ال Leaky Abstraction **ليس Layer** - هو **Smell** يظهر في **أي مكان** تستخدم فيه Interfaces:

```
┌─────────────────────────┐
│   Business Layer        │
│   ❌ يعرف تفاصيل SQL   │ ← Leaky!
├─────────────────────────┤
│   Data Access Layer     │
│   (SQL Server)          │
└─────────────────────────┘
```

**الأماكن الشائعة:**

- ال Repository Interfaces تكشف `<IQueryable<T>` أو `DbSet<T>`
- ال Service Interfaces تكشف `HttpContext` أو `ClaimsPrincipal`
- ال DTOs تحتوي على `[JsonProperty]` أو `[XmlElement]`

---

## ✅ When to use it (Prevention)

### كيف تتجنب Leaky Abstraction:

1. **استخدم Generic Types**: بدلاً من Technology-Specific Types
2. ال **Return Plain Objects**: تجنب إرجاع `IQueryable` أو `DbSet`
3. ال **Hide Implementation Details**: لا تكشف تفاصيل التكنولوجيا
4. ال **Design by Contract**: الـ Interface يحدد ماذا، ليس كيف

### القاعدة الذهبية:

> **If changing the implementation breaks the interface consumer, it's leaky.**

---

## ❌ When NOT to use it

لا ينطبق - Leaky Abstraction هو **Smell** يجب تجنبه دائماً.

لكن هناك استثناءات نادرة:

### استثناءات مقبولة:

1. ال **Performance-Critical Code**: أحياناً التسريب يكون ضروري للأداء
2. ال **Framework Limitations**: بعض الـ ORMs تتطلب `IQueryable`
3. ال **Pragmatic Trade-offs**: في مشاريع صغيرة جداً

**لكن حتى هذه الحالات يجب توثيقها والتحذير منها!**

---

## 🔥 Common Mistakes

### Mistake #1: Exposing `IQueryable<T>`

إرجاع `IQueryable` من Repository يكشف أن التنفيذ يستخدم LINQ/EF.

```csharp
// ❌ Interface يكشف أننا نستخدم EF
public interface IProductRepository
{
    IQueryable<Product> GetAll(); // ❌ Leaky!
}
```

**Why it's dangerous:**

- ال Consumer يعرف أن الـ Implementation تستخدم LINQ
- لو غيرت لـ Dapper أو MongoDB → Interface يتكسر
- ال Consumer يمكنه كتابة `.Where()` خارج Repository

---

### Mistake #2: Technology-Specific Attributes in DTOs

```csharp
// ❌ DTO تعرف تفاصيل JSON Serialization
public class ProductDto
{
    [JsonProperty("product_id")] // ❌ Leaky!
    public int Id { get; set; }

    [JsonIgnore] // ❌ Leaky!
    public decimal Cost { get; set; }
}
```

**Why it's dangerous:**

- ال DTO مرتبطة بـ Newtonsoft.Json
- لو غيرت لـ System.Text.Json → يجب تغيير Attributes
- خلط Concerns (Data + Serialization)

---

### Mistake #3: Exposing `DbContext` or `SqlConnection`

❌ ال Service يكشف تفاصيل Database

```csharp
public interface IOrderService
{
    void ProcessOrder(Order order, SqlTransaction transaction); // ❌ Leaky!
}
```

**Why it's dangerous:**

- ال Service مرتبطة بـ SQL Server
- لا يمكن استخدام PostgreSQL أو NoSQL
- انتهاك Abstraction

---

### Mistake #4: HTTP-Specific Types in Business Layer

❌ ال Business Layer تعرف تفاصيل HTTP

```csharp
public class UserService
{
    public User GetCurrentUser(HttpContext context) // ❌ Leaky!
    {
        var userId = context.User.FindFirst("id").Value;
        return _repository.GetById(int.Parse(userId));
    }
}
```

**Why it's dangerous:**

- ال Service مرتبطة بـ ASP.NET Core
- لا يمكن استخدامها في Console App أو Desktop
- انتهاك Separation of Concerns

---

### Mistake #5: Exception Types Reveal Implementation

❌ استثناءات تكشف Database

```csharp
public interface ICustomerRepository
{
    Customer GetById(int id) throws SqlException; // ❌ Leaky!
}
```

**Why it's dangerous:**

- المستخدم يعرف أننا نستخدم SQL Server
- تغيير Database يتطلب تغيير Exception Handling
- تسريب تفاصيل Implementation

---

## 🧪 Code Example — ❌ Bad Example

```csharp
// ❌ Repository Interface - Leaky Abstraction

public interface IProductRepository
{
    // ❌ يكشف استخدام EF/LINQ
    IQueryable<Product> GetAll();

    // ❌ يكشف استخدام EF
    DbSet<Product> Products { get; }

    // ❌ يكشف SQL-specific behavior
    void BulkInsert(DataTable products);
}

// Usage في Business Layer
public class ProductService
{
    private readonly IProductRepository _repo;

    public List<Product> GetExpensiveProducts()
    {
        // ❌ Business Layer تكتب LINQ queries!
        return _repo.GetAll()
            .Where(p => p.Price > 1000)
            .OrderBy(p => p.Name)
            .ToList();

        // ❗ لو غيرنا Repository لـ REST API, هذا الكود يتكسر!
    }
}

// ❌ DTO with Technology-Specific Attributes
public class OrderDto
{
    [JsonProperty("order_id")] // ❌ مرتبط بـ JSON
    public int Id { get; set; }

    [Column("customer_fk")] // ❌ مرتبط بـ Database
    public int CustomerId { get; set; }

    [NotMapped] // ❌ مرتبط بـ EF
    public decimal TotalWithTax => Total * 1.14m;
}

// ❌ Service مع HTTP Dependencies
public class AuthService
{
    public bool IsAuthorized(HttpContext context, string permission)
    {
        // ❌ Business Logic مرتبطة بـ HTTP
        var roles = context.User.FindAll(ClaimTypes.Role);
        return roles.Any(r => r.Value == permission);
    }
}
```

### ❗ Why this is wrong

- ال **Tight Coupling**: Business Layer مرتبطة بـ EF, JSON, HTTP
- ال **Cannot Change**: تغيير Database أو Web Framework يكسر كل شيء
- ال **Testing Nightmare**: صعوبة عمل Unit Tests بدون HTTP Context
- ال **Violation of DIP**: الاعتماد على Concrete Types
- ل **Mixed Concerns**: Business Logic تعرف تفاصيل Infrastructure

---

## 🧪 Code Example — ✅ Good Example

```csharp
// ✅ Clean Repository Interface

public interface IProductRepository
{
    // ✅ Generic - لا يكشف التكنولوجيا
    List<Product> GetAll();

    // ✅ مع فلترة محددة
    List<Product> GetByPriceRange(decimal min, decimal max);

    // ✅ واضح ومستقل
    Product? GetById(int id);

    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}

// Implementation
public class EfProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public List<Product> GetAll()
    {
        // ✅ نرجع List, ليس IQueryable
        return _context.Products.ToList();
    }

    public List<Product> GetByPriceRange(decimal min, decimal max)
    {
        // ✅ ال Query logic مخفية داخل Repository
        return _context.Products
            .Where(p => p.Price >= min && p.Price <= max)
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}

// ✅ Business Layer
public class ProductService
{
    private readonly IProductRepository _repo;

    public List<Product> GetExpensiveProducts()
    {
        // ✅ استخدام Repository Method محددة
        return _repo.GetByPriceRange(1000, decimal.MaxValue);

        // ✅ لو غيرنا Implementation (REST API, MongoDB)
        // الكود هنا لا يتأثر!
    }
}

// ✅ Clean DTO - No Technology-Specific Attributes
public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

// ✅ Mapping Configuration منفصلة
public class OrderDtoMappingProfile : Profile
{
    public OrderDtoMappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderId));
    }
}

// ✅ Auth Service - No HTTP Dependencies
public class AuthService
{
    public bool IsAuthorized(UserContext userContext, string permission)
    {
        // ✅ Business Logic تعمل مع Plain Object
        return userContext.Roles.Contains(permission);
    }
}

// ✅ UserContext - Generic Model
public class UserContext
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}

// ✅ ال Controller يحول من HTTP لـ Plain Object
public class OrderController
{
    private readonly IAuthService _authService;

    public IActionResult Delete(int id)
    {
        // ✅ نحول HttpContext لـ UserContext
        var userContext = new UserContext
        {
            UserId = int.Parse(User.FindFirst("id").Value),
            Username = User.Identity.Name,
            Roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
        };

        // ✅ Business Layer لا تعرف شيء عن HTTP
        if (!_authService.IsAuthorized(userContext, "DeleteOrder"))
            return Forbid();

        // ...
    }
}
```

### ✅ Why this works

- ال**Technology Agnostic**: Interface لا تكشف تفاصيل التنفيذ
- ال **Easy to Replace**: يمكن تغيير EF لـ Dapper أو MongoDB بدون تأثير
- ال **Testable**: يمكن عمل Mock بسهولة
- ال **Separation of Concerns**: كل طبقة مسؤولة عن شيء واحد
- ال **Follows DIP**: الاعتماد على Abstraction

---

## 🧩 Relation to Other Concepts

| المفهوم                            | العلاقة                            |
| ---------------------------------- | ---------------------------------- |
| **Dependency Inversion Principle** | Leaky Abstraction ينتهك DIP        |
| **Interface Segregation**          | ISP يساعد في منع Leaky Abstraction |
| **Separation of Concerns**         | Leaky Abstraction يخلط المسؤوليات  |
| **Repository Pattern**             | يجب أن يكون Non-Leaky              |
| **Clean Architecture**             | يحارب Leaky Abstraction بقوة       |
| **Adapter Pattern**                | يستخدم لإخفاء التفاصيل             |

---

## 🏁 Summary (Mental Model)

> **An abstraction is leaky if you need to know HOW it works to USE it.**

---

## 🚫 What this is NOT

- ❌ **ليس Encapsulation**: Encapsulation عن hiding data، Leaky عن hiding implementation
- ❌ **ليس Performance Optimization**: التسريب ليس تحسين، هو عيب
- ❌ **ليس Pragmatism**: "يعمل حالياً" ليس عذر لـ Bad Design
- ❌ **ليس Framework Requirement**: حتى ORMs يمكن استخدامها بدون تسريب

---

## 🧠 Final Thought

> If you remember one thing, remember this:
> **The moment you return `IQueryable`, `DbSet`, or `HttpContext` from a service, you've leaked.**

---

## ✍️ Author Notes

ال Leaky Abstraction من أكثر المشاكل شيوعاً في المشاريع الحقيقية.
السبب: السهولة المؤقتة (إرجاع `IQueryable` أسهل من تحديد Methods).
لكن الثمن: Tight Coupling و صعوبة التغيير لاحقاً.

ال **Golden Rule:** إذا غيرت Database/Framework وانكسر Interface Consumer → Leaky!
