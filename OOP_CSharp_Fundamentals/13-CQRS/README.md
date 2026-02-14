# 📌 CQRS (Command Query Responsibility Segregation)

**Category:** Architecture Pattern
**Level:** Advanced

---

## 🧠 What is it?

ال CQRS هو نمط معماري يفصل **عمليات القراءة (Queries)** عن **عمليات الكتابة (Commands)**.

**الفكرة الجوهرية:**

- ال **Commands**: تُغير حالة النظام (Create, Update, Delete) - لا ترجع بيانات
- ال **Queries**: تقرأ البيانات فقط (Read) - لا تُغير الحالة
- كل واحدة لها **مسار منفصل** و **Model منفصل**

**القاعدة الأساسية (CQS - Bertrand Meyer):**

> "A method should either change state OR return data, never both."

ال **CQRS يأخذ هذه القاعدة لمستوى Architecture:**

- ال Models منفصلة للقراءة والكتابة
- ال Database منفصلة (اختياري)
- ال Optimization منفصل لكل مسار

---

## ❓ Why does it exist?

ظهر CQRS لحل مشاكل الأنظمة المعقدة:

### المشاكل التي يحلها:

ال 1. **Complex Domain Models**:

- نفس الـ Model يُستخدم للقراءة والكتابة
- القراءة تحتاج Joins معقدة، الكتابة تحتاج Validation

ال 2. **Performance Bottlenecks**:

- القراءة 95% من العمليات، الكتابة 5%
- نفس الـ Database للاثنين → بطء

ال 3. **Different Scalability Needs**:

- القراءة تحتاج Horizontal Scaling
- الكتابة تحتاج Consistency

ال 4. **Complex Business Rules**:

- الكتابة تحتاج Validation معقدة
- القراءة تحتاج فقط عرض البيانات

**العواقب لو تجاهلناه (في الأنظمة المعقدة):**

- ال **Performance Issues**: نفس Database للقراءة والكتابة
- ال **Complex Models**: ال DTOs معقدة للغاية
- ال **Scalability Problems**: صعوبة Scale القراءة بشكل منفصل
- ال **Maintenance Nightmare**: نفس الكود يخدم حالتين مختلفتين

---

## 🧱 Where does it belong?

ال CQRS **ليس Layer** - هو **نمط معماري** يؤثر على **كل الطبقات**:

```
┌──────────────────────────────────────────────┐
│              Presentation Layer              │
│   ┌─────────────────┐  ┌─────────────────┐  │
│   │  Command API    │  │   Query API     │  │
│   └─────────────────┘  └─────────────────┘  │
├──────────────────────────────────────────────┤
│              Application Layer               │
│   ┌─────────────────┐  ┌─────────────────┐  │
│   │ Command Handler │  │  Query Handler  │  │
│   └─────────────────┘  └─────────────────┘  │
├──────────────────────────────────────────────┤
│                Domain Layer                  │
│   ┌─────────────────┐  ┌─────────────────┐  │
│   │  Write Model    │  │   Read Model    │  │
│   │  (Aggregates)   │  │     (DTOs)      │  │
│   └─────────────────┘  └─────────────────┘  │
├──────────────────────────────────────────────┤
│              Infrastructure                  │
│   ┌─────────────────┐  ┌─────────────────┐  │
│   │   Write DB      │  │    Read DB      │  │
│   │ (Normalized)    │  │ (Denormalized)  │  │
│   └─────────────────┘  └─────────────────┘  │
└──────────────────────────────────────────────┘
```

**التبعيات:**

- ال Command → Write Model → Write DB
- ال Query → Read Model → Read DB
- ال Write DB ──sync──> Read DB (eventual consistency)

---

## ✅ When to use it

### الحالات المناسبة:

