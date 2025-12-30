## 04 - Interfaces (الواجهات)

### مقدمة عن الواجهات

الواجهة (Interface) هي عقد يحدد مجموعة من الدوال والخصائص التي **يجب** على أي فئة تنفذها أن توفرها، بدون تحديد **كيف** سيتم التنفيذ.

#### التشبيه الحقيقي:
واجهة الهاتف - جميع الهواتف لديها:
- شاشة
- أزرار
- ميكروفون

لكن **كيفية** صنعها مختلفة (iPhone, Samsung, Huawei).

---

## 📚 المفاهيم الأساسية

### 1. ما هي الواجهة؟

<pre dir="ltr"><code class="language-csharp">
    
```csharp
// واجهة - عقد فقط، بدون تطبيق
public interface IAnimal
{
    void MakeSound();      // بدون body!
    string GetSpecies();
}

// فئة تطبق الواجهة
public class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("واف واف!");  // تطبيق
    }
    
    public string GetSpecies()
    {
        return "كلب";  // تطبيق
    }
}
```
</code></pre>

### 2. الوراثة المتعددة من Interfaces

<pre dir="ltr"><code class="language-csharp">
    
```csharp
// واجهة واحدة
public interface IMovable
{
    void Move();
}

// واجهة أخرى
public interface IFlying
{
    void TakeOff();
    void Land();
}
// فئة تطبق واجهتين! (وراثة متعددة)
public class Bird : IMovable, IFlying
{
    public void Move() { }
    public void TakeOff() { }
    public void Land() { }
}
```
</code></pre>

### 3. الفرق بين Interface و Abstract Class

| الميزة | Interface | Abstract Class |
|--------|-----------|--------|
| الوراثة المتعددة | ✅ نعم | ❌ لا |
| State (بيانات) | ❌ لا | ✅ نعم |
| Constructor | ❌ لا | ✅ نعم |
| Access Modifiers | عام فقط | جميعها |
| الاستخدام | العقود | نموذج أساسي |

---

## 🎯 أنواع الواجهات
<div dir="ltr">
    
### 1. Marker Interfaces (بدون members)
```csharp
// واجهة للتحديد فقط
public interface IComparable
{
}

public class Document : IComparable
{
}
```
</div>

### 2. Functional Interfaces (دالة واحدة)
```csharp
public interface ILogger
{
    void Log(string message);
}
```

### 3. Rich Interfaces (عدة members)
```csharp
public interface IRepository<T>
{
    T GetById(int id);
    List<T> GetAll();
    void Add(T item);
    void Delete(int id);
    void Update(T item);
}
```

### 4. Segregated Interfaces (نخصصات)
```csharp
public interface IReader
{
    string Read();
}

public interface IWriter
{
    void Write(string data);
}

// فئة قد تطبق واحدة فقط
public class ReadOnlyFile : IReader
{
    public string Read() => "قراءة فقط";
}
```

---

## 💡 فوائد الواجهات

### 1. العقود والالتزامات
```csharp
// العقد: أي class يرث هذا يجب أن يطبق هذا
public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount);
    bool RefundPayment(string transactionId);
}
```

### 2. الوراثة المتعددة (الميزة الكبرى!)
```csharp
// كلب هو حيوان وحيوان أليف
public class Dog : IAnimal, IPet, ITrainable
{
}
```

### 3. فصل الواجهات عن التطبيق
```csharp
// المستخدم يرى الواجهة فقط
public void ProcessOrder(IPaymentProcessor processor)
{
    processor.ProcessPayment(100);
}

// يمكن تمرير أي تطبيق
ProcessOrder(new CreditCardProcessor());
ProcessOrder(new PayPalProcessor());
ProcessOrder(new ApplePayProcessor());
```

### 4. سهولة الاختبار
```csharp
// Mock للاختبار
public class FakePaymentProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount) => true;
    public bool RefundPayment(string id) => true;
}
```

---

## ⚠️ أخطاء شائعة

### ❌ الخطأ 1: واجهة بدون معنى
```csharp
// ❌ خطأ - لا تجميع منطقي
public interface IStuff
{
    void DoSomething();
    void DoSomethingElse();
}

// ✅ صحيح - واجهة محددة
public interface IRepository<T>
{
    T GetById(int id);
}
```

### ❌ الخطأ 2: واجهة كبيرة جداً
```csharp
// ❌ خطأ - فئة قد لا تحتاج كل شيء
public interface IBigInterface
{
    void Read();
    void Write();
    void Delete();
    void Update();
    void Process();
    void Generate();
}

// ✅ صحيح - واجهات صغيرة محددة
public interface IReader { void Read(); }
public interface IWriter { void Write(); }
```

