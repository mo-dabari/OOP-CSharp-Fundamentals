# 🎯 OOP in C# - Encapsulation
---

1. Definition ?
Encapsulation هو مبدأ OOP يهدف إلى:
- تجميع البيانات (Data)
- مع السلوك (Behavior)
- داخل كيان واحد
- مع التحكم في كيفية الوصول إليهما

**Encapsulation is about controlling access, not just hiding data.** 

في C# يتحقق ذلك باستخدام:
- Access Modifiers (private, protected, public)
- Properties
- Methods

┌─────────────────────────────────┐
│     الكائن (Object)             |
├─────────────────────────────────┤
│  البيانات (Data/Attributes)     │  ← مخفية
│  الدوال (Methods)               │  ← مخفية
├─────────────────────────────────┤
│  الواجهة العامة (Public)       │  ← مرئية
│  - GetName()                    │
│  - SetAge()                     │
└─────────────────────────────────┘
---

2. What Problem Does Encapsulation Solve ?

في أي نظام برمجي، أكبر خطر هو وصول غير مُنضبط إلى حالة الكائن

**بدون Encapsulation:**

- أي جزء من الكود يمكنه تعديل البيانات
- الكائن يمكن أن يدخل Invalid State
- القواعد المنطقية (Business Rules) يمكن كسرها بسهولة
- يصبح التتبع (Debugging) والصيانة (Maintenance) مكلفين جدًا

وُجدال Encapsulation
لحماية الكائن من الاستخدام الخاطئ وحتى من نفسك كمطور
---

3. Data Hiding 
البيانات الداخلية لا يمكن الوصول إليها مباشرة

```
public class Person
{
    // ❌ لا تفعل هذا - البيانات معرضة
    public int age;
    
    // ✅ افعل هذا - البيانات مخفية
    private int age;
}
```
---

4. Access Control
نتحكم في كيفية الوصول والتعديل عبر ال Properties

```
public class Person
{
    private int age;
    
    // Property مع تحقق
    public int Age
    {
        get { return age; }
        set 
        { 
            if (value > 0 && value < 150)
                age = value;
            else
                Console.WriteLine("عمر غير صحيح!");
        }
    }
}
```
---
3. Validation (التحقق من الصحة)
التأكد من صحة البيانات قبل حفظها:

```
public string Email
{
    get { return email; }
    set
    {
        if (value.Contains("@"))
            email = value;
        else
            throw new ArgumentException("البريد غير صحيح!");
    }
}
```
---
4. الفرق بين Encapsulation و Data Hiding
```
| Concept       | Definition              | Concept                                    |
|---------------|-------------------------|-------------------------------------------------|
| Data Hiding   | إخفاء الحقول فقط       | private int age;                                | 
| Encapsulation |إخفاء + توفير واجهة آمنة| private int age; + public int Age { get; set; } |
```
Encapsulation = Data Hiding + Validation + Business Logic
---

5. Encapsulation != Getters & Setters
خطأ شائع:
Encapsulation يعني أعمل كل حاجة private وأضيف getters/setters.
هذا غير صحيح

Encapsulation الحقيقي:

- لا تعرض البيانات
- تعرض سلوكًا (Behavior)
- السلوك يفرض القواعد
---

6. How Encapsulation Works in C#
أدوات Encapsulation في C#:
| Tool             | Purpose                        |
|------------------|--------------------------------|
| private fields   |حماية الحالة الداخلية         |                       
| public methods   |       توفير سلوك آمن          |
| properties       |تحكم ذكي في القراءة/الكتابة   |
| readonly         |منع التعديل بعد الإنشاء        | 
---

7. Why Encapsulation Matters (Mental Model)
فكّر في الكائن كـ Black Box:

أنت لا تهتم كيف يعمل من الداخل
يهمك فقط ما الذي يسمح لك بفعله

**Encapsulation يحقق:**

- Data Integrity
- Reduced Coupling
- Clear Responsibilities
- Safer Refactoring
---

## Benefits
---
1. التحكم الكامل
```
private decimal balance;

public decimal Balance
{
    get { return balance; }
    set
    {
        if (value >= 0)
            balance = value;
    }
}
```
✅ لا يمكن لأحد أن يجعل الرصيد سالباً
---

