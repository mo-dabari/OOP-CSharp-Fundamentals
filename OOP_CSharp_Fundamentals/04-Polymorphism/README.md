## 05 - Polymorphism (تعدد الأشكال)

### مقدمة عن تعدد الأشكال

تعدد الأشكال (Polymorphism) هو القدرة على استخدام كائن واحد بطرق مختلفة حسب نوعه. الكود الواحد يسلك بأشكال مختلفة حسب السياق.

#### التشبيه الحقيقي:
- **الزر**: قد يكون زر تشغيل على الهاتف، أو زر إرسال على الكمبيوتر
- **الضغط على الزر**: نفس الحركة، لكن النتيجة مختلفة!
- **الشكل**: واحد (زر)
- **السلوك**: مختلف (تشغيل الهاتف / إرسال البريد)

---

## 📚 أنواع Polymorphism

### 1. Compile-time Polymorphism (Static)

#### أ) Method Overloading

```csharp
public class Calculator
{
    // نفس الاسم، parameters مختلفة

    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}

// الاستخدام:
var calc = new Calculator();
calc.Add(1, 2);          // int version
calc.Add(1.5, 2.5);      // double version
calc.Add(1, 2, 3);       // three int version

// الكمبايلر يختار الدالة الصحيحة في Compile Time
```

**المميزات:**
- ✅ سهل الفهم
- ✅ آمن (معروف في الكمبايل)
- ✅ بدون overhead في Runtime

**المعايير للـ Overloading:**
```csharp
✅ مختلف في عدد Parameters
✅ مختلف في نوع Parameters
✅ مختلف في ترتيب Parameters

❌ مختلف في Return Type فقط (لا يكفي!)
```

#### ب) Operator Overloading

```csharp
public class Vector
{
    public int X { get; set; }
    public int Y { get; set; }

    // تعريف + للـ Vector
    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector { X = a.X + b.X, Y = a.Y + b.Y };
    }

    // تعريف == للـ Vector
    public static bool operator ==(Vector a, Vector b)
    {
        return a.X == b.X && a.Y == b.Y;
    }
}

// الاستخدام:
var v1 = new Vector { X = 1, Y = 2 };
var v2 = new Vector { X = 3, Y = 4 };
var v3 = v1 + v2;  // استخدام +
if (v1 == v2) { }  // استخدام ==
```

---

### 2. Runtime Polymorphism (Dynamic)

#### أ) Method Overriding مع Virtual

```csharp
// الأب
public abstract class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("رسم شكل عام");
    }
}

// الابن 1
public class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("رسم دائرة");
    }
}

// الابن 2
public class Square : Shape
{
    public override void Draw()
    {
        Console.WriteLine("رسم مربع");
    }
}

// الاستخدام - Polymorphism الحقيقي!
Shape shape = new Circle();
shape.Draw();  // رسم دائرة (تطبيق Circle)

shape = new Square();
shape.Draw();  // رسم مربع (تطبيق Square)

// نفس المتغير، سلوك مختلف!
```

#### ب) Interface Polymorphism

```csharp
public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount);
}

public class CreditCard : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"دفع ببطاقة: {amount:C}");
        return true;
    }
}

public class PayPal : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"دفع بـ PayPal: {amount:C}");
        return true;
    }
}

// الاستخدام
IPaymentProcessor processor = new CreditCard();
processor.ProcessPayment(100);

processor = new PayPal();
processor.ProcessPayment(100);
```

---

## 🎯 الفرق بين Compile-time و Runtime

| الجانب | Compile-time | Runtime |
|--------|---------|---------|
| **الوقت** | في التجميع | أثناء التنفيذ |
| **الدالة** | معروفة سلفاً | تحدد من النوع الفعلي |
| **الأداء** | أسرع | أبطأ قليلاً |
| **الأمان** | أكثر أماناً | أقل أماناً |
| **المثال** | Overloading | Overriding |

```csharp
// Compile-time - الكمبايلر يعرف أي دالة
var calc = new Calculator();
calc.Add(1, 2);      // معروف في الكمبايل

// Runtime - يتحدد أثناء التنفيذ
Shape shape = GetRandomShape();  // Circle أم Square؟
shape.Draw();  // نعرف فقط عند التنفيذ!
```

