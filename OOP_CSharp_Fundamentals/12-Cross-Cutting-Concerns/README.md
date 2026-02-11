# 📌 Cross-Cutting Concerns

**Category:** Architecture Principle
**Level:** Intermediate

---

## 🧠 What is it?

ال Cross-Cutting Concerns هي **وظائف** أو **مسؤوليات** تنتشر عبر **عدة طبقات** وعدة modules في التطبيق.

**أمثلة شائعة:**

- ال **Logging**: تسجيل العمليات في كل الطبقات
- ال **Security/Authorization**: فحص الصلاحيات في كل مكان
- ال **Caching**: التخزين المؤقت للبيانات
- ال **Error Handling**: معالجة الأخطاء
- ال **Transaction Management**: إدارة المعاملات
- ال **Validation**: التحقق من البيانات
- ال **Performance Monitoring**: قياس الأداء

**الفكرة الجوهرية:**
هذه المسؤوليات **ليست جزء من Business Logic**، لكنها **ضرورية** لعمل التطبيق.

المشكلة: إذا كتبتها في كل مكان → **Code Duplication** و **Tangled Code**.

---

## ❓ Why does it exist?

ظهر المفهوم لحل مشكلة **Tangled Code**:

- **المشكلة الأصلية**: كود Logging, Security, Caching **مكرر** في كل Method
- **النتيجة**: Business Logic **مختلطة** بـ Infrastructure Code
- **الحل**: **فصل** Cross-Cutting Concerns عن Business Logic

**العواقب لو تجاهلناه:**

- ال **Code Duplication**: نفس كود Logging في 100 مكان
- ال **Maintenance Nightmare**: تغيير Logging يتطلب تعديل 100 ملف
- ال **Tangled Code**: ال Business Logic مخفية وسط Logging/Security
- ال **Violation of SRP**: كل Method يفعل 5 أشياء

---

## 🧱 Where does it belong?

ال Cross-Cutting Concerns **ليست Layer محددة** - تعمل **عبر كل الطبقات**:

```
┌───────────────────────────────────┐
│      Presentation Layer           │ ← Logging, Auth
├───────────────────────────────────┤
│      Business Logic Layer         │ ← Logging, Caching, Validation
├───────────────────────────────────┤
│      Data Access Layer            │ ← Logging, Transactions
└───────────────────────────────────┘
        ↕️ ↕️ ↕️
    Cross-Cutting Concerns
    (Logging, Security, Caching...)
```

**الحلول الشائعة:**

1. **AOP (Aspect-Oriented Programming)**: استخدام Attributes/Decorators

2. **Middleware**: في ASP.NET Core

3. **Decorators**: ال Design Pattern

4. **Filters**: في MVC

---

## ✅ When to use it

### الحالات المناسبة:

ا 1. **Repetitive Infrastructure Code**: نفس الكود يتكرر في أماكن كثيرة

ا 2. **Non-Business Functionality**: ال Logging, Security, etc.

ا 3. **Need for Centralization**: تغيير واحد يؤثر على كل المشروع

ا 4. **Separation of Concerns**: فصل Business Logic عن Infrastructure

### أمثلة:

✅ استخدام AOP لـ Logging

```csharp
[Log]
public void ProcessOrder(Order order)
{
    // ✅ Business Logic فقط - Logging تلقائي
    ValidateOrder(order);
    CalculateTotal(order);
    SaveOrder(order);
}
```

---

## ❌ When NOT to use it

### متى تتجنبه:

ف 1. **Over-Abstraction**: استخدام AOP لكل شيء صغير

ف 2. **Simple Applications**: تطبيقات صغيرة لا تحتاج التعقيد

ف 3. **Performance-Critical Code**: ال AOP يضيف Overhead

ف 4. **Team Unfamiliarity**: الفريق لا يفهم AOP

### Over-Engineering:

❌ استخدام AOP لشيء بسيط جداً

```csharp
[Log]
[Validate]
[Cache]
[Authorize]
[Transaction]
[PerformanceMonitor]
public int Add(int a, int b)
{
    return a + b; // ❗ Business Logic بسيطة جداً!
}
```

---

## 🔥 Common Mistakes

### Mistake #1: Mixing Cross-Cutting Concerns with Business Logic

❌ ال Business Logic مختلطة بـ Logging و Exception Handling

```csharp
public void ProcessPayment(Payment payment)
{
    _logger.LogInformation("Processing payment {Id}", payment.Id);

    try
    {
        if (payment.Amount <= 0)
        {
            _logger.LogWarning("Invalid amount");
            throw new ArgumentException("Amount must be positive");
        }

        _logger.LogInformation("Validating card");
        var isValid = _cardValidator.Validate(payment.CardNumber);

        if (!isValid)
        {
            _logger.LogWarning("Invalid card");
            throw new InvalidOperationException("Card validation failed");
        }

        _logger.LogInformation("Charging card");
        _paymentGateway.Charge(payment);

        _logger.LogInformation("Payment processed successfully");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Payment processing failed");
        throw;
    }
}
```

