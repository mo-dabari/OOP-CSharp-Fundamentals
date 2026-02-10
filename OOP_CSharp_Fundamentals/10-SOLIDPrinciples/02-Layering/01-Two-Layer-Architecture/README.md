# 📌 Two-Layer Architecture

**Category:** Architecture Pattern
**Level:** Fundamental

---

## 🧠 What is it?

Two-Layer Architecture هو نمط معماري بسيط يقسم التطبيق إلى طبقتين رئيسيتين:

- **Presentation Layer (UI)**: واجهة المستخدم وعرض البيانات
- **Data Access Layer (DAL)**: الوصول للبيانات والتخزين

الفكرة الجوهرية: **فصل عرض البيانات عن تخزينها**.

لا توجد طبقة منطق أعمال منفصلة - المنطق يكون إما في UI أو في DAL.

---

## ❓ Why does it exist?

ظهر هذا النمط لحل مشاكل التطبيقات الصغيرة التي كانت تخلط كل شيء في مكان واحد:

- **المشكلة الأصلية**: كود SQL مكتوب داخل واجهة المستخدم مباشرة
- **الحل**: فصل قاعدة البيانات عن الواجهة
- **العواقب لو تجاهلناه**:
  - استحالة تغيير قاعدة البيانات بدون تعديل UI
  - تكرار كود الوصول للبيانات
  - صعوبة الاختبار

---

## 🧱 Where does it belong?

```
┌─────────────────────┐
│  Presentation (UI)  │ ← Controllers, Views, Forms
├─────────────────────┤
│  Data Access (DAL)  │ ← Repositories, DbContext, SQL
└─────────────────────┘
```

**التبعيات (Dependencies):**

- UI → DAL ✅
- DAL → UI ❌ (ممنوع)

---

## ✅ When to use it

### الحالات المناسبة:

1. **CRUD Applications بسيطة**: تطبيقات إدخال وعرض بيانات فقط
2. **Prototypes سريعة**: تطبيقات تجريبية أو POC
3. **فريق صغير**: 1-2 مطور
4. **منطق أعمال بسيط جداً**: لا توجد قواعد معقدة

### Constraints:

- عدد الكيانات < 10
- لا توجد Integrations خارجية معقدة
- لا حاجة لـ Unit Testing مكثف

---

## ❌ When NOT to use it

### أخطاء شائعة:

1. **استخدامه في تطبيقات معقدة**: لما يكون فيه Business Rules كتيرة
2. **وضع المنطق في UI**: Controllers سمينة مليانة validation و calculations
3. **وضع المنطق في DAL**: Repositories تعمل business decisions
4. **نمو التطبيق**: بدأ بسيط وكبر بدون إعادة هيكلة

### Hidden Coupling:

```csharp
// ❌ UI تعرف تفاصيل Database
public class CustomerController
{
    public void SaveCustomer()
    {
        // Business logic في الـ UI!
        if (customer.Age < 18)
            customer.RequiresGuardian = true;

        _repository.Save(customer);
    }
}
```

---

## 🔥 Common Mistakes

### Mistake #1: Fat Controllers

وضع كل منطق الأعمال في Controllers بدلاً من طبقة منفصلة.

```csharp
// ❌ Controller يحتوي على Business Logic
public IActionResult CreateOrder(OrderDto dto)
{
    // Validation
    if (dto.Items.Count == 0) return BadRequest();

    // Business Rules
    var total = dto.Items.Sum(x => x.Price * x.Quantity);
    if (dto.DiscountCode == "SAVE10")
        total *= 0.9m;

    // Tax Calculation
    total *= 1.14m;

    // Save
    _repository.Save(new Order { Total = total });
}
```

**Why it's dangerous:**

- لا يمكن إعادة استخدام المنطق
- صعوبة الاختبار
- انتهاك SRP

---

### Mistake #2: Smart Repositories

جعل DAL يتخذ قرارات Business.

```csharp
// ❌ Repository يحتوي على Business Logic
public class OrderRepository
{
    public void SaveOrder(Order order)
    {
        // Business decision في DAL!
        if (order.Total > 1000)
            order.Status = OrderStatus.RequiresApproval;

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
}
```

**Why it's dangerous:**

- خلط المسؤوليات
- انتهاك SRP
- صعوبة تتبع Business Rules

---

### Mistake #3: Direct Database Access from UI

