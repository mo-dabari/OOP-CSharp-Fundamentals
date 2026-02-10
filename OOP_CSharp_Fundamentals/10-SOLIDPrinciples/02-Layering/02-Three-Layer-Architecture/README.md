# 📌 Three-Layer Architecture

**Category:** Architecture Pattern
**Level:** Fundamental

---

## 🧠 What is it?

ال Three-Layer Architecture هو نمط معماري يقسم التطبيق إلى ثلاث طبقات منفصلة:

1. ال **Presentation Layer (UI)**: عرض البيانات والتفاعل مع المستخدم
2. ال **Business Logic Layer (BLL)**: قواعد الأعمال والمنطق
3. ال **Data Access Layer (DAL)**: الوصول للبيانات والتخزين

الفكرة الجوهرية: **عزل منطق الأعمال عن الواجهة وقاعدة البيانات**.

كل طبقة لها مسؤولية واحدة واضحة ولا تعرف تفاصيل الطبقات الأخرى.

---

## ❓ Why does it exist?

ظهر هذا النمط لحل مشاكل Two-Layer Architecture:

- **المشكلة**: منطق الأعمال موزع بين UI و DAL
- **النتيجة**: صعوبة إعادة الاستخدام والاختبار
- **الحل**: طبقة منفصلة لـ Business Rules

**العواقب لو تجاهلناه:**

- تكرار Business Logic في أماكن متعددة
- صعوبة تغيير القواعد
- ال Controllers سمينة (Objects سيئة)
- استحالة عمل Unit Tests للقواعد

---

## 🧱 Where does it belong?

```
┌──────────────────────────┐
│   Presentation (UI)      │ ← Controllers, Views, API
├──────────────────────────┤
│   Business Logic (BLL)   │ ← Services, Domain Logic, Validation
├──────────────────────────┤
│   Data Access (DAL)      │ ← Repositories, DbContext, ORM
└──────────────────────────┘
```

**التبعيات (Dependencies):**

- UI → BLL → DAL ✅ (اتجاه واحد لأسفل)
- DAL → BLL ❌ (ممنوع)
- BLL → UI ❌ (ممنوع)

---

## ✅ When to use it

### الحالات المناسبة:

1. **تطبيقات Business-Heavy**: فيها قواعد أعمال معقدة
2. ال **Multiple Clients**: Web + Mobile + Desktop يستخدمون نفس المنطق
3. **قابلية الاختبار مهمة**: Unit Tests للـ Business Logic
4. **فريق متوسط**: 3-10 مطورين
5. **تطبيقات متوسطة لكبيرة**: 10+ كيانات

### Preconditions:

- منطق أعمال واضح وقابل للعزل
- حاجة لإعادة استخدام المنطق
- متطلبات اختبار عالية

---

## ❌ When NOT to use it

### متى تتجنبه:

1. **تطبيقات CRUD بسيطة**: لا توجد Business Rules حقيقية
2. **محتاج Prototypes سريعة**: Over-engineering للمشاريع التجريبية
3. **محتاج Microservices صغيرة جداً**: قد تكفي طبقتين
4. **ال Domain-Driven Design**: قد تحتاج Clean/Hexagonal Architecture

### Over-engineering:

```csharp
// ❌ Three layers لتطبيق بسيط جداً
// كل ما يفعله: عرض قائمة مستخدمين

// UserController
public IActionResult GetUsers()
    => Ok(_userService.GetAll());

// UserService
public List<User> GetAll()
    => _repository.GetAll();

// UserRepository
public List<User> GetAll()
    => _context.Users.ToList();

// ❗ لا توجد أي Business Logic - طبقة زائدة!
```

---

## 🔥 Common Mistakes

### Mistake #1: Anemic Business Layer

طبقة BLL فارغة تعمل كـ Pass-Through فقط.

```csharp
// ❌ Service لا يفعل شيء
public class OrderService
{
    public Order GetById(int id)
        => _repository.GetById(id); // مجرد تمرير!

    public void Save(Order order)
        => _repository.Save(order); // لا توجد Business Logic
}
```

**Why it's dangerous:**

- طبقة زائدة بلا قيمة
- Indirection غير مبرر
- انتهاك YAGNI

---

### Mistake #2: Business Logic Leakage

تسريب Business Logic للـ UI أو DAL.

```csharp
// ❌ Controller يحتوي على Business Rules
public IActionResult ApproveOrder(int orderId)
{
    var order = _service.GetById(orderId);

    // ❌ Business Logic في Controller!
    if (order.Total > 5000 && !User.IsInRole("Manager"))
        return Forbid();

    order.Status = OrderStatus.Approved;
    _service.Update(order);
    return Ok();
}
```

**Why it's dangerous:**

- لا يمكن إعادة استخدام القاعدة
- صعوبة الاختبار
- تكرار الكود

---

### Mistake #3: Layer Skipping

UI تستدعي DAL مباشرة متجاوزة BLL.

```csharp
// ❌ Controller يستخدم Repository مباشرة
public class ProductController
{
    private readonly IProductRepository _repo; // ❌ Should be IProductService

    public IActionResult Delete(int id)
    {
        _repo.Delete(id); // ❌ تجاوز Business Layer!
        return Ok();
    }
}
```

**Why it's dangerous:**

- تخطي Validation و Business Rules
- كسر Architecture
- إمكانية حذف بيانات مهمة بدون فحص

---

### Mistake #4: Circular Dependencies

```csharp
// ❌ BLL تعتمد على UI
public class OrderService
{
    public void ProcessOrder(Order order, HttpContext context) // ❌
    {
        var userId = context.User.FindFirst("id").Value; // ❌ BLL يعرف HttpContext!
        // ...
    }
}
```

**Why it's dangerous:**

