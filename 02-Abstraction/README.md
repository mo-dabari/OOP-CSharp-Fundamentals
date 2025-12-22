# 🎯 OOP in C# - Abstraction

مقدمة عن التجريد
هو مفهوم أساسي في البرمجة الكائنية التوجه يعني إخفاء التفاصيل المعقدة وعرض فقط الواجهةبكلمات أخرى نركز على:

ماذا يفعل الكائن
وليس كيف يفعل ذلك

**التشبيه الحقيقي:**
عندما تقود سيارة لا تحتاج أن تفهم كيف يعمل المحرك أو الترنسميشن تركز فقط على:

- الدواسات (الفرامل، الوقود، الكلتش)
- عجلة القيادة
- ناقل الحركة

كل هذه واجهات بسيطة تخفي خلفها آلاف التفاصيل المعقدة!

---

## المفاهيم الأساسية

### 1. الفرق بين Abstraction و Encapsulation

- Abstraction: 
هدفه : تبسيط الواجهة للمستخدم
تعريفه: إخفاء التفاصيل المعقدة

- Encapsulation:
هدفه : حماية البيانات من الوصول العشوائي
تعريفه: تجميع البيانات والدوال

**الفرق المهم**

Encapsulation: 
يركز على البيانات (كيفية حفظ البيانات بأمان)

Abstraction:
يركز على الإجراءات (كيفية إخفاء التفاصيل المعقدة)

### Example:
```
// Encapsulation - حماية البيانات
public class BankAccount
{
    private decimal balance;  // بيانات مخفية
    public decimal Balance { get; private set; }  // وصول محدود
}

// Abstraction - إخفاء العملية المعقدة
public abstract class PaymentProcessor
{
    public abstract void ProcessPayment(decimal amount);
    // لا نحتاج معرفة كيف يتم الدفع (بطاقة، محفظة رقمية، ...)
}
```
---

### 2. Types Abstraction:

**2.1 Abstract Classes:**
هو كلاس لايمكن انشاء منه instance 
يستخدم كا Template لل ال Classes تورث منه 

**الخصائص:**
- تحتوي على methods abstract (مجردة) بدون تطبيق
- تحتوي على methods عادية بتطبيق كامل
- لا يمكن new AbstractClass()
- يجب الوراثة منها وتطبيق جميع الـ abstract methods

### Example:
```
public class Animal
{
    // method مجردة - بدون تطبيق
    public abstract void MakeSound();

    // method عادية - بتطبيق كامل
    public void Sleep()
    {
        Console.WriteLine("الحيوان نائم...");
    }
}

// فئة وارثة - يجب تطبيق جميع الـ abstract methods
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("الكلب ينبح: واف واف!");
    }
}
```


**2.2 Interfaces:**
هى عثد تحدد مجموعة من ال Members (Methods, Properties) بدون تطبيق 