---

## 💡 فوائد Polymorphism

### 1. الكود الموحد (Unified Code)

```csharp
// ❌ بدون Polymorphism - كود مكرر
if (shape is Circle)
{
    ((Circle)shape).Draw();
}
else if (shape is Square)
{
    ((Square)shape).Draw();
}

// ✅ مع Polymorphism - كود واحد
shape.Draw();  // يعمل مع كل الأنواع!
```

### 2. المرونة (Flexibility)

```csharp
// إضافة شكل جديد = فئة جديدة فقط
public class Triangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("رسم مثلث");
    }
}

// الكود القديم يعمل بدون تعديل!
```

### 3. سهولة الصيانة (Maintainability)

```csharp
// كل شكل مسؤول عن نفسه
// لا حاجة لفحص الأنواع
// لا if/else معقدة
```

---

## ⚠️ أخطاء شائعة

### ❌ الخطأ 1: Shadowing بدل Overriding

```csharp
// ❌ خطأ
public class Animal
{
    public void MakeSound()
    {
        Console.WriteLine("صوت عام");
    }
}

public class Dog : Animal
{
    public new void MakeSound()  // ❌ new = shadowing!
    {
        Console.WriteLine("واف واف");
    }
}

// المشكلة:
Animal dog = new Dog();
dog.MakeSound();  // صوت عام ❌ (الأب!)

// ✅ الحل:
public class Dog : Animal
{
    public override void MakeSound()  // ✅ override!
    {
        Console.WriteLine("واف واف");
    }
}

Animal dog = new Dog();
dog.MakeSound();  // واف واف ✅ (الابن)
```

### ❌ الخطأ 2: نسيان virtual في الأب

```csharp
// ❌ خطأ - لم نضع virtual
public class Parent
{
    public void Method() { }
}

public class Child : Parent
{
    public override void Method() { }  // ❌ خطأ - لا يوجد virtual!
}

// ✅ الحل
public class Parent
{
    public virtual void Method() { }  // ✅ virtual!
}

public class Child : Parent
{
    public override void Method() { }  // ✅ الآن يعمل!
}
```

### ❌ الخطأ 3: Cast غير آمن

```csharp
// ❌ خطأ - قد يفشل
Shape shape = new Circle();
Square square = (Square)shape;  // ❌ ClassCastException!

// ✅ الحل - استخدم is/as
if (shape is Square)
{
    Square square = (Square)shape;
}

// أو
Square square = shape as Square;
if (square != null) { }
```

---

## 🔍 متى تستخدم Polymorphism؟

### ✅ استخدم عندما:

1. **عدة أنواع، سلوك واحد**
```csharp
foreach (var animal in animals)
{
    animal.MakeSound();  // حيواني مختلفة، صوت واحد
}
```

2. **التوسع المستقبلي**
```csharp
// إضافة موظف جديد = فئة جديدة
public class Manager : Employee { }
```

3. **الفصل بين العقد والتطبيق**
```csharp
public interface IRepository { }
// تطبيقات مختلفة
```

---

## 📊 مقارنة سريعة

```
Overloading    Overriding
─────────────────────────────────────────────
الوقت              Compile-time    Runtime
الأساس             عدد المعاملات    المكان (virtual)
الأمان             أكثر أماناً      أقل أماناً
الأداء             أسرع            أبطأ
التطبيق           نفس الفئة        فئات مختلفة
─────────────────────────────────────────────
```

---

## 🚀 الخطوات التالية

1. **اقرأ الأمثلة:**
   - BasicPolymorphism.cs - أمثلة بسيطة
   - AdvancedPolymorphism.cs - حالات معقدة
   - PolymorphicCollections.cs - قوائم متعددة الأنواع

2. **حل التمارين:**
   - Exercises.cs - 3 تمارين عملية

3. **ادرس الحالات الواقعية:**
   - RealWorldScenarios/ - مشاريع حقيقية

4. **أسئلة المقابلات:**
   - InterviewQuestions.md - 10 أسئلة مع إجابات

"feat: add Abstraction concept with examples and exercises and RealWorldScenarios"