1. ال **Complex Domain Logic**: قواعد أعمال معقدة في الكتابة
2. ال **High Read/Write Ratio**: ال 90% قراءة +، قليل من الكتابة
3. ال **Different Performance Needs**: القراءة تحتاج سرعة، الكتابة تحتاج Consistency
4. ال **Event Sourcing**: ال CQRS يعمل بشكل مثالي مع Event Sourcing
5. ال **Scalability Requirements**: حاجة لـ Scale القراءة بشكل مستقل
6. ال **Task-Based UI**: واجهة مبنية على Commands (إنشاء طلب، إلغاء طلب)

### Indicators:

✅ مؤشرات أنك تحتاج CQRS:

```csharp

// 1. DTOs معقدة جداً للقراءة
public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public CustomerDto Customer { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public AddressDto ShippingAddress { get; set; }
    public PaymentDto Payment { get; set; }
    // ... 50+ properties with 10+ joins
}

// 2. نفس الـ Model لحالات مختلفة تماماً
public class Order
{
    // For Write: complex validation
    public void ApproveOrder() { /* complex logic */ }

    // For Read: just display
    public decimal Total { get; set; }
}

// 3. Performance issues في القراءة
var orders = await _context.Orders
    .Include(o => o.Customer)
    .Include(o => o.Items).ThenInclude(i => i.Product)
    .Include(o => o.ShippingAddress)
    .Include(o => o.Payment)
    .ToListAsync(); // ❌ Slow!
```

---

## ❌ When NOT to use it

### متى تتجنبه:

1. ف **Simple CRUD Applications**: لا توجد Business Logic معقدة
2. ف **Small Projects**:هينتج عنه Over-engineering
3. ف **Similar Read/Write Models**: القراءة والكتابة متشابهة جداً
4. ف **Team Inexperience**: الفريق غير معتاد على CQRS
5. ف **No Performance Issues**: النظام سريع بدون CQRS

### Over-Engineering:

```csharp
// ❌ CQRS لتطبيق بسيط جداً

// Command: CreateUser
public class CreateUserCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
}

// Query: GetUserById
public class GetUserByIdQuery
{
    public int Id { get; set; }
}

// ❗ الـ Model بسيط جداً - لا حاجة لفصل!
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

**Why it's wrong:**

- ال Complexity زائدة بدون فائدة
- نفس الـ Model للقراءة والكتابة
- لا توجد مشاكل Performance
- YAGNI violation

---

## 🔥 Common Mistakes

### Mistake #1: Commands Returning Data

❌ ال Command يرجع بيانات

```csharp
public class CreateOrderCommand
{
    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class CreateOrderHandler
{
    public Order Handle(CreateOrderCommand cmd) // ❌ يرجع Order!
    {
        var order = new Order
        {
            CustomerId = cmd.CustomerId,
            Items = cmd.Items
        };

        _repository.Add(order);
        return order; // ❌ انتهاك CQS!
    }
}
```

**Why it's dangerous:**

- انتهاك مبدأ CQS
- خلط Command و Query
- يصعب تطبيق Event Sourcing لاحقاً

**✅ الحل الصحيح:**

```csharp
public class CreateOrderHandler
{
    public int Handle(CreateOrderCommand cmd) // ✅ يرجع ID فقط
    {
        var order = new Order { /* ... */ };
        _repository.Add(order);
        return order.Id; // ✅ فقط Identifier
    }
}
```

---

### Mistake #2: Queries Modifying State

❌ ال Query تُغير البيانات

```csharp
public class GetProductByIdQuery
{
    public int ProductId { get; set; }
}

public class GetProductByIdHandler
{
    public ProductDto Handle(GetProductByIdQuery query)
    {
        var product = _repository.GetById(query.ProductId);

        // ❌ Query تُعدّل البيانات!
        product.ViewCount++;
        _repository.Update(product);

        return _mapper.Map<ProductDto>(product);
    }
}
```

**Why it's dangerous:**

- انتهاك CQS
- ال Query غير Idempotent
- ال Side Effects غير متوقعة

**✅ الحل الصحيح:**
✅ ال Query منفصلة عن Command

```csharp
public class GetProductByIdHandler
{
    public ProductDto Handle(GetProductByIdQuery query)
    {
        var product = _repository.GetById(query.ProductId);
        return _mapper.Map<ProductDto>(product);
        // ✅ لا تُعدّل البيانات
    }
}

// ✅ ال Command منفصلة لتحديث ViewCount
public class IncrementProductViewCommand
{
    public int ProductId { get; set; }
}
```

---

### Mistake #3: Shared Models Between Read and Write

❌ نفس الـ Model للقراءة والكتابة

```csharp
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }

    // For Write: complex validation
    public void ApproveOrder()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException();

        Status = OrderStatus.Approved;
    }

    // For Read: display properties
    public string CustomerName { get; set; }
    public string StatusDisplay { get; set; }
}

