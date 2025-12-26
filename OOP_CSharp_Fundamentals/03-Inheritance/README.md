# 🎯 OOP in C# - Inheritance

<div dir="rtl">

## المفاهيم الأساسية


1. التعريف
الوراثة هي علاقة IS-A بين الأنواع، تُستخدم عندما يكون النوع الابن امتدادًا منطقيًا للنوع الأب.
يقوم الـ Base Class بتمثيل السلوك والحالة الجوهرية المشتركة بين جميع الأنواع المشتقة، بينما تضيف الأنواع الابنة سلوكًا أو خصائص خاصة بها، مما ينتج تسلسلًا هرميًا للأنواع ويُمكّن من الاستفادة من Polymorphism وتقليل تكرار الكود كأثر جانبي.

**الفكرة الأساسية**

إعادة استخدام السلوك والحالة المشتركة

إنشاء تسلسل هرمي للأنواع

تمكين Polymorphism

التعبير عن علاقة نوعية حقيقية (Type Relationship)

لماذا نستخدمها؟

توحيد السلوك المشترك في مكان واحد

تقليل التكرار الناتج عن تكرار المنطق المشترك

السماح بالتعامل مع الأنواع المشتقة من خلال النوع الأب

**تشبيه واقعي**

الطالب يرث من الإنسان (لديه نفس الخصائص: اسم، عمر، ...)
لكنه يضيف خصائص جديدة: رقم جامعة، معدل دراسي
وقد يعدل بعض السلوك: المعلم يعدل طريقة التدريس

</div>

- Example:
```
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public void Walk()
    {
        Console.WriteLine("يسير...");
    }
}

// الطالب هو نوع من الإنسان
public class Student : Person
{
    public string UniversityId { get; set; }
    public double GPA { get; set; }
    
    // ترث: Name, Age, Walk()
    // وتضيف: UniversityId, GPA
}
```
---

<div dir="rtl">

2. مصطلحات مهمة 
</div>

<div dir="rtl">
```

| المصطلح        | الشرح                              | المثال                         |
|---------------|------------------------------------|--------------------------------|
| Base Class    | الفئة الأب التي تحتوي السلوك المشترك | `Person`                       |
| Derived Class | الفئة الابن التي ترث من الفئة الأب   | `Student`                      |
| super / base  | للإشارة إلى الكلاس الأب             | `base.Walk()`                  |
| Override      | إعادة تعريف دالة موروثة من الأب     | `public override void Walk()`  |
| Virtual       | دالة يمكن إعادة تعريفها في الابن    | `public virtual void Work()`   |
| Sealed        | فئة لا يمكن الوراثة منها            | `public sealed class Final`    |

</div>
```
---

3. ### أنواع الوراثة

- **Single Inheritance**
```
public class Animal { }
public class Dog : Animal { }  // كلب يرث من حيوان
```

- **Multilevel**
```
public class LivingBeing { }
public class Animal : LivingBeing { }
public class Dog : Animal { }  // سلسلة وراثة
```
<div dir="rtl">

**⚠️ ملاحظة: C# لا يدعم الوراثة المتعددة من فئات!**

</div>

```
// ❌ لا يمكن:
public class Car : Vehicle, Machinery { }

// ✅ لكن يمكن من Interfaces:
public class Car : IVehicle, IMachinery { }
```
---

<div dir="rtl">

## المفاهيم الأساسية

1. ### الفرق بين Inheritance و Composition
</div>

<div dir="rtl">
```

| الفرق بين      | Inheritance (الوراثة)         | Composition (التركيب)                |
|---------------|-------------------------------|-------------------------------------|
| مثال الكود     | `public class Car : Vehicle`  | `public class Car { Vehicle engine; }` |
| العلاقة       | IS-A                          | HAS-A                               |
| قوة العلاقة    | قوية                           | ضعيفة                               |
| ما يتم وراثته | يرث كل شيء                     | فقط ما تحتاج                        |

</div>
```

<div dir="rtl">