**Why it's dangerous:**

- ال Business Logic **مخفية** وسط Logging
- ال **Code Duplication**: نفس Try-Catch في كل Method
- ال **Hard to Read**: صعب فهم ماذا يفعل الكود فعلياً
- ال **Violation of SRP**: ال Method واحدة تفعل 10 أشياء

---

### Mistake #2: Duplicating Security Checks

❌ نفس Authorization Code مكرر في كل Controller Action

```csharp

public IActionResult CreateOrder(OrderDto dto)
{
    if (!User.IsInRole("Customer"))
        return Forbid();

    // Business Logic...
}

public IActionResult UpdateOrder(int id, OrderDto dto)
{
    if (!User.IsInRole("Customer"))
        return Forbid();

    // Business Logic...
}

public IActionResult DeleteOrder(int id)
{
    if (!User.IsInRole("Admin"))
        return Forbid();

    // Business Logic...
}
```

**Why it's dangerous:**

- ال **Code Duplication**: نفس الكود في كل Action
- ال **Easy to Forget**: سهل نسيان الفحص في Action جديدة
- ال **Hard to Change**: تغيير Policy يتطلب تعديل كل الـ Actions

---

### Mistake #3: Manual Transaction Management

❌ ال Transaction Management يدوي في كل Service Method

```csharp

public void TransferMoney(int fromId, int toId, decimal amount)
{
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        var from = _context.Accounts.Find(fromId);
        var to = _context.Accounts.Find(toId);

        from.Balance -= amount;
        to.Balance += amount;

        _context.SaveChanges();
        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}

public void ProcessOrder(Order order)
{
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        // ... Business Logic
        _context.SaveChanges();
        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}
```

**Why it's dangerous:**

- ال **Code Duplication**: نفس Transaction Code في كل مكان
- ال **Easy to Forget**: سهل نسيان Transaction
- ال **Hard to Maintain**: تغيير Transaction Logic صعب

---

## 🧪 Code Example — ❌ Bad Example

❌ ال Cross-Cutting Concerns مختلطة بـ Business Logic

```csharp

public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderRepository _repository;
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;

    public Order GetById(int id)
    {
        // ❌ Logging Manual
        _logger.LogInformation("Getting order {OrderId}", id);

        try
        {
            // ❌ Caching Manual
            var cacheKey = $"order_{id}";
            if (_cache.TryGetValue(cacheKey, out Order cachedOrder))
            {
                _logger.LogInformation("Order found in cache");
                return cachedOrder;
            }

            // ❌ Authorization Manual
            if (!_currentUser.HasPermission("ViewOrder"))
            {
                _logger.LogWarning("Unauthorized access attempt");
                throw new UnauthorizedException();
            }

            var order = _repository.GetById(id);

            if (order == null)
            {
                _logger.LogWarning("Order not found");
                throw new NotFoundException();
            }

            // ❌ Caching Manual
            _cache.Set(cacheKey, order, TimeSpan.FromMinutes(10));

            _logger.LogInformation("Order retrieved successfully");
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting order");
            throw;
        }
    }

    public void CreateOrder(Order order)
    {
        _logger.LogInformation("Creating order");

        // ❌ Transaction Manual
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            // ❌ Authorization Manual
            if (!_currentUser.HasPermission("CreateOrder"))
            {
                _logger.LogWarning("Unauthorized");
                throw new UnauthorizedException();
            }

            // ❌ Validation Manual
            if (order.Items == null || !order.Items.Any())
            {
                _logger.LogWarning("Invalid order - no items");
                throw new ValidationException("Order must have items");
            }

            _repository.Add(order);

            // ❌ Cache Invalidation Manual
            _cache.Remove($"orders_list");

            transaction.Commit();
            _logger.LogInformation("Order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            transaction.Rollback();
            throw;
        }
    }
}
```

### ❗ Why this is wrong

- ال **Code Duplication**: ال Logging, Caching, Authorization مكررة في كل Method
- ال **Tangled Code**: ال Business Logic مخفية وسط Infrastructure
- ال **Hard to Maintain**: تغيير Logging Format يتطلب تعديل كل الـ Methods
- ال **Violation of SRP**: كل Method تفعل 10 أشياء
- ال **Not Reusable**: لا يمكن إعادة استخدام Logging/Caching Logic

---

## 🧪 Code Example — ✅ Good Example

✅ استخدام AOP مع Attributes

