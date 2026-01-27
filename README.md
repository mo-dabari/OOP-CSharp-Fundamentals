# 🎯 OOP C# Fundamentals | أساسيات البرمجة الكائنية في C#

<div align="center">

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Active-success?style=for-the-badge)

**مشروع تعليمي شامل لإتقان البرمجة الكائنية التوجه في C#**

[English](#english-version) | [العربية](#-نظرة-عامة)

</div>

---

## 📋 جدول المحتويات

- [نظرة عامة](#-نظرة-عامة)
- [إحصائيات المشروع](#-إحصائيات-المشروع)
- [المفاهيم المكتملة](#-المفاهيم-المكتملة)
- [هيكل المشروع](#-هيكل-المشروع)
- [خصائص المشروع](#-خصائص-المشروع)
- [كيفية الاستخدام](#-كيفية-الاستخدام)
- [معايير التقييم](#-معايير-التقييم)
- [نصائح الدراسة](#-نصائح-الدراسة)
- [خطة التعلم](#-خطة-التعلم)
- [المساهمة](#-المساهمة)
- [الترخيص](#-الترخيص)

---

## 🌟 نظرة عامة

**OOP C# Fundamentals** هو مشروع تعليمي متكامل يهدف إلى تقديم فهم عميق وشامل للبرمجة الكائنية التوجه (OOP) في لغة C#. المشروع مصمم للمبتدئين والمحترفين على حد سواء، ويتضمن شروحات عربية مفصلة، أمثلة عملية متطورة، تمارين محلولة، وحالات واقعية من بيئات العمل الحقيقية.

### 🎯 الأهداف الرئيسية

- ✅ **فهم عميق للمفاهيم**: شرح تفصيلي لكل مبدأ من مبادئ OOP
- ✅ **أمثلة عملية**: أكثر من 26 مثال برمجي من الحياة الواقعية
- ✅ **تمارين تطبيقية**: تمارين محلولة مع أفضل الممارسات
- ✅ **حالات واقعية**: مشاريع معقدة تحاكي بيئات العمل الفعلية
- ✅ **التحضير للمقابلات**: أسئلة مقابلات شائعة مع إجابات نموذجية
- ✅ **محتوى عربي 100%**: جميع الشروحات والتعليقات بالعربية

---

## 📊 إحصائيات المشروع

### 📁 الملفات

| النوع | العدد |
|------|------|
| ملفات C# | 26 ملف |
| ملفات README | 3 ملفات |
| ملفات أسئلة مقابلات | 3 ملفات |
| ملفات تمارين | 3 ملفات |
| ملفات حالات واقعية | 3 ملفات |

### 💻 الأسطر البرمجية

- **~15,000+ سطر** من الكود عالي الجودة
- شروحات عربية شاملة لكل سطر برمجي
- أمثلة عملية متطورة ومنظمة
- معايير برمجية احترافية (Clean Code)

### 📈 نسبة الاكتمال

```
01-Encapsulation        ██████████ 100%  ✅
02-Abstraction          ██████████ 100%  ✅
03-Inheritance          ██████████ 100%  ✅
04-Polymorphism         ██████████ 100%  ✅
05-Interfaces           ██████████ 100%  ✅
06-AbstractClasses      ██████████ 100%  ✅
07-SOLID-Principles     ██░░░░░░░░ 10%  
08-DesignPatterns       ░░░░░░░░░░   0%  (قريباً)

الإجمالي: ~25%
```

---

## 🏆 المفاهيم المكتملة

### 1️⃣ Encapsulation (الكبسولة) - قيد التطوير 🚧

**الملفات المكتملة:**
- ✅ `README.md` - شرح شامل لـ Data Hiding و Properties

**الأمثلة:**
- `BasicEncapsulation.cs` - مثال بسيط على الكبسولة
- `PropertyEncapsulation.cs` - جميع أنواع Properties (7 أمثلة)
- `DataHiding.cs` - إخفاء البيانات بتفاصيل عملية

**المواضيع المغطاة:**
- الفرق بين `public`/`private`/`protected`
- Properties و Auto-Properties
- Validation في الـ Setters
- Computed Properties
- Init-only Properties (C# 9+)
- Expression-bodied Properties
- Change Notifications

**حالات الاستخدام:**
```csharp
// BankAccount - فئة بنك مع حماية الرصيد
// Employee - موظف مع فحص الراتب
// Student - طالب مع حساب المعدل
```

---

### 2️⃣ Abstraction (التجريد) - مكتمل ✅

**الملفات المكتملة:**
- ✅ `README.md` - مقارنة شاملة بين Abstract Classes و Interfaces
- ✅ `Exercises.cs` - 3 تمارين عملية مع الحلول
- ✅ `RealWorldScenarios/DigitalLibrarySystem.cs` - نظام مكتبة رقمية
- ✅ `InterviewQuestions.md` - 10 أسئلة مقابلات + إجابات نموذجية

**الأمثلة:**
- `BasicAbstraction.cs` - حيوانات ووسائل النقل
- `ShapeCalculator.cs` - نظام حساب الأشكال الهندسية
- `PaymentSystem.cs` - نظام دفع متقدم مع Dependency Injection

**التمارين المحلولة:**
1. **نظام النقل** (Vehicle Management System)
2. **نظام الإشعارات** (Notification System)
3. **نظام التخزين** (Data Storage System)

**حالة واقعية معقدة:**
- **نظام المكتبة الرقمية**: يتضمن أنواع محتوى متعددة (كتب، فيديو، بودكاست)، طرق وصول مختلفة (بث، تنزيل)، ونظام توصيات ذكي

**المواضيع المغطاة:**
- Abstract Classes vs Interfaces
- Virtual و Abstract Methods
- Polymorphism في الواقع العملي
- Dependency Injection
- Factory Pattern
- Strategy Pattern
- Liskov Substitution Principle (LSP)

---

### 3️⃣ Inheritance (الوراثة) - مكتمل ✅

**الملفات المكتملة:**
- ✅ `README.md` - شرح شامل للوراثة والفرق مع Composition
- ✅ `Exercises.cs` - 3 تمارين عملية مع الحلول
- ✅ `RealWorldScenarios/StudentManagementSystem.cs` - نظام إدارة جامعة
- ✅ `InterviewQuestions.md` - 10 أسئلة مقابلات + إجابات نموذجية

**الأمثلة:**
- `BasicInheritance.cs` - Person, Student, Employee, Manager, GraduateStudent, Doctor
- `ShapeHierarchy.cs` - نظام أشكال هندسية (2D و 3D)
- `EmployeeSystem.cs` - نظام موظفي شركة متقدم

**التمارين المحلولة:**
1. **نظام الحيوانات** (Animal Sanctuary Management)
2. **نظام المستندات** (Document Management System)
3. **نظام المركبات** (Fleet Management System)

**حالة واقعية معقدة:**
- **نظام إدارة الجامعة**: يشمل طلاب عاديين (Undergraduate)، طلاب دراسات عليا (Graduate)، طلاب تبادل (Exchange)، مع حساب GPA والرسوم الدراسية

**المواضيع المغطاة:**
- Inheritance vs Composition (IS-A vs HAS-A)
- Single و Multilevel Inheritance
- Virtual و Override
- Base keyword
- Sealed Classes
- Constructor Chain
- Access Modifiers (protected/private/public)
- Method Overriding vs Overloading

---

## 📁 هيكل المشروع

```
OOP-CSharp-Fundamentals/
│
├── 📄 README.md                        # المقدمة العامة (هذا الملف)
├── 📄 CONTRIBUTING.md                  # دليل المساهمة
├── 📄 LEARNING_PATH.md                 # خطة التعلم (8 أسابيع)
├── 📄 LICENSE                          # رخصة المشروع
│
├── 📂 01-Encapsulation/               # الكبسولة 🚧
│   ├── 📄 README.md                   ✅
│   ├── 📂 Examples/
│   │   ├── BasicEncapsulation.cs      ✅
│   │   ├── PropertyEncapsulation.cs   ✅
│   │   └── DataHiding.cs              ✅
│   ├── 📂 Exercises/                  ✅
│   ├── 📂 RealWorldScenarios/         ✅ 
│   └── 📄 InterviewQuestions.md       ⏳ قريباً
│
├── 📂 02-Abstraction/                 # التجريد ✅
│   ├── 📄 README.md                   ✅
│   ├── 📂 Examples/
│   │   ├── BasicAbstraction.cs        ✅
│   │   ├── ShapeCalculator.cs         ✅
│   │   └── PaymentSystem.cs           ✅
│   ├── 📂 Exercises/
│   │   └── Exercises.cs               ✅
│   ├── 📂 RealWorldScenarios/
│   │   └── DigitalLibrarySystem.cs    ✅
│   └── 📄 InterviewQuestions.md       ⏳ قريباً  
│
├── 📂 03-Inheritance/                 # الوراثة ✅
│   ├── 📄 README.md                   ✅
│   ├── 📂 Examples/
│   │   ├── BasicInheritance.cs        ✅
│   │   ├── ShapeHierarchy.cs          ✅
│   │   └── EmployeeSystem.cs          ✅
│   ├── 📂 Exercises/
│   │   └── Exercises.cs               ✅
│   ├── 📂 RealWorldScenarios/
│   │   └── StudentManagementSystem.cs ✅
│   └── 📄 InterviewQuestions.md       ⏳ قريباً
│
├── 📂 04-Polymorphism/                ✅
├── 📂 05-Interfaces/                  ✅ قريباً
├── 📂 07-SOLID-Principles/            ⏳ قريباً
├── 📂 08-DesignPatterns/              ⏳ قريباً
├── 📂 09-AdvancedOOP/                 ⏳ قريباً
├── 📂 10-Projects/                    ⏳ قريباً
├── 📂 11-ExamPreparation/             ⏳ قريباً
└── 📂 12-Extras/                      ⏳ قريباً
```

---

## ✨ خصائص المشروع

### 🎓 للطلاب والمبتدئين

- **شرح عربي شامل**: جميع الملفات والشروحات بالعربية الفصحى
- **أمثلة بسيطة ومعقدة**: تبدأ من الأساسيات وتنتقل للمتقدم
- **تمارين محلولة**: 3+ تمارين مع حلول كاملة لكل مفهوم
- **رسوم توضيحية**: مقارنات وجداول لتوضيح الفروقات

### 💼 للمحترفين والمطورين

- **حالات واقعية معقدة**: مشاريع تحاكي بيئات العمل الحقيقية
- **Best Practices**: أفضل الممارسات البرمجية في كل موضوع
- **Design Patterns**: أنماط التصميم مع تطبيقات عملية
- **Clean Architecture**: معايير الكود النظيف والقابل للصيانة

### 🎯 للمقابلات التقنية

- **أسئلة مقابلات**: 10+ سؤال شائع مع إجابات نموذجية
- **Common Mistakes**: الأخطاء الشائعة وكيفية تجنبها
- **تمارين تفاعلية**: أسئلة coding challenges مع الحلول
- **نصائح المقابلات**: استراتيجيات للإجابة بثقة

---

## 🚀 كيفية الاستخدام

### 📚 للمبتدئين

1. **ابدأ بالترتيب**: اقرأ `README.md` في كل مفهوم بالترتيب
2. **تابع الأمثلة**: اقرأ الكود خطوة بخطوة مع التعليقات
3. **اكتب بنفسك**: لا تنسخ الكود، اكتبه بيدك لترسيخ المعلومات
4. **حل التمارين**: جرب حل التمارين قبل النظر للحل
5. **تطبيق عملي**: ادرس الحالات الواقعية وحاول تطبيقها بنفسك

```bash
# ابدأ من هنا
01-Encapsulation/README.md       # اقرأ الشرح أولاً
01-Encapsulation/Examples/       # ثم تابع الأمثلة
01-Encapsulation/Exercises/      # ثم حل التمارين
```

### 💡 للمتقدمين

1. **اقفز للتمارين**: ابدأ بحل التمارين مباشرة
2. **راجع الحلول**: قارن حلك مع الحل النموذجي
3. **ادرس الحالات الواقعية**: ركز على المشاريع المعقدة
4. **أسئلة المقابلات**: احفظ الأجوبة وتدرب على شرحها
5. **بناء مشاريع**: طبق المفاهيم في مشاريعك الخاصة

### 🎤 للتحضير للمقابلات

1. ادرس أسئلة المقابلات في كل موضوع
2. افهم الإجابات بعمق، لا تحفظها فقط
3. مارس الإجابة شفهياً أمام المرآة
4. أضف أمثلة من تجربتك الشخصية
5. راجع الأخطاء الشائعة وكيفية تجنبها

---

## 🏅 معايير التقييم

### ✅ أنت تفهم الموضوع عندما:

#### 1. الشرح الذاتي
- ✅ تستطيع شرح المفهوم **بكلماتك الخاصة**
- ✅ بدون النظر للملف أو الأمثلة
- ✅ بشكل واضح ومفهوم لشخص مبتدئ

#### 2. الكتابة من الذاكرة
- ✅ تستطيع كتابة أمثلة من الذاكرة
- ✅ بدون نسخ أو لصق
- ✅ بأسلوبك البرمجي الخاص

#### 3. حل التمارين الجديدة
- ✅ تستطيع حل تمارين إضافية لم تراها قبلاً
- ✅ بدون مساعدة أو رجوع للأمثلة
- ✅ بثقة وسرعة

#### 4. الإجابة على أسئلة المقابلات
- ✅ تجيب على الأسئلة بسرعة
- ✅ تشرح بتفصيل كامل
- ✅ تعطي أمثلة واقعية من خبرتك

#### 5. التطبيق العملي
- ✅ تستطيع بناء تطبيق صغير
- ✅ يستخدم المفهوم بشكل صحيح
- ✅ يتبع أفضل الممارسات (Best Practices)

---

## 💡 نصائح الدراسة

### ✅ افعل هذا

| النصيحة | التوضيح |
|---------|----------|
| **اكتب الكود بنفسك** | لا تنسخ/تلصق - الكتابة اليدوية ترسخ المعلومات |
| **جرب التعديلات** | غيّر المتغيرات، أضف حالات جديدة، اكسر الكود وأصلحه |
| **استخدم Debugger** | اتبع التنفيذ خطوة بخطوة لفهم كيف يعمل الكود |
| **اكتب ملاحظاتك** | دوّن ما تعلمته بأسلوبك الخاص |
| **علّم غيرك** | اشرح للآخرين - التعليم أفضل طريقة للتعلم |
| **بناء مشاريع** | طبّق على حالات حقيقية من خيالك |

### ❌ تجنب هذا

| الخطأ | لماذا يجب تجنبه |
|-------|-----------------|
| **النسخ واللصق** | لن تتعلم شيئاً - الكتابة اليدوية ضرورية |
| **الحفظ بدون فهم** | ستنسى كل شيء بعد أسبوع |
| **تخطي التمارين** | التمارين هي الجزء الأهم للتعلم |
| **الاستعجال** | خذ وقتك - الفهم أهم من السرعة |
| **الاستسلام** | الأخطاء طبيعية - حاول مرة أخرى |
| **الكود فقط** | افهم السبب والمنطق وراء الكود |

### 🎯 استراتيجية الدراسة المثالية

```
1. اقرأ الشرح        → فهم المفهوم النظري
2. تابع الأمثلة      → رؤية التطبيق العملي
3. اكتب الكود بنفسك  → ترسيخ المعلومات
4. حل التمارين       → اختبار الفهم
5. الحالات الواقعية → التطبيق المتقدم
6. المقابلات        → التحضير للعمل

كرر الدورة حتى تتقن المفهوم تماماً
```

---

## 📅 خطة التعلم

### 🗓️ خطة 8 أسابيع

| الأسبوع | الموضوع | المهام |
|---------|---------|--------|
| **1** | Encapsulation | README + Examples + Exercises |
| **2** | Abstraction | README + Examples + Exercises + Real World |
| **3** | Inheritance | README + Examples + Exercises + Real World |
| **4** | Polymorphism | README + Examples + Exercises + Real World |
| **5** | Interfaces | README + Examples + Exercises + Real World |
| **6** | SOLID Principles | الخمس مبادئ + أمثلة عملية |
| **7** | Design Patterns | أهم 10 أنماط + تطبيقات |
| **8** | Exam Preparation | مراجعة + مقابلات + مشاريع |

### 📊 التقدم اليومي المقترح

```
الأسبوع الواحد = 5 أيام عمل

اليوم 1: قراءة README + فهم المفهوم          (2 ساعة)
اليوم 2: دراسة الأمثلة + كتابة الكود          (3 ساعات)
اليوم 3: حل التمارين بنفسك                   (3 ساعات)
اليوم 4: دراسة الحالات الواقعية             (3 ساعات)
اليوم 5: مراجعة + أسئلة المقابلات + مشروع صغير (3 ساعات)

المجموع: 14 ساعة / أسبوع
```

---

## 💻 الأمثلة العملية المغطاة

### 🏦 أنظمة مالية
- **نظام البنك**: إدارة الحسابات والرصيد (Encapsulation)
- **نظام الدفع**: طرق دفع متعددة (Abstraction + Strategy Pattern)

### 🎓 أنظمة تعليمية
- **نظام الجامعة**: إدارة الطلاب والدرجات (Inheritance)
- **نظام المكتبة**: إدارة المحتوى الرقمي (Abstraction)

### 🏢 أنظمة موارد بشرية
- **نظام الموظفين**: إدارة الرواتب والترقيات (Inheritance)
- **نظام الحضور**: تتبع ساعات العمل (Polymorphism)

### 🚗 أنظمة نقل
- **نظام المركبات**: إدارة الأسطول (Inheritance)
- **نظام النقل**: وسائل نقل متعددة (Abstraction)

### 📐 أنظمة هندسية
- **حساب الأشكال**: مساحات وحجوم (Abstraction + Polymorphism)
- **نظام رسم**: رسم أشكال 2D و 3D (Inheritance)

---

## 🤝 المساهمة

نرحب بمساهماتك! إذا كنت ترغب في إضافة محتوى، تحسين الشروحات، أو إصلاح الأخطاء:

1. **Fork** المشروع
2. أنشئ **Branch** جديد (`git checkout -b feature/new-concept`)
3. **Commit** تغييراتك (`git commit -m 'Add new concept'`)
4. **Push** إلى Branch (`git push origin feature/new-concept`)
5. افتح **Pull Request**

### 📋 إرشادات المساهمة

- **الشروحات**: يجب أن تكون بالعربية الفصحى
- **الكود**: اتبع معايير C# Coding Conventions
- **التعليقات**: أضف تعليقات توضيحية بالعربية
- **الأمثلة**: يجب أن تكون عملية وواقعية
- **الاختبار**: تأكد من أن الكود يعمل بدون أخطاء

---

## 📞 الدعم والمساعدة

### 🆘 إذا لم تفهم شيئاً

1. اقرأ الشرح مرة أخرى **بتمعن وتركيز**
2. ادرس الأمثلة **بتفاصيلها الدقيقة**
3. حاول كتابة **مثالك الخاص** من الصفر
4. ابحث في **Microsoft Docs** للمزيد
5. اطرح **سؤال محدد** في Issues

### 🐛 إذا وجدت خطأ

1. تحقق من **القواعد النحوية** (Syntax)
2. استخدم **Debugger** لتتبع المشكلة
3. اقرأ **رسالة الخطأ** بعناية
4. ابحث عن **الحل** في Google/StackOverflow
5. أبلغنا عبر **Issues** إذا كان خطأ في المشروع

### 📚 موارد إضافية

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)
- [.NET API Browser](https://docs.microsoft.com/en-us/dotnet/api/)

---

## 📜 الترخيص

هذا المشروع مرخص تحت **MIT License** - راجع ملف [LICENSE](LICENSE) للتفاصيل.

```
MIT License

Copyright (c) 2024 Mohammed Abdullah

يُسمح باستخدام، نسخ، تعديل، ودمج هذا المشروع مجاناً
```

---

## 🌟 إذا أعجبك المشروع

إذا استفدت من هذا المشروع:
- ⭐ **Star** المشروع على GitHub
- 🔗 **شاركه** مع أصدقائك والمطورين
- 💬 **تابع** التحديثات الجديدة
- 🤝 **ساهم** بإضافة محتوى جديد

---

## 👨‍💻 عن المؤلف

**Mohammed Abdullah**
- 💼 Backend Software Engineer
- 🎯 متخصص في C# و .NET Core
- 🏗️ مهتم بـ Clean Architecture و Design Patterns
- 📚 شارح تقني على LinkedIn

### 📱 تواصل معي

- 🔗 [GitHub Portfolio](https://github.com/MohammedAbdullah01)
- 💼 [LinkedIn Profile](https://linkedin.com/in/your-profile)
- 📧 [Email](mailto:your.email@example.com)

---

## 🎯 الأهداف المستقبلية

### ⏳ قريباً

- [ ] إكمال Encapsulation (Exercises + Real World + Interview)
- [ ] Polymorphism (4 أمثلة + تمارين + حالات واقعية)
- [ ] Interfaces (شرح شامل + أمثلة متقدمة)
- [ ] Abstract Classes (الفروقات + أفضل الاستخدامات)

### 🚀 المستقبل

- [ ] SOLID Principles (الخمس مبادئ بالتفصيل)
- [ ] Design Patterns (23 نمط تصميم)
- [ ] Advanced OOP (Generic Types, Delegates, Events)
- [ ] Real Projects (5 مشاريع كاملة)
- [ ] Exam Preparation (اختبارات ومحاكاة مقابلات)

---

<div align="center">

## 📚 رحلتك في إتقان OOP تبدأ هنا!

**صُمم بـ ❤️ للمجتمع العربي من المطورين**

[⬆ العودة للأعلى](#-oop-c-fundamentals--أساسيات-البرمجة-الكائنية-في-c)

---

</div>

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