```csharp
// ❌ UI تستخدم DbContext مباشرة
public class ProductController
{
    private readonly AppDbContext _db;

    public IActionResult GetProducts()
    {
        var products = _db.Products
            .Where(p => p.IsActive)
            .ToList();
        return View(products);
    }
}
```

**Why it's dangerous:**

- تسريب تفاصيل Database للـ UI
- صعوبة تغيير مصدر البيانات
- انتهاك Separation of Concerns

---

## 🧪 Code Example — ❌ Bad Example

❌ كل شيء مختلط

```csharp
public class CustomerForm
{
    private SqlConnection _connection;

    public void SaveButton_Click()
    {
        // Validation في UI
        if (string.IsNullOrEmpty(txtName.Text))
        {
            MessageBox.Show("Name required");
            return;
        }

        // Business Logic في UI
        var discount = 0m;
        if (int.Parse(txtAge.Text) > 60)
            discount = 0.1m;

        // SQL مباشرة في UI
        var cmd = new SqlCommand(
            "INSERT INTO Customers (Name, Age, Discount) VALUES (@n, @a, @d)",
            _connection
        );
        cmd.Parameters.AddWithValue("@n", txtName.Text);
        cmd.Parameters.AddWithValue("@a", txtAge.Text);
        cmd.Parameters.AddWithValue("@d", discount);
        cmd.ExecuteNonQuery();
    }
}
```

### ❗ Why this is wrong

- **انتهاك Separation of Concerns**: UI, Validation, Business, Data كلهم في مكان واحد
- **استحالة الاختبار**: لا يمكن عمل Unit Test
- **Tight Coupling**: UI مرتبطة بـ SQL Server مباشرة
- **تكرار الكود**: لو فيه شاشة تانية محتاجة نفس المنطق

---

## 🧪 Code Example — ✅ Good Example

```csharp
// ✅ Presentation Layer
public class CustomerController
{
    private readonly ICustomerRepository _repository;

    public IActionResult CreateCustomer(CustomerDto dto)
    {
        // UI تتعامل مع Validation فقط
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // تحويل DTO لـ Entity
        var customer = new Customer
        {
            Name = dto.Name,
            Age = dto.Age
        };

        // Business logic بسيط (يُفضل نقله لطبقة منفصلة لو كبر)
        if (customer.Age > 60)
            customer.Discount = 0.1m;

        _repository.Add(customer);
        return Ok();
    }
}

// ✅ Data Access Layer
public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public Customer? GetById(int id)
    {
        return _context.Customers.Find(id);
    }
}

// ✅ Interface للفصل
public interface ICustomerRepository
{
    void Add(Customer customer);
    Customer? GetById(int id);
}
```

### ✅ Why this works

- **فصل واضح**: UI منفصلة عن DAL
- **Testable**: يمكن عمل Mock لـ ICustomerRepository
- **قابل للتغيير**: يمكن تغيير Database بدون تعديل Controller
- **Interface**: استخدام Abstraction يقلل Coupling

---

## 🧩 Relation to Other Concepts

| المفهوم                      | العلاقة                               |
| ---------------------------- | ------------------------------------- |
| **Three-Layer Architecture** | التطور الطبيعي - إضافة Business Layer |
| **Repository Pattern**       | يُستخدم في DAL للفصل عن DbContext     |
| **SRP**                      | كل طبقة لها مسؤولية واحدة             |
| **Dependency Inversion**     | UI تعتمد على Abstraction (Interface)  |
| **Separation of Concerns**   | الأساس اللي مبني عليه                 |

---

## 🏁 Summary (Mental Model)

> **Two-Layer is about separating WHAT you show from WHERE you store it.**

---

## 🚫 What this is NOT

- ❌ **ليس Framework**: هو مجرد طريقة تنظيم
- ❌ **ليس مناسب لكل شيء**: التطبيقات المعقدة تحتاج Three-Layer أو أكثر
- ❌ **ليس مكان للـ Business Logic**: لو كبر المنطق، انتقل لـ Three-Layer

---

## 🧠 Final Thought

> If you remember one thing, remember this:
> **Two layers work until you have real business rules - then you need three.**

---

## ✍️ Author Notes

ال Two-Layer مناسب جداً للبداية
لكن أغلب المشاريع بتكبر وتحتاج Three-Layer

لو لقيت نفسك بتكتب `if` كتير في Controllers → وقت الانتقال لـ Business Layer