**مثال:**
```
// ❌ Inheritance خاطئ
public class Engine { }
public class Car : Engine { }  // سيارة ليست محرك!

// ✅ Composition صحيح
public class Car
{
    public Engine Engine { get; set; }  // السيارة تحتوي على محرك
}
```
</div>
---

2. ### Access Modifiers
```
public class Person
{
    public string Name { get; set; }           // ✅ الكل يرى
    protected string SSN { get; set; }         // ✅ الأطفال يرون
    private string Password { get; set; }      // ❌ حتى الأطفال لا يرون!
    internal int EmployeeId { get; set; }      // Assembly فقط
}

public class Student : Person
{
    public void ShowInfo()
    {
        Console.WriteLine(Name);               // ✅ يعمل
        Console.WriteLine(SSN);                // ✅ يعمل
        Console.WriteLine(Password);           // ❌ خطأ!
    }
}
```
---

3. ### virtual و override
```
public class Animal
{
    // virtual = يمكن override
    public virtual void Eat()
    {
        Console.WriteLine("يأكل الطعام العام");
    }
}

public class Dog : Animal
{
    // override = تعديل السلوك
    public override void Eat()
    {
        Console.WriteLine("الكلب يأكل اللحم");
    }
}

// الاستخدام:
Animal animal = new Dog();
animal.Eat();  // الكلب يأكل اللحم (تطبيق الابن)
```
---

4. ### Constructor In Inheritance
```
public class Person
{
    public string Name { get; set; }
    
    public Person(string name)
    {
        Name = name;
        Console.WriteLine("Constructor الأب");
    }
}

public class Student : Person
{
    public string UniversityId { get; set; }
    
    // يجب استدعاء base constructor
    public Student(string name, string id) : base(name)
    {
        UniversityId = id;
        Console.WriteLine("Constructor الابن");
    }
}

// الاستخدام:
var student = new Student("أحمد", "2023001");
// Output:
// Constructor الأب
// Constructor الابن
```
---
<div dir="rtl">

## 💡 فوائد الوراثة
</div>

<div dir="rtl">

1. ### تقليل التكرار (Code Reuse)
</div>

```
// بدل كتابة نفس الكود في 10 فئات
public class Animal
{
    public void Sleep() { }
    public void Eat() { }
}

// جميع الحيوانات ترثها
public class Dog : Animal { }
public class Cat : Animal { }
public class Bird : Animal { }
```
---

<div dir="rtl">

2. ### سهولة الصيانة
</div>

```
// إذا غيرنا الأب، جميع الأطفال يتأثرون
public class Animal
{
    public virtual void Sleep()
    {
        Console.WriteLine("نائم...");  // يتم التحديث في كل مكان
    }
}
```
---

<div dir="rtl">

2. ### تنظيم هرمي (Hierarchy)
</div>

```
// تنظيم واضح:
LivingBeing
├── Animal
│   ├── Dog
│   ├── Cat
│   └── Bird
└── Plant
```
---

<div dir="rtl">

2. ### Polymorphism الحقيقي
</div>

```
List<Animal> animals = new()
{
    new Dog(),
    new Cat(),
    new Bird()
};

foreach (var animal in animals)
{
    animal.MakeSound();  // كل حيوان يصوت بطريقته!
}
```
---

<div dir="rtl">

## ⚠️ أخطاء شائعة
</div>

<div dir="rtl">

1. ### ❌ الخطأ : Inheritance بدل Composition
</div>

```
// ❌ خطأ
public class Circle : Shape { }
public class Square : Shape { }
public class Drawing : List<Shape> { }  // وراثة من List!

// ✅ صحيح
public class Drawing
{
    private List<Shape> shapes = new();  // Composition
}
```
---

<div dir="rtl">

2. ### ❌ الخطأ : نسيان استدعاء base Constructor
</div>

```
public class Student : Person
{
    // ❌ خطأ
    public Student(string name)
    {
        // Name لم يتم تعيينه! Crash!
    }
    
    // ✅ صحيح
    public Student(string name) : base(name)
    {
        // Name تم تعيينه من الأب
    }
}
```
---

<div dir="rtl">

3. ### ❌ الخطأ : Protected بدلاً من Private
</div>

