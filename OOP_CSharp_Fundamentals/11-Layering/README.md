# 📚 Software Architecture Concepts

دليل موجز لأهم الأنماط المعمارية والمشاكل الشائعة في تطوير البرمجيات.

---

## 📌 Two-Layer Architecture

**المفهوم:** نمط معماري بسيط يفصل التطبيق إلى طبقتين:

- **Presentation Layer (UI)**: واجهة المستخدم وعرض البيانات
- **Data Access Layer (DAL)**: الوصول للبيانات والتخزين

**متى تستخدمه:**

- تطبيقات CRUD بسيطة
- Prototypes سريعة
- لا توجد قواعد أعمال معقدة
- فريق صغير (1-2 مطور)

**متى تتجنبه:**

- وجود Business Rules معقدة
- الحاجة لإعادة استخدام المنطق في أكثر من واجهة
- نمو التطبيق المستمر

**الأخطاء الشائعة:**

- ❌ وضع منطق الأعمال في Controllers (Fat Controllers)
- ❌ وضع قرارات العمل في Repositories (Smart Repositories)
- ❌ الوصول المباشر لقاعدة البيانات من UI

**القاعدة الذهبية:**

> Two layers work until you have real business rules - then you need three.

---

## 📌 Three-Layer Architecture

**المفهوم:** نمط معماري يعزل منطق الأعمال في طبقة منفصلة:

- ال **Presentation Layer (UI)**: عرض البيانات والتفاعل
- ال **Business Logic Layer (BLL)**: قواعد الأعمال والمنطق
- ال **Data Access Layer (DAL)**: الوصول للبيانات

**متى تستخدمه:**

- تطبيقات Business-Heavy
- ف Multiple Clients (Web + Mobile + Desktop)
- الحاجة لـ Unit Testing قوي
- فريق متوسط (3-10 مطورين)
- 10+ كيانات

**متى تتجنبه:**

- تطبيقات CRUD بسيطة جداً
- محتاح Prototypes سريعة
- فى Microservices صغيرة جداً

**الأخطاء الشائعة:**

- ❌ال Anemic Business Layer (طبقة فارغة تعمل كـ Pass-Through)
- ❌ال Business Logic Leakage (تسريب المنطق للـ UI أو DAL)
- ❌ال Layer Skipping (UI تستدعي DAL مباشرة)
- ❌ال Circular Dependencies (BLL تعتمد على UI)

**التبعيات الصحيحة:**

```
UI → BLL → DAL ✅
DAL → BLL ❌
BLL → UI ❌
```

**القاعدة الذهبية:**

> If your Controllers have `if` statements about business rules, you need a Business Layer.

---

## 📌 Leaky Abstraction

**المفهوم:** عيب معماري يحدث عندما يكشف الـ Interface تفاصيل التنفيذ للطبقة السفلى.

**الفكرة الأساسية:**

- الـ Interface يجب أن يكون مستقل عن التكنولوجيا
- المستخدم لا يجب أن يعرف كيف تم التنفيذ
- التسريب = كشف تفاصيل Implementation

**أمثلة على التسريب:**

- ❌ إرجاع `IQueryable<T>` من Repository (يكشف استخدام EF/LINQ)
- ❌ال Attributes مثل `[JsonProperty]` في DTOs (مرتبطة بـ JSON)
- ❌ال Exposing `DbContext` أو `SqlConnection` في Services
- ❌ استخدام `HttpContext` في Business Layer
- ❌ال Exception Types تكشف Database (`SqlException`)

**كيف تتجنبه:**

- ✅ استخدم Generic Types بدلاً من Technology-Specific
- ✅ أرجع Plain Objects (`List<T>`) بدلاً من `IQueryable<T>`
- ✅ أخفِ تفاصيل التنفيذ خلف Interfaces واضحة
- ✅ افصل Mapping/Serialization عن Models

**مثال سريع:**

```csharp
// ❌ Leaky
public interface IProductRepository
{
    IQueryable<Product> GetAll(); // يكشف LINQ!
}

// ✅ Clean
public interface IProductRepository
{
    List<Product> GetAll();
    List<Product> GetByPriceRange(decimal min, decimal max);
}
```

**القاعدة الذهبية:**

> If changing the implementation breaks the interface consumer, it's leaky.

---

## 🔄 العلاقة بين المفاهيم

```
Two-Layer → Three-Layer → Clean Architecture
    ↓           ↓              ↓
  بسيط      معياري          متقدم

  يجب تجنب Leaky Abstraction في جميع المستويات ✅
```

**مسار التطور:**

1. ابدأ بـ **Two-Layer** للمشاريع البسيطة
2. انتقل لـ **Three-Layer** عند ظهور Business Rules
3. احذر من **Leaky Abstraction** في كل مرحلة
4. تقدم لـ Clean/Hexagonal Architecture عند الحاجة

---

## 🎯 خلاصة سريعة

| المفهوم               | متى تستخدمه                            | العلامة الحمراء                            |
| --------------------- | -------------------------------------- | ------------------------------------------ |
| **Two-Layer**         | CRUD بسيط، لا business logic           | وجود `if` كثيرة في Controllers             |
| **Three-Layer**       | Business rules واضحة، Multiple clients | BLL فارغة (Pass-Through)                   |
| **Leaky Abstraction** | تجنبه دائماً!                          | إرجاع `IQueryable`, `DbSet`, `HttpContext` |

---

## 📖 للمزيد من التفاصيل

راجع الملفات الكاملة لكل مفهوم للحصول على:

- أمثلة كود مفصلة
- ال Use Cases محددة
- ال Common Mistakes موسعة
- ال Best Practices

---

**ملاحظة نهائية:**

اختر البساطة دائماً

لا تستخدم Three-Layer إذا كان Two-Layer يكفي

ولا تستخدم Clean Architecture إذا كان Three-Layer يكفي

ال Over-engineering أسوأ من Under-engineering في المشاريع الصغيرة