// ❌ نستخدمه في Command و Query
public class CreateOrderCommand { public Order Order { get; set; } }
public class GetOrderQuery { public int Id { get; set; } }
public class GetOrderHandler
{
    public Order Handle(GetOrderQuery q) // ❌ نفس الـ Model!
    {
        return _repository.GetById(q.Id);
    }
}
```

**Why it's dangerous:**

- خلط Write Model مع Read Model
- ال Properties زائدة (CustomerName للكتابة؟)
- صعوبة Optimization

**✅ الحل الصحيح:**

```csharp
// ✅ Write Model (Rich Domain Model)
public class Order // For Commands
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    private List<OrderItem> _items = new();

    public void ApproveOrder()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException();

        Status = OrderStatus.Approved;
    }
}

// ✅ Read Model (Simple DTO)
public class OrderDto // For Queries
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
```

---

### Mistake #4: Same Database for Read and Write

❌ نفس DbContext للـ Commands و Queries

```csharp
public class OrderCommandHandler
{
    private readonly AppDbContext _context; // ❌

    public void Handle(CreateOrderCommand cmd)
    {
        var order = new Order { /* ... */ };
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}

public class OrderQueryHandler
{
    private readonly AppDbContext _context; // ❌ نفس الـ DbContext!

    public OrderDto Handle(GetOrderQuery query)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .Where(o => o.Id == query.Id)
            .Select(o => new OrderDto { /* ... */ })
            .FirstOrDefault();
    }
}
```

**Why it's dangerous:**

- نفس Schema للقراءة والكتابة
- لا يمكن Optimize بشكل منفصل
- ال Write DB normalized، Read DB يحتاج denormalized

**✅ الحل الصحيح:**

```csharp
// ✅ Write Database (Normalized)
public class OrderWriteRepository
{
    private readonly WriteDbContext _writeDb;

    public void Add(Order order)
    {
        _writeDb.Orders.Add(order);
        _writeDb.SaveChanges();

        // ✅ Publish event لتحديث Read DB
        _eventBus.Publish(new OrderCreatedEvent(order));
    }
}

// ✅ Read Database (Denormalized)
public class OrderReadRepository
{
    private readonly ReadDbContext _readDb;

    public OrderDto GetById(int id)
    {
        // ✅ Query بسيطة - البيانات مُجهزة مسبقاً
        return _readDb.OrderViews
            .Where(o => o.Id == id)
            .FirstOrDefault();
    }
}

// ✅ Event Handler لمزامنة Read DB
public class OrderCreatedEventHandler
{
    public void Handle(OrderCreatedEvent e)
    {
        var view = new OrderView
        {
            Id = e.OrderId,
            CustomerName = e.CustomerName,
            Total = e.Total,
            // ... denormalized data
        };

        _readDb.OrderViews.Add(view);
        _readDb.SaveChanges();
    }
}
```

---

### Mistake #5: Synchronous Read Model Updates

❌ تحديث Read Model بشكل Synchronous

```csharp
public class CreateOrderHandler
{
    private readonly IOrderRepository _writeRepo;
    private readonly IOrderReadRepository _readRepo;