```
public class Person
{
    // ❌ protected = جميع الأطفال يرون
    protected string Password { get; set; }
    
    // ✅ private = آمن
    private string Password { get; set; }
}
```
---

<div dir="rtl">

4. ### ❌ الخطأ : Deep Inheritance Hierarchy
</div>

```
// ❌ معقد جداً
public class A { }
public class B : A { }
public class C : B { }
public class D : C { }
public class E : D { }

// ✅ أقصى 2-3 مستويات
public class Base { }
public class Derived : Base { }
public class MoreDerived : Derived { }
```
---


<div dir="rtl">

## الفرق بين Inheritance و Interfaces و Abstract Classes

</div>

```
| Feature / Concept        | Class            | Abstract Class           | Interface                 |
|--------------------------|------------------|--------------------------|---------------------------|
| Multiple Inheritance     | ❌ No            | ❌ No                    | ✅ Yes                    |
| State (Fields / Data)    | ✅ Yes           | ✅ Yes                   | ❌ No                     |
| Constructor              | ✅ Yes           | ✅ Yes                   | ❌ No                     |
| Usage                    | Strong IS-A      | IS-A + Contract          | Capability / Contract     |
| Access Modifiers         | ✅ All           | ✅ All                   | ⚠️ Public only (default)  |

```
---

<div dir="rtl">

## 📝 أفضل الممارسات
### ✅ افعل هذا:
</div>

```
// 1. استخدم Inheritance لـ IS-A قوية
public class Employee : Person { }

// 2. استخدم virtual للدوال القابلة للتعديل
public class Shape
{
    public virtual double GetArea() { return 0; }
}

// 3. استدعِ base عند الحاجة
public class Circle : Shape
{
    public override double GetArea() => Math.PI * r * r;
}

// 4. استخدم sealed عندما تريد منع الوراثة
public sealed class FinalClass : BaseClass { }

// 5. الفصل الواضح بين الفئات
public class Dog : Animal { }
public class GoldenRetriever : Dog { }
```
---

<div dir="rtl">

### ❌ لا تفعل هذا:
</div>

```
// ❌ Inheritance بدون سبب
public class User : List<string> { }

// ❌ Deep Inheritance
public class Level5 : Level4 { }

// ❌ نسيان base
public class Child : Parent
{
    public Child() { /* لا استدعاء base */ }
}

// ❌ Protected على كل شيء
public class Unsafe
{
    protected string secret;  // ❌ خطر!
}

// ❌ Inheritance من أكثر من فئة
public class Multiple : ClassA, ClassB { }  // ❌ لا يعمل!
```
---


<div dir="rtl">

## حالات الاستخدام الشائعة
### 1. تصنيفات الموظفين
</div>

```
public class Employee { }
public class Manager : Employee { }
public class Developer : Employee { }
public class HR : Employee { }
```
---


<div dir="rtl">

### 2. أنظمة الدفع
</div>

```
public class PaymentMethod { }
public class CreditCard : PaymentMethod { }
public class PayPal : PaymentMethod { }
```
---

<div dir="rtl">

### 3. أنواع الحيوانات
</div>

```
public class Animal { }
public class Mammal : Animal { }
public class Dog : Mammal { }
```
---

<div dir="rtl">

### 4. واجهات المستخدم
</div>

```
public class Control { }
public class Button : Control { }
public class TextBox : Control { }
```
---


<div dir="rtl">

## مثال شامل
</div>

```
// 1. الأب
public class Vehicle
{
    public string Brand { get; set; }
    public virtual void Start() => Console.WriteLine("محرك يعمل");
}

// 2. الابن
public class Car : Vehicle
{
    public override void Start()
    {
        base.Start();
        Console.WriteLine("السيارة تستعد");
    }
}

// 3. حفيد
public class ElectricCar : Car
{
    public override void Start()
    {
        base.Start();
        Console.WriteLine("البطارية مشحونة");
    }
}

// 4. الاستخدام
Vehicle vehicle = new ElectricCar();
vehicle.Start();
// Output:
// محرك يعمل
// السيارة تستعد
// البطارية مشحونة
```
---