### ❌ الخطأ 3: نسيان تطبيق جميع الـ members
```csharp
// ❌ خطأ - ملف لم ينفذ جميع الـ members
public class MyClass : ILogger
{
    // لم ننفذ Log() - خطأ تجميع!
}
```

---

## 🔍 متى تستخدم Interface؟

### ✅ استخدم عندما:
1. **عقود يجب التزام بها**
   ```csharp
   public interface IDisposable
   {
       void Dispose();  // كل resource يجب أن يحرر نفسه
   }
   ```

2. **وراثة متعددة**
   ```csharp
   public interface IAnimal { }
   public interface IFlying { }
   public class Bird : IAnimal, IFlying { }
   ```

3. **Dependency Injection**
   ```csharp
   public class Service
   {
       private ILogger logger;
       public Service(ILogger log) => logger = log;
   }
   ```

4. **Polymorphism**
   ```csharp
   List<IPaymentProcessor> processors = new()
   {
       new CreditCard(),
       new PayPal(),
       new Apple()
   };
   ```

---

## 📝 أفضل الممارسات

### ✅ افعل هذا:

```csharp
// 1. سمّي الواجهة بـ I
public interface IRepository { }

// 2. اجعلها محددة
public interface IUserRepository
{
    User GetById(int id);
}

// 3. فصل المخاوف
public interface IReader { }
public interface IWriter { }
public interface IClosable { }

// 4. استخدم generics
public interface IRepository<T>
{
    T GetById(int id);
}

// 5. طبق الواجهة بالكامل
public class UserRepository : IUserRepository
{
    public User GetById(int id) { /* تطبيق */ }
}
```

### ❌ لا تفعل هذا:

```csharp
// ❌ بدون I prefix
public interface Animal { }

// ❌ واجهة فضفاضة
public interface IStuff { }

// ❌ واجهة كبيرة جداً
public interface IBigInterface
{
    void Method1();
    void Method2();
    // ... 50 method
}

// ❌ نسيان تطبيق
public class Wrong : IInterface
{
    // لم نطبق الـ members!
}
```

---

## 🎓 Interface Segregation Principle

> **قاعدة SOLID:** "الفئة لا يجب أن تُجبر على تطبيق واجهات لا تحتاجها"

```csharp
// ❌ خطأ - واجهة كبيرة
public interface IWorker
{
    void Work();
    void Eat();
}

// مقهى يعمل، لكن لا يأكل!
public class Robot : IWorker
{
    public void Work() { }
    public void Eat() { }  // ❌ لا معنى!
}

// ✅ صحيح - واجهات صغيرة
public interface IWorkable { void Work(); }
public interface IEatable { void Eat(); }

public class Robot : IWorkable
{
    public void Work() { }  // ✅ منطقي فقط
}

public class Human : IWorkable, IEatable
{
    public void Work() { }
    public void Eat() { }
}
```

---

## 💻 حالات الاستخدام الشائعة

### 1. Data Access Pattern
```csharp
public interface IRepository<T>
{
    T GetById(int id);
    List<T> GetAll();
    void Add(T item);
    void Delete(int id);
}
```

### 2. Logging
```csharp
public interface ILogger
{
    void Log(string message);
    void LogError(string error);
}
```

### 3. Configuration
```csharp
public interface IConfiguration
{
    string GetValue(string key);
}
```

### 4. Service Pattern
```csharp
public interface IEmailService
{
    void SendEmail(string to, string message);
}
```

### 5. Factory Pattern
```csharp
public interface IPaymentProcessorFactory
{
    IPaymentProcessor CreateProcessor(string type);
}
```

---

## 📊 مقارنة سريعة

```
                    Interface    Abstract    Class
────────────────────────────────────────────────
الوراثة المتعددة      ✅           ❌         ❌
State               ❌           ✅         ✅
Constructor         ❌           ✅         ✅
Access Modifiers    عام          جميع       جميع
الاستخدام          عقود         نموذج      كائن
────────────────────────────────────────────────
```

---

## 🚀 الخطوات التالية

1. **اقرأ الأمثلة:**
   - BasicInterface.cs - أمثلة بسيطة
   - InterfaceHierarchy.cs - تسلسل معقد
   - DependencyInjection.cs - DI مع Interfaces

2. **حل التمارين:**
   - Exercises.cs - 3 تمارين عملية

3. **ادرس الحالات الواقعية:**
   - RealWorldScenarios/ - مشاريع حقيقية

4. **أسئلة المقابلات:**
   - InterviewQuestions.md - 10 أسئلة مع إجابات

---

## 📚 المراجع

- Microsoft Docs: Interfaces
- SOLID Principles
- Design Patterns with Interfaces
- C# Language Features

---

**التاريخ:** ديسمبر 2024
**الإصدار:** 1.0
**الحالة:** جاهز للدراسة ✅