```csharp

// ✅ Logging Aspect
public class LogAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<LogAttribute>>();

        var methodName = context.ActionDescriptor.DisplayName;
        logger.LogInformation("Executing {Method}", methodName);

        var result = await next();

        if (result.Exception == null)
            logger.LogInformation("{Method} completed successfully", methodName);
        else
            logger.LogError(result.Exception, "{Method} failed", methodName);
    }
}

// ✅ Caching Aspect
public class CacheAttribute : Attribute
{
    public int DurationMinutes { get; set; } = 10;
}

public class CacheInterceptor : IInterceptor
{
    private readonly IMemoryCache _cache;

    public void Intercept(IInvocation invocation)
    {
        var cacheAttr = invocation.Method
            .GetCustomAttribute<CacheAttribute>();

        if (cacheAttr == null)
        {
            invocation.Proceed();
            return;
        }

        var cacheKey = GenerateCacheKey(invocation);

        if (_cache.TryGetValue(cacheKey, out var cachedResult))
        {
            invocation.ReturnValue = cachedResult;
            return;
        }

        invocation.Proceed();

        _cache.Set(cacheKey, invocation.ReturnValue,
            TimeSpan.FromMinutes(cacheAttr.DurationMinutes));
    }
}

// ✅ Authorization Aspect
public class RequirePermissionAttribute : Attribute
{
    public string Permission { get; }

    public RequirePermissionAttribute(string permission)
    {
        Permission = permission;
    }
}

// ✅ Transaction Aspect
public class TransactionalAttribute : Attribute { }

public class TransactionInterceptor : IInterceptor
{
    private readonly AppDbContext _context;

    public void Intercept(IInvocation invocation)
    {
        var hasTransactional = invocation.Method
            .GetCustomAttribute<TransactionalAttribute>() != null;

        if (!hasTransactional)
        {
            invocation.Proceed();
            return;
        }

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            invocation.Proceed();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}

// ✅ Clean Service - فقط Business Logic!

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    [Log] // ✅ Logging تلقائي
    [Cache(DurationMinutes = 10)] // ✅ Caching تلقائي
    [RequirePermission("ViewOrder")] // ✅ Authorization تلقائي
    public virtual Order GetById(int id)
    {
        // ✅ فقط Business Logic!
        var order = _repository.GetById(id);

        if (order == null)
            throw new NotFoundException($"Order {id} not found");

        return order;
    }

    [Log] // ✅ Logging تلقائي
    [Transactional] // ✅ Transaction تلقائي
    [RequirePermission("CreateOrder")] // ✅ Authorization تلقائي
    public virtual void CreateOrder(Order order)
    {
        // ✅ فقط Business Logic!
        if (order.Items == null || !order.Items.Any())
            throw new ValidationException("Order must have items");

        order.CreatedAt = DateTime.UtcNow;
        order.Status = OrderStatus.Pending;

        _repository.Add(order);
    }
}

// ✅ Middleware للـ Global Error Handling
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Resource not found");
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed");
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
        }
    }
}
```

### ✅ Why this works

- ال **Separation of Concerns**: ال Business Logic **منفصلة** عن Infrastructure
- ال **No Duplication**: ال Logging, Caching, etc. مكتوبة **مرة واحدة**
- ال **Reusable**: ال Aspects قابلة لإعادة الاستخدام
- ال **Maintainable**: تغيير Logging يتم في **مكان واحد**
- ال **Readable**: ال Service Methods سهلة القراءة - فقط Business Logic
- ال **Testable**: يمكن اختبار Business Logic بدون Infrastructure

---

## 🧩 Relation to Other Concepts

| المفهوم                               | العلاقة                                   |
| ------------------------------------- | ----------------------------------------- |
| **AOP (Aspect-Oriented Programming)** | الحل الأساسي لـ Cross-Cutting Concerns    |
| **Decorator Pattern**                 | طريقة تطبيق Cross-Cutting Concerns        |
| **Middleware**                        | طريقة تطبيق في ASP.NET Core               |
| **SRP**                               | Cross-Cutting Concerns تساعد في تطبيق SRP |
| **DRY**                               | تمنع تكرار Infrastructure Code            |
| **Separation of Concerns**            | الأساس الذي تُبنى عليه                    |

---

## 🏁 Summary (Mental Model)

> **Cross-Cutting Concerns are the things your business logic needs, but isn't about.**

---

## 🚫 What this is NOT

- ❌ **ليس Business Logic**: Logging ليس جزء من قواعد الأعمال
- ❌ **ليس Layer**: تعمل **عبر** الطبقات، ليس **داخلها**
- ❌ **ليس Framework**: هو مفهوم، ليس تكنولوجيا
- ❌ **ليس Silver Bullet**: لا تستخدم AOP لكل شيء

---

## 🧠 Final Thought

> If you remember one thing, remember this:
> **If you're writing the same `try-catch` or logging code in 10 places, you need AOP.**

---

## ✍️ Author Notes

ال Cross-Cutting Concerns من أكثر المفاهيم أهمية في Enterprise Applications.
السبب: تطبيقات كبيرة تحتاج Logging, Security, Caching في كل مكان.
بدون AOP → Code Duplication و Maintenance Nightmare.
مع AOP → Clean, Maintainable, Testable Code.

ال **Golden Rule:** إذا وجدت نفسك تكتب نفس Infrastructure Code في أكثر من مكان → اعمل Aspect!
