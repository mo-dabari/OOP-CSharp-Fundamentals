# 🎯 OOP C# Fundamentals - أساسيات البرمجة الكائنية في سي شارب

<div align="center">

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)

**مرجع شامل ومجاني لتعلم البرمجة الكائنية (OOP) في C# من الصفر للاحتراف**

[📚 ابدأ التعلم](#-محتويات-الريبو) • [🎓 خطة التعلم](LEARNING_PATH.md) • [🤝 ساهم معنا](CONTRIBUTING.md)

</div>

---

## 📖 عن الريبو

الريبو ده مصمم خصيصاً **للطلاب والمبتدئين** اللي عايزين يفهموا البرمجة الكائنية (Object-Oriented Programming) في لغة C# بشكل عملي وسهل.

### 🎯 ليه الريبو ده مفيد ليك؟

- ✅ **شرح بالعربي** - كل المفاهيم مشروحة بالعربي مع المصطلحات التقنية بالإنجليزي
- ✅ **من الصفر للاحتراف** - مناسب للمبتدئين تماماً ومش محتاج خبرة سابقة
- ✅ **أمثلة واقعية** - كل مفهوم معاه أمثلة من الحياة العملية
- ✅ **تمارين عملية** - تمارين مع الحلول عشان تطبق اللي اتعلمته
- ✅ **مجاني 100%** - المحتوى كله مجاني ومتاح للجميع
- ✅ **تحديثات مستمرة** - بنضيف محتوى جديد باستمرار

---

## 🗺️ محتويات الريبو

### 📌 المستوى الأساسي - OOP Fundamentals

| #   | الموضوع                         | الحالة         | الوصف                                       |
| --- | ------------------------------- | -------------- | ------------------------------------------- |
| 01  | **Encapsulation** (الكبسولة)    | 🚧 قيد التطوير | إخفاء البيانات والتحكم في الوصول ليها       |
| 02  | **Abstraction** (التجريد)       | ✅ جاهز        | إخفاء التفاصيل المعقدة وإظهار الأساسيات فقط |
| 03  | **Inheritance** (الوراثة)       | ✅ جاهز        | إعادة استخدام الكود عن طريق توريث الخصائص   |
| 04  | **Polymorphism** (تعدد الأشكال) | 🚧 قيد التطوير | استخدام نفس الاسم لأكتر من وظيفة            |
| 05  | **Interfaces** (الواجهات)       | 🚧 قيد التطوير | تعريف عقود يجب على الكلاسات تنفيذها         |
| 06  | **Composition** (التركيب)       | ✅ جاهز        | بناء كلاسات معقدة من كلاسات بسيطة           |
| 07  | **Aggregation & Association**   | ✅ جاهز        | العلاقات بين الكائنات المختلفة              |

### 🏗️ أمثلة واقعية كاملة

| المشروع           | الوصف                                     |
| ----------------- | ----------------------------------------- |
| **Zoo System** 🦁 | نظام حديقة حيوانات شامل يطبق كل مبادئ OOP |

### 🎓 المستوى المتقدم

| #   | الموضوع              | الحالة         |
| --- | -------------------- | -------------- |
| 09  | **SOLID Principles** | 🚧 قيد التطوير |
| 10  | **Design Patterns**  | ⏳ قريباً      |
| 11  | **Advanced C#**      | ⏳ قريباً      |

---

## 🚀 كيف تبدأ؟

### الطريقة الأولى: متابعة خطة التعلم (مُوصى بها للمبتدئين)

1. **اقرأ ملف** [خطة التعلم](LEARNING_PATH.md) - فيه ترتيب الموضوعات خطوة بخطوة
2. **ابدأ بالترتيب** - من Encapsulation لحد ما توصل للـ Advanced Topics
3. **حل التمارين** - كل موضوع فيه تمارين عملية، حاول تحلها قبل ما تبص على الحل

### الطريقة الثانية: البحث عن موضوع معين

```

📁 اختار الموضوع اللي عايز تتعلمه (مثلاً: 02-Abstraction)
   └── 📄 اقرأ README.md - شرح المفهوم
   └── 📁 Examples - شوف الأمثلة
   └── 📁 Exercises - حل التمارين
   └── 📁 RealWorldScenarios - شوف تطبيقات واقعية

01-Encapsulation        ██████████ 100%  ✅
02-Abstraction          ██████████ 100%  ✅
03-Inheritance          ██████████ 100%  ✅
04-Polymorphism         ██████████ 100%  ✅
05-Interfaces           ██████████ 100%  ✅
06-AbstractClasses      ██████████ 100%  ✅
07-SOLID-Principles     ░░░░░░░░░░   0%  (قريباً)
08-DesignPatterns       ░░░░░░░░░░   0%  (قريباً)

الإجمالي: ~25%

```

---

## 📂 هيكل كل موضوع

كل موضوع في الريبو منظم بالشكل ده:

```
📁 اسم-الموضوع/
├── 📄 README.md                    # شرح المفهوم بالتفصيل
├── 📁 Examples/                    # أمثلة توضيحية بسيطة
│   ├── Basic...                    # أمثلة أساسية
│   └── Advanced...                 # أمثلة متقدمة
├── 📁 Exercises/                   # تمارين عملية
│   ├── Exercise01.cs               # التمرين (جرب تحله بنفسك)
│   └── Exercise01_Solution.cs      # الحل (بص عليه بعد ما تحاول)
├── 📁 RealWorldScenarios/          # مشاريع واقعية صغيرة
└── 📄 InterviewQuestions.md        # أسئلة إنترفيو شائعة
```

---

## 💡 نصائح للتعلم الفعّال

1. **ما تستعجلش** - خد وقتك في فهم كل مفهوم قبل ما تنتقل للي بعده
2. **اكتب الكود بإيدك** - ما تكتفيش بالقراءة، اكتب وجرب بنفسك
3. **حل التمارين** - التطبيق العملي أهم من الحفظ
4. **راجع الحلول** - بعد ما تحل، شوف الحل وقارن
5. **اسأل لو مش فاهم** - افتح Issue أو Discussion واحنا هنساعدك

---

## 🎯 المشاريع الواقعية

الريبو فيه مشاريع كاملة بتطبق كل اللي اتعلمته:

### 🦁 Zoo System - نظام حديقة حيوانات

مشروع شامل بيستخدم:

- Abstract Classes للحيوانات المختلفة
- Interfaces زي `IFlyable` و `IWalkable`
- Inheritance لتصنيف الحيوانات (طيور، ثدييات، إلخ)
- Encapsulation لحماية البيانات
- Polymorphism للتعامل مع الحيوانات بشكل موحد

📖 [شوف التفاصيل الكاملة](08-PublicExamples/Zoo.md)

---

## 🔥 المواضيع المتقدمة

### SOLID Principles

المبادئ الخمسة اللي بتخلي الكود بتاعك:

- سهل في الصيانة
- قابل للتوسع
- سهل الاختبار

**متوفر حالياً:**

- ✅ Dependencies & Dependency Injection
- ✅ DI Container Lifetimes (Singleton, Scoped, Transient)

**قريباً:**

- ⏳ Single Responsibility Principle
- ⏳ Open/Closed Principle
- ⏳ Liskov Substitution Principle
- ⏳ Interface Segregation Principle
- ⏳ Dependency Inversion Principle

---

## 🤝 عايز تساهم معانا؟

الريبو ده **مفتوح المصدر** وبنرحب بأي مساهمات:

- 📝 إضافة أمثلة جديدة
- 🐛 تصليح أخطاء
- 📖 تحسين الشرح
- ✨ اقتراح مواضيع جديدة

📚 اقرأ [دليل المساهمة](CONTRIBUTING.md) عشان تعرف تبدأ إزاي

---

## 📞 تواصل معانا

- 💬 **GitHub Discussions** - للأسئلة والنقاشات
- 🐛 **GitHub Issues** - للإبلاغ عن مشاكل أو اقتراحات
- ⭐ **لو الريبو عجبك** - ما تنساش تعمل Star

---

## 📜 الرخصة

المشروع متاح تحت رخصة [MIT License](LICENSE) - يعني مجاني للاستخدام الشخصي والتجاري.

---

## 🌟 دعم المشروع

لو الريبو ده ساعدك في التعلم:

- ⭐ اعمل **Star** للريبو
- 🔄 اعمل **Share** مع أصحابك
- 🐛 ساعدنا بالإبلاغ عن أي مشاكل

---

<div align="center">

**صُنع بـ ❤️ للمجتمع العربي**

من المبتدئين للمبتدئين 🚀

[⬆ Back to Top](#-oop-c-fundamentals---أساسيات-البرمجة-الكائنية-في-سي-شارب)

</div>
<<<<<<< HEAD
=======

---

# English Version

## 🎯 OOP C# Fundamentals

A comprehensive educational project for mastering Object-Oriented Programming (OOP) in C#.

### 📊 Project Statistics

- **26 C# files** with examples, exercises, and real-world scenarios
- **~15,000+ lines** of high-quality code
- **100% Arabic** comprehensive explanations
- **3 completed topics**: Encapsulation (30%), Abstraction (100%), Inheritance (100%)

### ✨ Key Features

- **In-depth Explanations**: Detailed Arabic explanations for every concept
- **Practical Examples**: 26+ real-world programming examples
- **Solved Exercises**: 3+ exercises with complete solutions per concept
- **Real-world Scenarios**: Complex projects simulating actual work environments
- **Interview Questions**: 10+ common interview questions with model answers
- **Best Practices**: Clean code standards and professional guidelines

### 🗂️ Topics Covered

1. **Encapsulation** - Data hiding, properties, access modifiers
2. **Abstraction** - Abstract classes vs interfaces, polymorphism
3. **Inheritance** - IS-A relationships, method overriding, sealed classes
4. **Polymorphism** - Coming soon
5. **Interfaces** - Coming soon
6. **SOLID Principles** - Coming soon
7. **Design Patterns** - Coming soon

### 🚀 Getting Started

```bash
# Clone the repository
git clone https://github.com/MohammedAbdullah01/OOP-CSharp-Fundamentals.git

# Navigate to a topic
cd 02-Abstraction/Examples

# Run the examples
dotnet run BasicAbstraction.cs
```

### 📖 Learning Path

Follow the structured 8-week learning path in `LEARNING_PATH.md` to master OOP concepts progressively.

### 🤝 Contributing

Contributions are welcome! Please read `CONTRIBUTING.md` for guidelines.

### 📜 License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

---

<div align="center">

**Made with ❤️ for the Arab Developer Community**

⭐ Star this repo if you find it helpful!

</div>


<<<<<<< HEAD

> > > > > > > # f4ae77a (Update README.md)
> > > > > > >
> > > > > > > f4ae77a (Update README.md)