    public void Handle(CreateOrderCommand cmd)
    {
        var order = new Order { /* ... */ };
        _writeRepo.Add(order);

        // ❌ تحديث مباشر للـ Read Model
        var orderView = MapToView(order);
        _readRepo.Add(orderView);

        // ❗ ماذا لو فشل أحدهما؟
    }
}
```

**Why it's dangerous:**

- ال Coupling بين Write و Read
- ال Transaction عبر Database مختلفة
- ال Performance Impact

**✅ الحل الصحيح:**

```csharp
// ✅ Eventual Consistency مع Events
public class CreateOrderHandler
{
    private readonly IOrderRepository _writeRepo;
    private readonly IEventBus _eventBus;

    public void Handle(CreateOrderCommand cmd)
    {
        var order = new Order { /* ... */ };
        _writeRepo.Add(order);

        // ✅ Async event
        _eventBus.Publish(new OrderCreatedEvent(order));
        // Read Model سيتحدث لاحقاً
    }
}
```

---

## 🧪 Code Example — ❌ Bad Example

```csharp
// ❌ CQRS Implementation - خاطئة

// ❌ نفس الـ Model للقراءة والكتابة
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ViewCount { get; set; } // ❌ للقراءة فقط

    // ❌ Business Logic مختلطة
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException();
        Price = newPrice;
    }
}

// ❌ Command يرجع بيانات
public class UpdateProductPriceCommand
{
    public int ProductId { get; set; }
    public decimal NewPrice { get; set; }
}

public class UpdateProductPriceHandler
{
    private readonly AppDbContext _context; // ❌ نفس DbContext

    public Product Handle(UpdateProductPriceCommand cmd) // ❌ يرجع Product!
    {
        var product = _context.Products.Find(cmd.ProductId);
        product.UpdatePrice(cmd.NewPrice);
        _context.SaveChanges();

        return product; // ❌ Command يرجع بيانات!
    }
}

// ❌ Query تُعدّل البيانات
public class GetProductByIdQuery
{
    public int Id { get; set; }
}

public class GetProductByIdHandler
{
    private readonly AppDbContext _context; // ❌ نفس DbContext

    public Product Handle(GetProductByIdQuery query)
    {
        var product = _context.Products.Find(query.Id);

        // ❌ Query تُعدّل البيانات!
        product.ViewCount++;
        _context.SaveChanges();

        return product;
    }
}

// ❌ Controller مختلط
public class ProductController
{
    private readonly UpdateProductPriceHandler _updateHandler;
    private readonly GetProductByIdHandler _getHandler;

    // ❌ Action يعمل Update ويرجع البيانات المُحدثة
    public IActionResult UpdatePrice(UpdateProductPriceCommand cmd)
    {
        var product = _updateHandler.Handle(cmd); // ❌
        return Ok(product); // ❌ عرض البيانات بعد Update
    }

    // ❌ Action للقراءة يُعدّل البيانات
    public IActionResult GetProduct(int id)
    {
        var product = _getHandler.Handle(new GetProductByIdQuery { Id = id });
        return Ok(product); // ❌ Query عدلت ViewCount
    }
}
```

### ❗ Why this is wrong

- ال **Shared Model**: نفس Product للقراءة والكتابة
- ال **CQS Violation**: ال Command يرجع بيانات، Query تُعدّل
- ال **Shared Database**: نفس DbContext للاثنين
- ال **No Separation**: لا يوجد فصل حقيقي
- ال **Side Effects**: ال Query لها Side Effects (ViewCount++)
- ال **Not Scalable**: لا يمكن Scale القراءة بشكل منفصل

---

## 🧪 Code Example — ✅ Good Example

```csharp
// ✅ CQRS Implementation - صحيحة

// ────────────────────────────────────────────
// WRITE SIDE (Commands)
// ────────────────────────────────────────────

// ✅ Write Model (Rich Domain Model)
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    // ✅ Business Logic فقط
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be positive");

        if (newPrice > Price * 2)
            throw new InvalidOperationException("Price increase too high");

        Price = newPrice;

        // ✅ Raise domain event
        DomainEvents.Raise(new ProductPriceChangedEvent(Id, Price, newPrice));
    }
}