**الخصائص:**
- members مجردة (في C# 7 والأقدم)
- C# 8+ يدعم تطبيقات افتراضية
- فئة واحدة يمكنها implement اكثر من interfaces
- لا توجد state

### Example:
```
// واجهة - عقد فقط
public interface IAnimal
{
    void MakeSound();
    string GetSpecies();
}

// فئة تنفذ الواجهة
public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("القطة تموء: مياو!");
    }

    public string GetSpecies()
    {
        return "قطة";
    }
}
```
---

### Abstract Class vs Interface

```
| Feature / Capability        | Abstract Class | Interface |
|----------------------------|----------------|-----------|
| Multiple Inheritance       | ❌ No          | ✅ Yes    |
| State (Fields / Data)      | ✅ Yes         | ❌ No (C# 7 and earlier) |
| Access Modifiers           | ✅ All         | ⚠️ Public only (by default) |
| Constructor                | ✅ Yes         | ❌ No     |
| Static Members             | ✅ Yes         | ✅ Yes (C# 11+) |
| Usage / Relationship       | IS-A Relationship          | IS-A Behavior     |

```
---

### Abstruaction Benefites

1. Simplify the code
```
// بدون abstraction - معقد!
if (paymentType == "CreditCard")
{
    // عمليات معقدة جداً...
    ValidateCard();
    CheckBalance();
    ProcessTransaction();
    UpdateDatabase();
    SendEmail();
}
else if (paymentType == "PayPal")
{
    // عمليات مختلفة تماماً...
}

// مع abstraction - بسيط وواضح!
IPaymentProcessor processor = GetProcessor(paymentType);
processor.ProcessPayment(amount);  // كل التفاصيل مخفية!
```
---

2. Ease of maintenance and development
```
// إضافة طريقة دفع جديدة بدون تغيير الكود القديم
public class GooglePayProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount)
    {
        // تطبيق جديد
    }
}
```
---

3. Flexibility and expansion
```
// يمكن تبديل التطبيق بدون تغيير الكود الذي يستخدمه
IPaymentProcessor processor1 = new CreditCardProcessor();
IPaymentProcessor processor2 = new PayPalProcessor();
IPaymentProcessor processor3 = new GooglePayProcessor();

// جميعها تعمل بنفس الطريقة!
processor1.ProcessPayment(100);
processor2.ProcessPayment(100);
processor3.ProcessPayment(100);
```
---

4. Security
```
// المستخدم لا يعرف التفاصيل الحساسة
public interface IUserRepository
{
    User GetUser(int id);
    void SaveUser(User user);
}

// التطبيق يمكن أن يكون معقداً جداً (encryption, caching, ...)
public class SecureUserRepository : IUserRepository
{
    // تفاصيل معقدة مخفية
}
```
---

### Best practices
```
// 1. استخدم Interfaces للعقود
public interface ILogger
{
    void Log(string message);
}

// 2. أخفِ التفاصيل المعقدة
public class FileLogger : ILogger
{
    public void Log(string message)
    {
        // تفاصيل معقدة مخفية
        string timestamp = DateTime.Now.ToString();
        string formattedMessage = Format(message, timestamp);
        ValidatePath();
        WriteToFile(formattedMessage);
    }
}

// 3. استخدم Abstract Classes للـ base functionality
public abstract class Shape
{
    protected double width;
    protected double height;
    
    public abstract double CalculateArea();
    
    public virtual void Display()
    {
        Console.WriteLine($"الشكل: {GetType().Name}");
    }
}
```
---

### Don't do this
```
// ❌ تفاصيل معقدة معرضة
public class PaymentProcessor
{
    public void ProcessWithAllDetails(string cardNumber, string cvv, 
        DateTime expiry, decimal amount, string merchant, string currency,
        bool requiresAuthorization, int retryCount, ...)
    {
        // معاملات كثيرة جداً!
    }
}

// ❌ عدم إخفاء الخطوات المعقدة
public class DatabaseConnection
{
    public void ExecuteQuery()
    {
        EstablishConnection();
        OpenSocket();
        SendPackets();
        WaitForResponse();
        ParseResponse();
        CloseSocket();
        // المستخدم يرى كل هذا!
    }
}
```
---

### Common Uses

**1. Payment system**
```
public interface IPaymentProcessor
{
    bool ProcessPayment(Payment payment);
    bool RefundPayment(string transactionId);
}

// تطبيقات متعددة - الكود الرئيسي لا يعرف الفرق
public class StripePaymentProcessor : IPaymentProcessor { }
public class PayPalPaymentProcessor : IPaymentProcessor { }
public class ApplePayProcessor : IPaymentProcessor { }
```
---

**2. Database**
```
public interface IRepository<T>
{
    T GetById(int id);
    void Save(T entity);
    void Delete(int id);
}

// يمكن تبديل التطبيق
public class SqlRepository<T> : IRepository<T> { }
public class MongoRepository<T> : IRepository<T> { }
public class InMemoryRepository<T> : IRepository<T> { }
```
---

3. **Logging**
```
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message, Exception ex);
}

// تطبيقات مختلفة
public class ConsoleLogger : ILogger { }
public class FileLogger : ILogger { }
public class CloudLogger : ILogger { }
```
---

### without Abstruction (X) :(
```
public class Order
{
    public void Process()
    {
        if (paymentMethod == "Card")
        {
            ValidateCardDetails();
            CheckBalance();
            DeductFromBank();
            UpdateBankRecords();
            SendConfirmationToBank();
            LogTransaction();
        }
        else if (paymentMethod == "PayPal")
        {
            CreatePayPalSession();
            SendRequest();
            WaitForResponse();
            UpdatePayPalDatabase();
            NotifyPayPal();
        }
        // 100+ سطر كود!
    }
}
```
---

### With Abstruction
```
public class Order
{
    private IPaymentProcessor processor;
    
    public Order(IPaymentProcessor processor)
    {
        this.processor = processor;
    }
    
    public void Process()
    {
        processor.ProcessPayment(amount);  // سطر واحد!
    }
}
```
---