- BLL مرتبطة بـ Web Technology
- لا يمكن استخدامها في Desktop أو Mobile
- انتهاك Separation of Concerns

---

## 🧪 Code Example — ❌ Bad Example

❌ طبقات موجودة لكن مختلطة

```csharp
// UI Layer
public class OrderController
{
    private readonly OrderRepository _repo; // ❌ يجب أن يكون Service

    public IActionResult CreateOrder(OrderDto dto)
    {
        // ❌ Validation في Controller
        if (dto.Items == null || dto.Items.Count == 0)
            return BadRequest("Order must have items");

        // ❌ Business Logic في Controller
        var total = dto.Items.Sum(x => x.Price * x.Quantity);

        if (dto.CustomerType == "VIP")
            total *= 0.9m; // VIP discount

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            Total = total,
            Status = total > 1000
                ? OrderStatus.PendingApproval
                : OrderStatus.Confirmed
        };

        // ❌ استدعاء Repository مباشرة
        _repo.Add(order);
        return Ok();
    }
}

// Business Layer (Empty!)
public class OrderService
{
    // ❌ لا توجد Business Logic!
}

// Data Layer
public class OrderRepository
{
    public void Add(Order order)
    {
        // ❌ Business Logic في Repository!
        if (order.Total > 10000)
            order.RequiresManagerApproval = true;

        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}
```

### ❗ Why this is wrong

- ال **Mixed Responsibilities**: Business Logic موزعة بين UI و DAL
- ال **No Reusability**: لا يمكن استخدام نفس المنطق من Mobile App
- ال **Untestable**: لا يمكن اختبار Business Rules بدون Database
- ال **Violation of Layering**: UI تستدعي DAL مباشرة

---

## 🧪 Code Example — ✅ Good Example

```csharp
// ✅ Presentation Layer
public class OrderController
{
    private readonly IOrderService _service;

    public IActionResult CreateOrder(OrderDto dto)
    {
        // ✅ فقط Validation و تحويل DTO
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateOrderCommand
        {
            CustomerId = dto.CustomerId,
            Items = dto.Items,
            CustomerType = dto.CustomerType
        };

        // ✅ استدعاء Business Layer
        var result = _service.CreateOrder(command);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Data);
    }
}

// ✅ Business Logic Layer
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public Result<Order> CreateOrder(CreateOrderCommand command)
    {
        // ✅ Business Validation
        if (command.Items == null || !command.Items.Any())
            return Result<Order>.Failure("Order must contain items");

        // ✅ Business Logic
        var total = CalculateTotal(command.Items);

        // ✅ Business Rules
        if (command.CustomerType == "VIP")
            total = ApplyVipDiscount(total);

        var status = DetermineOrderStatus(total);

        var order = new Order
        {
            CustomerId = command.CustomerId,
            Total = total,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };

        // ✅ استدعاء Data Layer
        _repository.Add(order);

        return Result<Order>.Success(order);
    }

    private decimal CalculateTotal(List<OrderItemDto> items)
    {
        return items.Sum(x => x.Price * x.Quantity);
    }

    private decimal ApplyVipDiscount(decimal total)
    {
        return total * 0.9m; // 10% discount
    }

    private OrderStatus DetermineOrderStatus(decimal total)
    {
        return total > 1000
            ? OrderStatus.PendingApproval
            : OrderStatus.Confirmed;
    }
}

// ✅ Data Access Layer
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public void Add(Order order)
    {
        // ✅ فقط Data Access - لا Business Logic
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public Order? GetById(int id)
    {
        return _context.Orders.Find(id);
    }
}

// ✅ Interfaces
public interface IOrderService
{
    Result<Order> CreateOrder(CreateOrderCommand command);
}

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int id);
}
```

### ✅ Why this works

- ال **Clear Separation**: كل طبقة لها مسؤولية واضحة
- ال **Testable**: يمكن اختبار OrderService بدون Database
- ال **Reusable**: يمكن استخدام OrderService من أي Client
- ال **Maintainable**: تغيير Business Rules في مكان واحد
- ال **Dependencies Flow Down**: UI → BLL → DAL

---

## 🧩 Relation to Other Concepts

| المفهوم                    | العلاقة                               |
| -------------------------- | ------------------------------------- |
| **Two-Layer Architecture** | الخطوة السابقة - Three-Layer تطوير له |
| **Clean Architecture**     | الخطوة التالية - عكس Dependencies     |
| **Service Layer Pattern**  | BLL غالباً تُطبق كـ Services          |
| **Repository Pattern**     | DAL غالباً تستخدم Repositories        |
| **SRP**                    | كل طبقة مسؤولية واحدة                 |
| **Dependency Inversion**   | استخدام Interfaces بين الطبقات        |
| **CQRS**                   | يمكن تطبيقه داخل BLL                  |

---

## 🏁 Summary (Mental Model)

> **Three-Layer is about isolating WHAT the system does from HOW it shows it and WHERE it stores it.**

---

## 🚫 What this is NOT

- ❌ **ليس Clean Architecture**: Dependencies هنا تتجه لأسفل، ليس للداخل
- ❌ **ليس Domain-Driven Design**: لا يوجد Rich Domain Model
- ❌ **ليس مناسب لكل شيء**: التطبيقات البسيطة لا تحتاجه
- ❌ **ليس Framework**: هو مجرد طريقة تنظيم

---

## 🧠 Final Thought

> If you remember one thing, remember this:
> **If your Controllers have `if` statements about business rules, you need a Business Layer.**

---

## ✍️ Author Notes

ال Three-Layer هو المعيار الصناعي للتطبيقات المتوسطة.
أغلب المشاريع التجارية تبدأ هنا.
لو كبرت أكتر → Clean Architecture.
لو صغيرة جداً → Two-Layer يكفي.