// ✅ Command (لا يرجع بيانات)
public class UpdateProductPriceCommand : ICommand
{
    public int ProductId { get; set; }
    public decimal NewPrice { get; set; }
}

// ✅ Command Handler
public class UpdateProductPriceHandler : ICommandHandler<UpdateProductPriceCommand>
{
    private readonly IProductWriteRepository _repository;
    private readonly IEventBus _eventBus;

    public void Handle(UpdateProductPriceCommand cmd) // ✅ void - لا يرجع بيانات
    {
        var product = _repository.GetById(cmd.ProductId);

        if (product == null)
            throw new NotFoundException($"Product {cmd.ProductId} not found");

        // ✅ Business Logic
        product.UpdatePrice(cmd.NewPrice);

        // ✅ Save to Write DB
        _repository.Update(product);

        // ✅ Publish event لتحديث Read DB
        _eventBus.Publish(new ProductPriceUpdatedEvent
        {
            ProductId = product.Id,
            NewPrice = product.Price,
            UpdatedAt = DateTime.UtcNow
        });
    }
}

// ✅ Write Repository
public class ProductWriteRepository : IProductWriteRepository
{
    private readonly WriteDbContext _writeDb;

    public Product GetById(int id)
    {
        return _writeDb.Products.Find(id);
    }

    public void Update(Product product)
    {
        _writeDb.Products.Update(product);
        _writeDb.SaveChanges();
    }
}

// ────────────────────────────────────────────
// READ SIDE (Queries)
// ────────────────────────────────────────────

// ✅ Read Model (Simple DTO)
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ViewCount { get; set; }
    public DateTime LastUpdated { get; set; }
}

// ✅ Query
public class GetProductByIdQuery : IQuery<ProductDto>
{
    public int Id { get; set; }
}

// ✅ Query Handler (لا يُعدّل البيانات)
public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductReadRepository _repository;

    public ProductDto Handle(GetProductByIdQuery query)
    {
        // ✅ فقط قراءة - لا تُعدّل
        return _repository.GetById(query.Id);
    }
}

// ✅ Read Repository (Denormalized)
public class ProductReadRepository : IProductReadRepository
{
    private readonly ReadDbContext _readDb;

    public ProductDto GetById(int id)
    {
        // ✅ Query بسيطة جداً - البيانات جاهزة
        return _readDb.ProductViews
            .Where(p => p.Id == id)
            .FirstOrDefault();
    }

    public List<ProductDto> GetAll()
    {
        return _readDb.ProductViews.ToList();
    }
}

// ✅ Event Handler لمزامنة Read DB
public class ProductPriceUpdatedEventHandler
{
    private readonly ReadDbContext _readDb;

    public async Task Handle(ProductPriceUpdatedEvent e)
    {
        var view = await _readDb.ProductViews.FindAsync(e.ProductId);

        if (view != null)
        {
            // ✅ تحديث Read Model
            view.Price = e.NewPrice;
            view.LastUpdated = e.UpdatedAt;

            await _readDb.SaveChangesAsync();
        }
    }
}

// ✅ Command منفصلة لتتبع المشاهدات
public class IncrementProductViewCommand : ICommand
{
    public int ProductId { get; set; }
}

public class IncrementProductViewHandler : ICommandHandler<IncrementProductViewCommand>
{
    private readonly ReadDbContext _readDb;

    public async Task Handle(IncrementProductViewCommand cmd)
    {
        var view = await _readDb.ProductViews.FindAsync(cmd.ProductId);

        if (view != null)
        {
            view.ViewCount++;
            await _readDb.SaveChangesAsync();
        }
    }
}

// ────────────────────────────────────────────
// CONTROLLER
// ────────────────────────────────────────────

