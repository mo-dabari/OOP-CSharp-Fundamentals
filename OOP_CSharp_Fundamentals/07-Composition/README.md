## 06 - Composition (التركيب)

### مقدمة عن التركيب

التركيب (Composition) هو "علاقة HAS-A" حيث يحتوي كائن على كائن آخر كجزء منه. الكائن الأب يتحكم في دورة حياة الكائن الابن.

#### التشبيه الحقيقي

- **السيارة تحتوي على محرك** - إذا حذفنا السيارة، يُحذف المحرك معها
- **المنزل يحتوي على غرف** - الغرف جزء من المنزل
- **الموظف يحتوي على عنوان** - العنوان موجود فقط للموظف

---

## 📚 الفرق بين Composition و Inheritance

| الجانب | Composition | Inheritance |
|--------|-------------|------------|
| **العلاقة** | HAS-A | IS-A |
| **الاستخدام** | احتواء | توريث |
| **المرونة** | أكثر مرونة | أقل مرونة |
| **التغيير** | سهل في Runtime | صعب |
| **الاستخدام الأفضل** | معظم الحالات | حالات محدودة |

```csharp
// ❌ Inheritance (سيء) - Bird IS-A Airplane
public class Airplane : Bird { }

// ✅ Composition (جيد) - Car HAS-A Engine
public class Car
{
    private Engine engine;  // احتواء
}
```

---

## 🎯 أنواع التركيب

### 1. Strong Composition (تركيب قوي)

الكائن الأب يتحكم بدورة حياة الكائن الابن.

```csharp
public class Car
{
    private Engine engine;  // يُنشأ وينتهي مع السيارة

    public Car()
    {
        engine = new Engine();
    }
}

// إذا حذفنا Car، يُحذف Engine تلقائياً
var car = new Car();
// ... استخدام
// car تنتهي → engine ينتهي أيضاً
```

### 2. Weak Composition (تركيب ضعيف)

الكائن الأب لا يتحكم بدورة حياة الكائن الابن.

```csharp
public class Person
{
    private Address address;

    public void SetAddress(Address addr)
    {
        address = addr;  // Address موجود بشكل مستقل
    }
}

// Address قد يكون مشترك بين عدة Persons
var addr = new Address("القاهرة");
var person1 = new Person();
var person2 = new Person();
person1.SetAddress(addr);
person2.SetAddress(addr);
// عندما تنتهي person1، addr لا يزال موجود
```

---

## 💡 فوائد Composition

### 1. المرونة (Flexibility)

```csharp
// يمكن تغيير أجزاء السيارة بسهولة
public class Car
{
    private Engine engine;
    private Wheels wheels;
    private Transmission transmission;

    public void ChangeEngine(Engine newEngine)
    {
        engine = newEngine;  // تغيير ديناميكي
    }
}
```

### 2. إعادة الاستخدام (Reusability)

```csharp
// نفس Engine يمكن أن يكون في سيارات مختلفة
public class SportsCar
{
    private Engine engine;  // نفس Engine
}

public class Truck
{
    private Engine engine;  // نفس Engine
}
```

### 3. فصل المسؤوليات (Single Responsibility)

```csharp
// كل فئة مسؤولة عن نفسها
public class Engine { }      // محرك فقط
public class Wheels { }      // عجلات فقط
public class Car             // تركيب فقط
{
    private Engine engine;
    private Wheels wheels;
}
```

---

## ⚠️ أخطاء شائعة

### ❌ الخطأ 1: استخدام Inheritance بدل Composition

```csharp
// ❌ خطأ - Engine ليست نوع Car
public class Engine : Car { }

// ✅ صحيح - Car يحتوي على Engine
public class Car
{
    private Engine engine;
}
```

### ❌ الخطأ 2: نسيان البدء والنهاية

```csharp
// ❌ خطأ - engine لم تُنشأ
public class Car
{
    private Engine engine;

    public void Start()
    {
        engine.Start();  // NullReferenceException!
    }
}

// ✅ صحيح
public class Car
{
    private Engine engine;

    public Car()
    {
        engine = new Engine();  // إنشاء
    }
}
```

### ❌ الخطأ 3: الإفراط في التركيب

```csharp
// ❌ معقد جداً
public class Car
{
    private Engine engine;
    private Wheels wheels;
    private Transmission transmission;
    private Suspension suspension;
    private ElectricalSystem electrical;
    private FuelSystem fuel;
    // ... 20 مكون آخر
}

// ✅ تجميع أفضل
public class Car
{
    private Engine engine;
    private Chassis chassis;  // يحتوي على wheels, suspension
    private Transmission transmission;
}
```

---

## 🔍 متى تستخدم Composition؟

### ✅ استخدم عندما

1. **علاقة HAS-A**

```csharp
// السيارة لديها محرك
public class Car
{
    private Engine engine;
}
```

1. **أجزاء مستقلة**

```csharp
// كل جزء يعمل بشكل مستقل
public class Computer
{
    private CPU cpu;
    private RAM ram;
    private HardDisk disk;
}
```

1. **مرونة ديناميكية**

```csharp
// يمكن تبديل الأجزاء
public class Robot
{
    private IArm leftArm;
    private IArm rightArm;

    public void ChangeArm(IArm newArm) { }
}
```

---

## 🎨 المسارات الشائعة

### 1. Basic Composition

```csharp
public class Address { }
public class Person
{
    private Address address;
}
```

### 2. Composition with Collections

```csharp
public class Department
{
    private List<Employee> employees;  // أجزاء متعددة
}
```

### 3. Deep Composition

```csharp
public class Company
{
    private List<Department> departments;
    // كل Department يحتوي على Employees
}
```

---

## 📊 مقارنة سريعة

```
              Composition    Inheritance
────────────────────────────────────────
المرونة        عالية جداً      منخفضة
الاستخدام      شائع جداً       محدود
التعقيد        متوسط          قد يكون عميق
الأداء        سريع           سريع
────────────────────────────────────────
```

---

## 🚀 الخطوات التالية

1. **اقرأ الأمثلة:**
   - BasicComposition.cs
   - AdvancedComposition.cs
   - CompositionVsInheritance.cs

2. **حل التمارين:**
   - Exercises.cs

3. **ادرس الحالات الواقعية:**
   - RealWorldScenarios/

4. **أسئلة المقابلات:**
   - InterviewQuestions.md

---

### ❌ Note:

1. **هل يوجد "أب" و"ابن" في Composition؟**

❌ لا يوجد Parent / Child بالمعنى الوراثي
✔ لكن في الشرح التعليمي يُستخدم المصطلح مجازًا

لذلك الجملة:

الكائن الأب يتحكم بدورة حياة الكائن الابن

مفهومة تعليميًا
لكن ليست دقيقة مصطلحيًا 100%

2. **الصياغة الصحيحة**

الكائن المالك يتحكم بالكامل في دورة حياة الكائن المُكوِّن، ولا يمكن لهذا الكائن أن يوجد مستقلًا عنه.

المالك هنا : Car
المكون هو : Engine
