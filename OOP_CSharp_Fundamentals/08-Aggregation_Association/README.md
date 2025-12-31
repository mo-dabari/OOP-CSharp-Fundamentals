## 07 - Aggregation (التجميع)

### مقدمة عن التجميع

التجميع (Aggregation) هو علاقة "جزء من" حيث الأجزاء يمكن أن توجد بشكل مستقل عن الكل.

#### التشبيه

- **الفريق لديه لاعبون** - إذا حذفنا الفريق، اللاعبون يبقون
- **الشركة لديها موظفون** - الموظفون يمكن أن يتركوا
- **الفصل لديه طلاب** - الطالب يمكن أن ينتقل لفصل آخر

```csharp
public class Team
{
    private List<Player> players;  // Aggregation
    // الفريق يُحذف، لكن اللاعبون يبقون
}
```

---

## الفرق بين Aggregation و Composition

| الجانب | Composition | Aggregation |
|--------|-------------|-------------|
| **القوة** | قوية | ضعيفة نسبياً |
| **دورة الحياة** | محكومة | مستقلة |
| **الاستقلالية** | معتمد | مستقل |
| **المثال** | Car-Engine | Team-Player |

---

## 📚 08 - Association (الارتباط)

### مقدمة عن الارتباط

الارتباط (Association) هو أضعف علاقة حيث كائنين لديهما معرفة ببعضهم بدون اعتماد قوي.

#### التشبيه

- **الطالب يسجل في مقرر** - معرفة بسيطة
- **المعلم يدرس الطلاب** - تفاعل مؤقت
- **السيارة تحمل السائق** - استخدام مؤقت

```csharp
public class Student
{
    public void Enroll(Course course) { }
}

public class Course
{
    public void AddStudent(Student student) { }
}
```

---

## الفرق بين الثلاثة

```
Composition (قوي):
Car ⊃ Engine
• Engine موجود فقط مع Car
• ينتهي مع Car
• تحكم كامل

Aggregation (متوسط):
Team ⊃ Player
• Player قد يكون في فرق أخرى
• يمكن أن يبقى بدون Team
• استقلالية نسبية

Association (ضعيف):
Student ↔ Course
• معرفة بسيطة فقط
• لا اعتماد قوي
• تفاعل مؤقت
```

---

## 🎯 متى تستخدم كل واحدة؟

### Composition (قوي)

```csharp
public class House
{
    private Roof roof;      // يملك
    private Walls walls;    // يملك
    private Foundation foundation;  // يملك
}
```

### Aggregation (متوسط)

```csharp
public class Department
{
    private List<Employee> employees;  // لديه
    // Employee قد يكون في department آخر
}
```

### Association (ضعيف)

```csharp
public class Doctor
{
    public void Treat(Patient patient) { }  // يعالج
    // علاقة مؤقتة فقط
}
```

---

## ⚠️ أخطاء شائعة

### ❌ خلط الأنواع

```csharp
// ❌ خطأ - Composition بدل Aggregation
public class Department
{
    private List<Employee> employees = new();  // لا! موظفون مستقلون
}

// ✅ صحيح
public class Department
{
    private List<Employee> employees;

    public void HireEmployee(Employee emp)
    {
        employees.Add(emp);  // Employee موجود قبلاً
    }
}
```

---

## 📊 المقارنة النهائية

```
| Relationship Type | Composition | Aggregation | Association |
|------------------|------------|------------|------------|
| Relationship      | Owns       | Has        | Knows/Uses |
| Strength          | Very Strong| Medium     | Weak       |
| Independence      | Dependent  | Independent| Fully Independent |
| Lifecycle         | Controlled | Separate   | Separate   |

```

---

## 🚀 أفضل الممارسات

```
✅ استخدم الأنسب لكل حالة
✅ تجنب Inheritance إلا عند الضرورة
✅ فكر في دورة الحياة
✅ تذكر Independent Objects
✅ الوضوح والبساطة أولاً
```