2. سهولة الصيانة والتطوير
```
// قديماً
public int age;  // إذا أردنا تغيير الاسم، نغير كل الأماكن!

// حديثاً
public int Age { get; set; }  // يمكننا تغيير التطبيق من هنا فقط
```
---
3. Security
```
private string pin;  // لا أحد يمكنه قراءتها مباشرة

public bool VerifyPin(string enteredPin)
{
    return enteredPin == pin;  // فقط التحقق، لا الوصول المباشر
}
```
--- 
4. المرونة في التطوير المستقبلي
```
// اليوم: قيمة بسيطة
private string name;

// غداً: نحتاج تسجيل تاريخ التغيير
private string name;
private DateTime nameChangedDate;

// الواجهة العامة لم تتغير!
public string Name
{
    get { return name; }
    set
    {
        name = value;
        nameChangedDate = DateTime.Now;
    }
}
```
## الأخطاء الشائعة
---

1. ❌ الخطأ 1: جعل كل شيء Public


```
public class BankAccount
{
    public decimal balance;  // 🚨 خطر!
    
    // الآن أي شخص يمكنه تعديل الرصيد مباشرة
    // account.balance = -1000;  // ❌ لا حماية!
}

الحل:

public class BankAccount
{
    private decimal balance;  // ✅ مخفي
    
    public decimal Balance
    {
        get { return balance; }
        set
        {
            if (value >= 0)
                balance = value;
        }
    }
}
```
---

2. ❌ الخطأ 2: عدم التحقق من البيانات
```
public class Person
{
    public string Name
    {
        get { return name; }
        set { name = value; }  // ⚠️ ما الذي يمنع value = ""؟
    }
}

الحل:

public class Person
{
    public string Name
    {
        get { return name; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                name = value;
            else
                throw new ArgumentException("الاسم لا يمكن أن يكون فارغاً");
        }
    }
}
```
---

3. ❌ الخطأ 3: Getter و Setter معقدة جداً
```
public int Value
{
    get
    {
        // 50 سطر من الكود! ❌
        if (...)
        {
            // ...
        }
        return something;
    }
    set { /* ... */ }
}

الحل: استخدم دوال مساعدة:

public int Value
{
    get { return CalculateValue(); }
    set { SetValue(value); }
}

private int CalculateValue()
{
    // 50 سطر من الكود ✅
    // ...
}

private void SetValue(int value)
{
    // ...
}
```
---

4. Common Misunderstandings
❌ Encapsulation = private fields فقط ❌ Encapsulation = DTOs ❌ Encapsulation slows development

✅ Encapsulation = Controlled behavior exposure
---

## Relation to Other OOP Concepts

- Abstraction: Encapsulation يخفي التفاصيل، Abstraction يختار ما يظهر
- Inheritance: لا معنى له بدون Encapsulation
- Polymorphism: يعتمد على Encapsulated behavior
---

## 🎯 أفضل الممارسات
---
1. اجعل جميع الحقول Private
```
// ❌ خطأ
public class Student
{
    public string name;
    public int age;
}

// ✅ صحيح
public class Student
{
    private string name;
    private int age;
    
    public string Name { get; set; }
    public int Age { get; set; }
}
```
---
2. استخدم Properties بدلاً من Getter/Setter
```
// ❌ قديم
public string GetName() { return name; }
public void SetName(string value) { name = value; }

// ✅ حديث
public string Name
{
    get { return name; }
    set { name = value; }
}

// ✅ الأفضل (Auto Property)
public string Name { get; set; }
```
---
 3. أضف Validation في Setters
 ```
 public int Age
{
    get { return age; }
    set
    {
        if (value >= 0 && value <= 150)
            age = value;
        else
            throw new ArgumentException("العمر غير صحيح");
    }
}
 ```
 ---

 4. استخدم Read-Only Properties عند الضرورة
 ```
 ```
 public class Person
{
    public string ID { get; }  // لا يمكن تغييره بعد الإنشاء
    
    public Person(string id)
    {
        ID = id;
    }
}

var person = new Person("123");
// person.ID = "456";  // ❌ خطأ
 ---

  5. وثق واجهاتك العامة
  ```
  public class User
{
    /// <summary>
    /// الحصول على أو تعيين اسم المستخدم
    /// </summary>
    /// <remarks>
    /// يجب أن يكون الاسم من 3 إلى 50 حرف
    /// </remarks>
    public string Username { get; set; }
    
    /// <summary>
    /// التحقق من صحة كلمة المرور
    /// </summary>
    /// <param name="password">كلمة المرور المدخلة</param>
    /// <returns>true إذا كانت صحيحة</returns>
    public bool VerifyPassword(string password)
    {
        return HashPassword(password) == storedHash;
    }
}
  ```