public class ProductController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    // ✅ Command Endpoint - لا يرجع بيانات
    [HttpPut("products/{id}/price")]
    public IActionResult UpdatePrice(int id, UpdatePriceRequest request)
    {
        var command = new UpdateProductPriceCommand
        {
            ProductId = id,
            NewPrice = request.NewPrice
        };

        _commandBus.Send(command); // ✅ void

        return NoContent(); // ✅ 204 - لا بيانات
    }

    // ✅ Query Endpoint - لا يُعدّل البيانات
    [HttpGet("products/{id}")]
    public IActionResult GetProduct(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var result = _queryBus.Send(query);

        if (result == null)
            return NotFound();

        // ✅ Track view asynchronously (fire and forget)
        _ = _commandBus.SendAsync(new IncrementProductViewCommand { ProductId = id });

        return Ok(result);
    }
}

// ────────────────────────────────────────────
// DATABASE SCHEMAS
// ────────────────────────────────────────────

// ✅ Write DB Schema (Normalized)
public class WriteDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    // Normalized, with foreign keys, constraints
}

// ✅ Read DB Schema (Denormalized)
public class ReadDbContext : DbContext
{
    public DbSet<ProductView> ProductViews { get; set; }
    public DbSet<OrderView> OrderViews { get; set; }
    // Denormalized, optimized for queries
}

// ✅ Read Model Table (Flat)
public class ProductView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ViewCount { get; set; }
    public DateTime LastUpdated { get; set; }
    // All data in one table - no joins needed
}
```

### ✅ Why this works

- ال **Clear Separation**: ال Commands منفصلة عن Queries تماماً

- ال **CQS Compliance**: ال Commands لا ترجع بيانات، Queries لا تُعدّل

- ال **Different Models**: ال Write Model (Rich) vs Read Model (DTO)

- ال **Different Databases**: ال Write DB (Normalized) vs Read DB (Denormalized)

- ال **Eventual Consistency**: ال Events لمزامنة Read Model

- ال **Optimized**: كل مسار مُحسّن لغرضه

- ال**Scalable**: يمكن Scale القراءة بشكل منفصل

- ال **Testable**: سهولة اختبار Commands و Queries بشكل منفصل

---

## 🧩 Relation to Other Concepts

| المفهوم                  | العلاقة                                        |
| ------------------------ | ---------------------------------------------- |
| **Event Sourcing**       | CQRS يعمل بشكل مثالي مع Event Sourcing         |
| **Domain-Driven Design** | CQRS جزء من DDD Tactical Patterns              |
| **Microservices**        | CQRS يساعد في فصل Read/Write Services          |
| **CAP Theorem**          | CQRS يختار Availability و Eventual Consistency |
| **Mediator Pattern**     | يُستخدم لإرسال Commands و Queries              |
| **Repository Pattern**   | Write Repo منفصل عن Read Repo                  |
| **DTO Pattern**          | Read Model غالباً DTOs                         |

---

## 🏁 Summary (Mental Model)

> **CQRS is about recognizing that reading and writing are fundamentally different operations that deserve different optimizations.**

---

## 🚫 What this is NOT

- ❌ **ليس Event Sourcing**: ال CQRS منفصل، لكن يعملان جيداً معاً
- ❌ **ليس Microservices**: يمكن تطبيقه في Monolith
- ❌ **ليس Two Databases Always**: يمكن نفس DB مع Models منفصلة
- ❌ **ليس للتطبيقات البسيطة**: Over-engineering للـ CRUD
- ❌ **ليس Silver Bullet**: يضيف Complexity

---

## 🧠 Final Thought

> If you remember one thing, remember this:
> **CQRS is overkill until you have different needs for reading vs writing - then it's essential.**

---

## ✍️ Author Notes

ال CQRS من أقوى الأنماط المعمارية، لكنه **ليس للجميع**.

**متى تستخدمه:**

- نظام معقد بـ Business Logic ثقيلة
- ال Performance critical (millions of reads)
- ال Different scalability needs

**متى تتجنبه:**

- ا CRUD بسيط
- فريق صغير غير معتاد
- لا توجد مشاكل Performance

**Golden Rule:**

ابدأ بسيط (CRUD) → إذا واجهت مشاكل Performance في القراءة → CQRS.

**تذكر:** ال Complexity لها ثمن - لا تدفعه إلا لو احتجته فعلاً!
