# 🦁 Zoo Management System

نظام إدارة حديقة حيوانات مبني بـ **C#** يوضح تطبيق مبادئ **OOP** و **SOLID Principles**.

---

## 📖 نظرة عامة

مشروع يوضح تصميم نظام إدارة حديقة حيوانات باستخدام:

- **Abstract Classes** و **Interfaces** لبناء Hierarchy واضح
- **SOLID Principles** لكود نظيف وقابل للتوسع
- **Modern C# Features** للتحقق من المدخلات والأمان

---

## 🏗️ التصميم المعماري

### البنية الأساسية

```
Animal (Abstract)
├── Mammal (Abstract) → Lion, Elephant
└── Bird (Abstract) → Eagle, Penguin

Enclosure (Abstract)
├── Cage
└── WaterEnclosure (Abstract) → Pool, Basin

Zoo (Aggregate Root)
└── Contains: Enclosures → Animals
```

### Class Diagram

```
        Animal (Abstract)
        ├── MakeSound()
        ├── Eat()
        └── DisplayInfo()
              ↓
    ┌─────────┴─────────┐
Mammal (Abstract)    Bird (Abstract)
    ↓                    ↓
Lion, Elephant      Eagle, Penguin
    ↓                    ↓
IWalkable          IFlyable
ISwimmable         ISwimmable


    Enclosure (Abstract)
    ├── AddAnimal()
    ├── RemoveAnimal()
    └── Clean()
          ↓
    ┌─────┴──────┐
  Cage    WaterEnclosure (Abstract)
                 ↓
            Pool, Basin
```

---

## 🎨 Design Patterns

### 1. Template Method Pattern

```csharp
public abstract class Animal
{
    public abstract void MakeSound(); // Must override
    public abstract void Eat();       // Must override
    public virtual string DisplayInfo() { } // Can override
}
```

### 2. Strategy Pattern (via Interfaces)

```csharp
public interface IFlyable { void Fly(); }
public interface ISwimmable { void Swim(); }
public interface IWalkable { void Walk(); }

// الفيل يستطيع المشي والسباحة
public class Elephant : Mammal, IWalkable, ISwimmable { }
```

### 3. Encapsulation

```csharp
private List<Animal> _animals; // Internal state
public IReadOnlyList<Animal> Animals; // Read-only access
```

---

## 🔐 SOLID Principles

| Principle                 | تطبيق في المشروع                                             |
| ------------------------- | ------------------------------------------------------------ |
| **Single Responsibility** | كل كلاس له مسؤولية واحدة (Zoo للإدارة، Animal للبيانات)      |
| **Open/Closed**           | مفتوح للتوسع (إضافة حيوانات جديدة) بدون تعديل الكود الأساسي  |
| **Liskov Substitution**   | أي Animal يمكن استبداله بـ Lion أو Elephant بدون مشاكل       |
| **Interface Segregation** | واجهات صغيرة (IFlyable, ISwimmable) بدلاً من واجهة كبيرة     |
| **Dependency Inversion**  | الاعتماد على Abstractions (Animal) بدلاً من Concrete classes |

---

## 💻 التقنيات المستخدمة

- **C# 12** - Modern language features
- **.NET 8+** - Latest framework
- **ArgumentException APIs** - Input validation
- **ReadOnly Collections** - Data protection

### Modern C# Features

```csharp
// C# 11+ Validation
ArgumentNullException.ThrowIfNull(name);
ArgumentOutOfRangeException.ThrowIfNegativeOrZero(age);

// Init-only properties
public string Name { get; }

// Read-only collections
Animals = _animals.AsReadOnly();
```

---

## 🚀 مثال الاستخدام

```csharp
// إنشاء حديقة
var address = new Address("Egypt", "Cairo", "Giza", "Pyramids Road", 100);
var zoo = new Zoo("Giza Zoo", 50000.0, address);

// إنشاء حيوانات
var lion = new Lion("Simba", 5, hasDanger: true);
var elephant = new Elephant("Dumbo", 10, hasDanger: false);

// إنشاء قفص وإضافة حيوان
var cage = new Cage("Lion Enclosure", 100.0, 50.0, false, true);
cage.AddAnimal(lion);
zoo.AddEnclosure(cage);

// استخدام القدرات
lion.Walk(); // IWalkable
elephant.Walk(); // IWalkable
elephant.Swim(); // ISwimmable
```

---

## 📁 هيكل المشروع

```
ZooManagementSystem/
├── AbstractClasses/
│   ├── Animal.cs
│   ├── Bird.cs
│   ├── Mammal.cs
│   ├── Enclosure.cs
│   └── WaterEnclosure.cs
├── ConcreteClasses/
│   ├── Animals/ (Lion, Elephant, Eagle, Penguin)
│   └── Enclosures/ (Cage, Pool, Basin)
├── Interfaces/
│   ├── IFlyable.cs
│   ├── ISwimmable.cs
│   └── IWalkable.cs
├── ValueObjects/
│   └── Address.cs
└── Zoo.cs
```

---

## 🧠 المفاهيم المطبقة

✅ **Abstraction** - فصل التعريف عن التنفيذ
✅ **Inheritance** - Hierarchy واضح (Animal → Mammal → Lion)
✅ **Polymorphism** - تعدد الأشكال (`Animal animal = new Lion()`)
✅ **Encapsulation** - إخفاء التفاصيل الداخلية
✅ **Composition** - استخدام Interfaces بدلاً من وراثة معقدة
✅ **Defensive Programming** - التحقق من المدخلات ومنع الأخطاء

---

## 🔄 التحسينات المستقبلية

- [ ] إضافة **MaxCapacity** للحاويات
- [ ] تطبيق **Repository Pattern**
- [ ] استخدام **Entity Framework Core**
- [ ] بناء **ASP.NET Core Web API**
- [ ] إضافة **Unit Tests**

---

## 👨‍💻 المطور

**Mohammed Abdullah**
Backend Software Engineer | C# & .NET Specialist

- 🌐 GitHub: [@MohammedAbdullah01](https://github.com/MohammedAbdullah01)
- 💼 LinkedIn: [Mohammed Abdullah](https://linkedin.com/in/mohammedabdullah01)

---

**Built with ❤️ using C# and .NET**
